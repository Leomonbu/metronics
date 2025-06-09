import React from 'react';
import { useAuth } from '../auth/AuthProvider';
import { useNavigate } from 'react-router-dom';

const Navbar = () => {
    const { logout, user } = useAuth();
    const navigate = useNavigate();

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

return (
    <nav className="navbar navbar-expand navbar-light bg-light">
        <div className="container-fluid">

            {user ? (
                <span className="navbar-text me-auto">
                    Gestion registro de materias{user["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]}
                </span>
            ) : (
                <span className="navbar-text me-auto">Para realizar su registro ingrese sus credenciales de acceso</span>
                )}
            {user && (
            <button onClick={handleLogout} className="btn btn-outline-danger">
            Cerrar sesión
            </button>
            )}
            
        </div>
    </nav>
    );
};

export default Navbar;