import { useState, useEffect } from "react";
import Breadcrumbs from "../components/Breadcrumbs";
import { useAuth } from "../auth/AuthProvider";
import { toast } from "react-toastify";
import { getMaterias, getSemestre, postInscripcion, getInscripciones } from "../api/registoService";
import "../css/InscribirMateria.css";

const InscribirMateria = () => {
  const context = useAuth();
  const [materia0, setMateria0] = useState([]);
  const [materia1, setMateria1] = useState([]);
  const [materia2, setMateria2] = useState([]);
  const [semestres, setsemestres] = useState([]);
  const [form, setForm] = useState({
    materiaId0: "",
    materiaId1: "",
    materiaId2: "",
    semestreId: ""
  });

  // Obtener materia 0 al cargar
  useEffect(() => {
    const getMaterias0 = async () => {
      try {
        const res = await getMaterias();
        setMateria0(res);
      } catch (err) {
        toast.error('No fue posible consultar la primera materia');
        console.error("Error cargando materia 0", err);
      }
    };
    getMaterias0();
  }, [context.token]);

  // Obtener materia 1 según materia 0
  useEffect(() => {
    if (!form.materiaId0) return;
    
    const getMateria1 = async () => {
      try {
        const IdProfeMateria = materia0.find(m => m.idMateria === Number(`${form.materiaId0}`)).idProfesor;
        
        const materiasFiltradas = materia0.filter(m => m.idMateria !== Number(`${form.materiaId0}`) && m.idProfesor !== IdProfeMateria);
        setMateria1(materiasFiltradas)
      } catch (err) {
        toast.error('No fue posible consultar la segunda materia');
        console.error("Error cargando materia 1", err);
      }
    };
    getMateria1();
  }, [form.materiaId0]);

  // Obtener materia 2 según materia 1
  useEffect(() => {
    if (!form.materiaId1) return;

    const getMateria2 = async () => {
      try {
        const IdProfeMateria1 = materia1.find(m => m.idMateria === Number(`${form.materiaId1}`)).idProfesor;

        const materiasFiltradas1 = materia1.filter(m => m.idMateria !== Number(`${form.materiaId1}`) && m.idProfesor !== IdProfeMateria1);
        setMateria2(materiasFiltradas1);
      } catch (err) {
        toast.error('No fue posible consultar la tercera materia');
        console.error("Error cargando materia 2", err);
      }
    };
    getMateria2();
  }, [form.materiaId1]);

  // Obtener semestres
  useEffect(() => {
    const getSemestres = async () => {
      try {
        const res = await getSemestre();
        setsemestres(res.data);
      } catch (err) {
        toast.error('No fue posible consultar los semestres');
        console.error("Error cargando semestres", err);
      }
    };
    getSemestres();
  }, [context.token]);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });

    // Reset dependientes
    if (e.target.name === "idMateria0") {
      setMateria1([]);
      setMateria2([]);
      setForm((prev) => ({ ...prev, materiaId1: "", materiaId2: "" }));
    }
    if (e.target.name === "idMateria1") {
      setMateria2([]);
      setForm((prev) => ({ ...prev, materiaId2: "" }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const materiasMatriculada = await getInscripciones(context.user.nameid);
      const materiasSeleccionadas = [form.materiaId0, form.materiaId1, form.materiaId2];

      //Adiciona las columnas a la entidad que espera el servicio
      const inscripciones = materiasSeleccionadas.map(indice => ({
        IdEstudiante: context.user.nameid,
        IdMateria: indice,
        IdSemestre: form.semestreId,
        EstadoInscripcion: 1
      }));

      const lstMateriasMat = [];
      const lstMateriasSel = [];

      //Adiciona columnas para validar insert
      for (const mat of materiasMatriculada.data) {
        const registroCompleto = { idMateria: null, idProfesor: null, creditos: null };

        const rowComplete = materia0.filter(x => x.idMateria === mat.idMateria);
        registroCompleto.idMateria = mat.idMateria;
        registroCompleto.idProfesor = rowComplete[0].idProfesor;
        registroCompleto.creditos =  rowComplete[0].creditos;
        lstMateriasMat.push(registroCompleto);
      };

      //Adiciona columnas de las materias elegidas antes de insert
      for (const sel of materiasSeleccionadas) {
        const registroCompleto = { idMateria: null, idProfesor: null, creditos: null };

        const rowComplete = materia0.filter(x => x.idMateria === Number(sel));
        registroCompleto.idMateria = Number(sel);
        registroCompleto.idProfesor = rowComplete[0].idProfesor;
        registroCompleto.creditos =  rowComplete[0].creditos;
        lstMateriasSel.push(registroCompleto);
      };

      let materiasInsert = 0;
      let materiasInscritas = lstMateriasMat.length;

      for (const inscripcion of inscripciones) {
        //Valida segun orden de insercion si cumple las normas
        const findMateria = lstMateriasMat.filter(m => m.idMateria === Number(inscripcion.IdMateria)).length;
        
        if (findMateria === 0) {
          //Busco materia para traer el profesor
          const findIdProf = lstMateriasSel.find(m => m.idMateria === Number(inscripcion.IdMateria)).idProfesor;
              
          //Busco si el profesor ya lo tiene en otra materia
          const findProf = lstMateriasMat.filter(p => p.idProfesor === findIdProf).length;

          if(findProf === 0) {
            if(materiasInscritas < 3) {
              materiasInsert += 1;
              materiasInscritas += 1;
              await postInscripcion(inscripcion);
            }
          }
        }
      };

      if(materiasInsert > 0)
        toast.success(`Se inscribieron ${materiasInsert} materia(s) exitosamente`);
      else
        toast.info("Materias no inscritas, porque no cumple con las politicas");

      setForm({ materiaId0: "", materiaId1: "", materiaId2: "", semestreId: "" });
    } catch (err) {
      console.error("Error al inscribir materia", err);
      toast.error("Ya te encuentras inscrito o hubo un error.");
    }
  };

  return (
    <div className="ins-empty">
      <Breadcrumbs />

      <div className="inscribir-form-container">
        <h2>Inscripción de Materias</h2>
        <form className="inscribir-form" onSubmit={handleSubmit}>

          <select name="materiaId0" value={form.materiaId0} onChange={handleChange} required>
            <option value="">Seleccione la primer materia</option>
            {materia0.map((c) => (
              <option key={c.idMateria} value={c.idMateria}>{c.nombreMateria}</option>
            ))}
          </select>

          <select name="materiaId1" value={form.materiaId1} onChange={handleChange} required disabled={!materia1.length}>
            <option value="">Seleccione la segunda materia</option>
            {materia1.map((m) => (
            <option key={m.idMateria} value={m.idMateria}>{m.nombreMateria}</option>
            ))}
          </select>

          <select name="materiaId2" value={form.materiaId2} onChange={handleChange} required disabled={!materia2.length}>
            <option value="">Seleccione la tercer materia</option>
            {materia2.map((g) => (
              <option key={g.idMateria} value={g.idMateria}>{g.nombreMateria}</option>
            ))}
          </select>

          <select name="semestreId" value={form.semestreId} onChange={handleChange} required>
            <option value="">Seleccione el semestre</option>
            {semestres.map((s) => (
              <option key={s.idSemestre} value={s.idSemestre}>{s.nombreSemestre}</option>
            ))}
          </select>

          <button type="submit">Inscribirse</button>

        </form>
      </div>
    </div>
  );
};

export default InscribirMateria;
