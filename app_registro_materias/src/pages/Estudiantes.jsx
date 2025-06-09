import { useState, useEffect } from "react";
import { toast } from "react-toastify";
import { getLstEstudiantes } from "../api/registoService";
import Breadcrumbs from "../components/Breadcrumbs";
import "../css/Companeros.css";

const Estudiantes = () => {
    const ITEMS_PER_PAGE = 5;
    const [filteredEstudiantes, setFilteredEstudiantes] = useState([]);
    const [paginaActual, setPaginaActual] = useState(1);


    const consultarEstudiantes = async () => {
        try {
            const response = await getLstEstudiantes();
            
            setFilteredEstudiantes(response.data.items);
        } catch (error) {
            toast.error('Error cargando materias');
            console.error('Error cargando materias:', error);
        }
    };

    useEffect(() => {
        consultarEstudiantes();
    }, []);

    const totalPages = Math.ceil(filteredEstudiantes.length / ITEMS_PER_PAGE);
    const startIndex = (paginaActual - 1) * ITEMS_PER_PAGE;
    const currentPageItems = filteredEstudiantes.slice(startIndex, startIndex + ITEMS_PER_PAGE);


return (
    <div className="compa-empty">
        <Breadcrumbs />

        <div className="compa-page">
            <h2>Listado de estudiantes</h2>

            <p></p>
            <table>
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Documento</th>
                    <th>Nombre</th>
                    <th>Apellido</th>
                    <th>Correo</th>
                </tr>
            </thead>
            <tbody>
                {currentPageItems.length > 0 ? (
                    currentPageItems.map((estudiante) => (
                    <tr key={estudiante.idEstudiante}>
                        <td>{estudiante.idEstudiante}</td>
                        <td>{estudiante.documentoEstudiante}</td>
                        <td>{estudiante.nombresEstudiante}</td>
                        <td>{estudiante.apellidosEstudiante}</td>
                        <td>{estudiante.emailEstudiante}</td>
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

export default Estudiantes;
