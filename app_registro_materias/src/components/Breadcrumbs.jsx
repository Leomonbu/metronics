import { Link, useLocation } from "react-router-dom";
import "../css/Breadcrumbs.css";

const Breadcrumbs = () => {
  const location = useLocation();

  // Divide el pathname en segmentos (sin rutas vacías)
  const pathnames = location.pathname.split("/").filter((x) => x);

  const routeNameMap = {
  companeros: "Compañeros",
  editrol: "Editar_Rol",
  crearrol: "Crear_Rol",
  dashboard: "Inicio",
  inscribirmateria: "Matricular_cursos",
  gestionmaterias: "Edicion_matricula",
  roles: "Roles",
  asignar: "Asignar Rol"
};

  return (
    <nav className="breadcrumb">
      <Link to="/dashboard" className="breadcrumb-link">Inicio</Link>

      {pathnames.map((name, index) => {
        const routeTo = "/" + pathnames.slice(0, index + 1).join("/");
        const isLast = index === pathnames.length - 1;

        const displayName = routeNameMap[name] || name;

        return isLast ? (
          <span key={routeTo} className="breadcrumb-current">{decodeURIComponent(displayName)}</span>
        ) : (
          <Link key={routeTo} to={routeTo} className="breadcrumb-link">
            {decodeURIComponent(displayName)}
          </Link>
        );
      })}
    </nav>
  );
};

export default Breadcrumbs;
