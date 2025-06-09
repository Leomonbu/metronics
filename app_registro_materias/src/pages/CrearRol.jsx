import { useState } from "react";
import Breadcrumbs from "../components/Breadcrumbs";
import axios from "axios";
import { useAuth } from "../auth/AuthProvider";
import { toast } from "react-toastify";
import "../css/RolForm.css";

const CrearRol = () => {
  const [form, setForm] = useState({ idEstudiante: "", IdRole: "" });
  const context = useAuth();

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const token = context.token;
      await axios.post("http://localhost:5148/api/Estudiantes_Rol", form, {
        headers: {
            Authorization: `Bearer ${token}`
          }
      });
      toast.success("Rol asignado exitosamente");
      setForm({ idEstudiante: "", IdRole: "" });
    } catch (error) {
      console.error("Error al crear rol:", error);
      toast.error("Hubo un error al asignar el rol.");
    }
  };

  return (
    <div className="rol-empty">
      <Breadcrumbs />

      <div className="rol-form-container">
        <h2>Asignar Rol</h2>
        <form className="rol-form" onSubmit={handleSubmit}>
          <input
            type="number"
            name="idEstudiante"
            placeholder="ID Estudiante"
            value={form.idEstudiante}
            onChange={handleChange}
            required
          />
          <input
            type="number"
            name="IdRole"
            placeholder="ID Rol"
            value={form.IdRole}
            onChange={handleChange}
            required
          />
          <button type="submit">Asignar Rol</button>
        </form>
      </div>
    </div>
  );
};

export default CrearRol;