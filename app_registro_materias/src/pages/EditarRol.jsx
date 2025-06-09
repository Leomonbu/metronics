import { useState } from "react";
import axios from "axios";
import { toast } from "react-toastify";
import Breadcrumbs from "../components/Breadcrumbs";
import { useAuth } from "../auth/AuthProvider";
import "../css/RolForm.css";

const EditarRol = () => {
  const context = useAuth();
  const [form, setForm] = useState({ idEstudiante: context.user.nameid, idRole: "" });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const token = context.token;
      await axios.put(`http://localhost:5148/api/Estudiantes_Rol/${form.idEstudiante}`, form, {
        headers: {
            Authorization: `Bearer ${token}`
          }
      });
      toast.success("Rol actualizado correctamente");
    } catch (error) {
      console.error("Error al actualizar rol:", error);
      toast.error("Hubo un error al actualizar el rol.");
    }
  };

  return (
    <div className="rol-empty">
      <Breadcrumbs />
      
      <div className="rol-form-container">
        <h2>Editar Rol</h2>
        <form className="rol-form" onSubmit={handleSubmit}>
          <input
            type="number"
            name="idEstudiante"
            placeholder="ID Estudiante"
            value={form.idEstudiante}
            onChange={handleChange}
            readOnly
          />
          <input
            type="number"
            name="idRole"
            placeholder="ID Rol"
            value={form.idRole}
            onChange={handleChange}
            required
          />
          <button type="submit">Actualizar Rol</button>
        </form>
      </div>
    </div>
  );
};

export default EditarRol;
