import { useState, useEffect } from "react";
import { toast } from "react-toastify";
import { getEstudiantes, getMaterias, getInscripciones } from "../api/registoService";
import { useAuth } from "../auth/AuthProvider";
import Breadcrumbs from "../components/Breadcrumbs";
import "../css/Companeros.css";

const CompanerosClase = () => {
    const context = useAuth();
    const ITEMS_PER_PAGE = 5;
    const [materias, setMaterias] = useState([]);
    const [filteredEstudiantes, setFilteredEstudiantes] = useState([]);
    const [filtro, setFiltro] = useState({ nombre: "" });
    const [paginaActual, setPaginaActual] = useState(1);

    // Obtener materias
    const fetchMaterias = async () => {
        try {
            const inscrito = await getInscripciones(context.user.nameid);
            const res = await getMaterias();

            const idMateriasInscritas = inscrito.data.map(i => i.idMateria);

            const materiasDelEstudiante = res.filter(m =>
                idMateriasInscritas.includes(m.idMateria)
            );

            setMaterias(materiasDelEstudiante);
        } catch (err) {
            toast.error('No se pudo consultar las materias');
            console.error("Error al cargar materias", err);
        }
    };

    const handleChange = (e) => {
    setFiltro({ ...filtro, [e.target.name]: e.target.value });
    consultarCompas(e.target.value === '' ? 0 : e.target.value);
    };

    const consultarCompas = async (idmateria) => {
        try {
            const response = await getEstudiantes(context.user.nameid, idmateria);
            setFilteredEstudiantes(response.data.items);
        } catch (error) {
            toast.error('Error cargando materias');
            console.error('Error cargando materias:', error);
        }

    };

    useEffect(() => {
        fetchMaterias();
    }, []);

    const totalPages = Math.ceil(filteredEstudiantes.length / ITEMS_PER_PAGE);
    const startIndex = (paginaActual - 1) * ITEMS_PER_PAGE;
    const currentPageItems = filteredEstudiantes.slice(startIndex, startIndex + ITEMS_PER_PAGE);

return (
    <div className="compa-empty">
        <Breadcrumbs />

        <div className="compa-page">
            <h2>Listado de estudiantes</h2>

            <select name="idMateria" value={filtro.idMateria} onChange={handleChange} >
                <option value="">Seleccione una materia...</option>
                {materias.map((m) => (
                <option key={m.idMateria} value={m.idMateria}> {m.nombreMateria} </option>
                ))}
            </select>

            <p></p>
            <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Materia</th>
                    <th>Estudiante</th>
                </tr>
            </thead>
            <tbody>
                {currentPageItems.length > 0 ? (
                    currentPageItems.map((estudiante) => (
                    <tr key={estudiante.idInscripcion}>
                        <td>{estudiante.idInscripcion}</td>
                        <td>{estudiante.nombreEstudiante}</td>
                        <td>{estudiante.nombreMateria}</td>
                    </tr>
                    ))
                ) : (
                <tr>
                    <td colSpan="3">No tiene compañeros para esta materia</td>
                </tr>
                )}
            </tbody>
        </table>

        {/* Paginación */}
        <div style={{ marginTop: '1rem', textAlign: 'center' }}>
            {Array.from({ length: totalPages }, (_, index) => (
                <button
                    key={index + 1}
                    className="btn success"
                    style={{ margin: '0 5px' }}
                    onClick={() => setPaginaActual(index + 1)}
                    disabled={paginaActual === index + 1}
                >
                    {index + 1}
                </button>
            ))}
        </div>
        </div>
    </div>
    );
};

export default CompanerosClase;
