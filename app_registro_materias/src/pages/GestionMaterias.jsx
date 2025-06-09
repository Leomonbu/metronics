import React, { useEffect, useState } from "react";
import Breadcrumbs from "../components/Breadcrumbs";
import { useAuth } from "../auth/AuthProvider";
import { getInscripciones, getMaterias, putInscripcion, deleteInscripciones } from "../api/registoService";
import { toast } from "react-toastify";
import Swal from 'sweetalert2';
import "../css/GestionMaterias.css";

const GestionMaterias = () => {
  const context = useAuth();
  const [materias, setMaterias] = useState([]);
  const [paramMaterias, setParamMaterias] = useState([]);
  const [editingMateria, setEditingMateria] = useState(null);
  const [form, setForm] = useState({ nombre: "" });
  
  // Obtener materias
  const fetchMaterias = async () => {
    try {
      const res = await getInscripciones(context.user.nameid);

      const res1 = await getMaterias();
      
      const RegistrosEncontrdos = res.data;
      const ListadoMaterias = res1;
      const inscripciones = RegistrosEncontrdos.map(ind => ({
          idinscripcion: ind.idInscripcion,
          idMateria: ind.idMateria,
          idSemestre: ind.idSemestre,
          nombreMateria: ListadoMaterias.find(x => x.idMateria === ind.idMateria).nombreMateria
      }));

      setParamMaterias(ListadoMaterias);
      setMaterias(inscripciones);
    } catch (err) {
      toast.error('No se pudo consultar las materias');
      console.error("Error al cargar materias", err);
    }
  };


  useEffect(() => {
    fetchMaterias();
  }, []);

  const openEditModal = (materia) => {
    setEditingMateria(materia);
    setForm({ idMateria: materia.idMateria });
  };

  const closeModal = () => {
    setEditingMateria(null);
    setForm({ idMateria: "" });
  };

  const MateriaValida = () => {
    let cuentaError = 0;
    const lstRelacionMateriaProf = [];
    
    //Iteracion para adicionar el IdProfesor de las materias ya inscritas
    for(const matInscrita of materias) {
      const relaciones = { idMateria: null, idProfesor: null};
      //Busco el Idprofesor de materias registradas
      const idProfbyMat = paramMaterias.find(x => x.idMateria === matInscrita.idMateria).idProfesor
      relaciones.idMateria = matInscrita.idMateria;
      relaciones.idProfesor = idProfbyMat;
      lstRelacionMateriaProf.push(relaciones);
    }

    //Adiciono el IdProfesor de la nueva materia seleccionada
    const idProfbyMatSelect = paramMaterias.find(x => x.idMateria === Number(form.idMateria)).idProfesor
    
    //Se saca el id del profesor de la fila actual
    const excluirRowSelect = lstRelacionMateriaProf.filter(z => z.idMateria !== editingMateria.idMateria);
    
    //Busco si existe el idProfesor
    const countIdProfesor = excluirRowSelect.filter(t => t.idProfesor === idProfbyMatSelect).length;

    //Busco si existe el idMateria
    const countIdMateria = excluirRowSelect.filter(t => t.idMateria === Number(form.idMateria)).length;

    if (countIdMateria > 0) {
      cuentaError += 1;
      toast.warning('La materia seleccionada ya esta matriculada');  
    } else if(countIdProfesor > 0) {
      cuentaError += 1;
      toast.warning('La materia seleccionada la dicta un docente con el que ya tiene otra materia');
    }
    
    if (cuentaError > 0)
      return false;
    else
      return true;
  };

  const handleUpdate = async () => {
      try {
        if(MateriaValida()) {
          const inscripcion = {
            IdInscripcion: Number(editingMateria.idinscripcion),
            idEstudiante: Number(context.user.nameid),
            idMateria: Number(form.idMateria),
            idSemestre: Number(editingMateria.idSemestre),
            estadoInscripcion: 1
          }

          await putInscripcion(editingMateria.idinscripcion, inscripcion);

          closeModal();
          fetchMaterias();
          toast.success('Materia actualizada exitosamente');
        }
      } catch (err) {
        toast.error('Error actualizando materia');
        console.error("Error actualizando materia", err);
      }
  };

const handleDelete = async (row) => {
  const result = await Swal.fire({
    title: 'Eliminar materia',
    text: '¿Seguro que desea eliminar esta materia?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Eliminar',
    cancelButtonText: 'Cancelar'
  });

  if (result.isConfirmed) {
    try {
      await deleteInscripciones(row.idinscripcion); // 👈 Asegúrate de usar await
      await fetchMaterias(); // ✅ Ahora se llama solo si se eliminó
    } catch (err) {
      console.error("Error eliminando materia", err);
    }
  }
};


  return (
    <div className="materias-empty">
      <Breadcrumbs />

      <div className="materias-page">
        <h2>Gestión de Materias</h2>
        <table>
          <thead>
            <tr>
              <th className="noVisible">ID</th>
              <th>Codigo</th>
              <th>Nombre</th>
              <th>Semestre</th>
              <th>Acciones</th>
            </tr>
          </thead>
          <tbody>
            {materias.map((m) => (
              <tr key={m.idMateria}>
                <td className="noVisible">{m.idinscripcion}</td>
                <td>{m.idMateria}</td>
                <td>{m.nombreMateria}</td>
                <td>{m.idSemestre}</td>
                <td>
                  <button className="btn edit" onClick={() => openEditModal(m)}>Editar</button>
                  <button className="btn delete" onClick={() => handleDelete(m)}>Eliminar</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>

  {/* Modal */}
  {editingMateria && (
  <div className="modal-overlay" onClick={closeModal}>
    <div className="modal-content" onClick={(e) => e.stopPropagation()}>
      <h3>Editar Materia</h3>
      
      <select
        value={form.idMateria}
        onChange={(e) => setForm({ ...form, idMateria: e.target.value })}
      >
        <option value="">Seleccione una materia</option>
        {paramMaterias.map((m) => (
          <option key={m.idMateria} value={m.idMateria}> {m.nombreMateria} </option>
        ))}
      </select>

      <div className="modal-buttons">
        <button className="btn success" onClick={handleUpdate}>
          Guardar
        </button>
        <button className="btn cancel" onClick={closeModal}>
          Cancelar
        </button>
      </div>
    </div>
  </div>
)}

      </div>
    </div>
  );
};

export default GestionMaterias;
