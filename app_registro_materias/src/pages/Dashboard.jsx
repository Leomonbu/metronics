import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { useAuth } from "../auth/AuthProvider";
import { getInfoEstudiante } from "../api/registoService";
import Breadcrumbs from "../components/Breadcrumbs";
import '../css/Dashboard.css';

const Dashboard = () => {
  const [estudiante, setEstudiante] = useState([]);
  const context = useAuth();

  const consultarInfoLogin = async () => {
    try 
    {
      const perfil = await getInfoEstudiante(context.user.email);
      setEstudiante(perfil.data);
    } catch(err) {
      console.error("Error al consultar perfil:", err);
      return null;
    }
  };

  useEffect(() => {
    consultarInfoLogin();
  }, []);


  return (
    <div className='dashboard-empty'>
      <Breadcrumbs />

      <div className="dashboard-container">
        <h2 className="dashboard-title">{estudiante.nombresEstudiante ? `${estudiante.nombresEstudiante}` : ''}, selecciona una opcion </h2>

        <br></br>

          {estudiante.idRol === 1 && (
            <div className="dashboard-buttons">
              <Link to="/crearrol" className="dashboard-button">Crear rol usuario</Link>
              <Link to="/editrol" className="dashboard-button">Editar rol usuario</Link>
              <Link to="/inscribirmateria" className="dashboard-button">Inscribir materias</Link>
              <Link to="/gestionmaterias" className="dashboard-button">Editar materias</Link>
              <Link to="/companeros" className="dashboard-button">Compañeros de clase</Link>
              <Link to="/estudiantes" className="dashboard-button">Listado estudiantes</Link>
            </div>
          )}

          {estudiante.idRol === 2 && (
            <div className="dashboard-buttons">
              <Link to="/inscribirmateria" className="dashboard-button">Inscribir materias</Link>
              <Link to="/gestionmaterias" className="dashboard-button">Editar materias</Link>
              <Link to="/companeros" className="dashboard-button">Compañeros de clase</Link>
            </div>
          )}

      </div>
    </div>
  );
};

export default Dashboard;
