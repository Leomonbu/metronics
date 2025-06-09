import './App.css';
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { AuthProvider } from "./auth/AuthProvider";
import ProtectedRoute from "./components/ProtectedRoute";
import Login from "./pages/Login";
import CompanerosClase from "./pages/CompanerosClase";
import Register from "./pages/Register";
import Dashboard from './pages/Dashboard';
import CrearRol from './pages/CrearRol';
import EditarRol from './pages/EditarRol';
import InscribirMateria from './pages/InscribirMateria';
import GestionMaterias from './pages/GestionMaterias';
import Estudiantes from './pages/Estudiantes';
import { ToastContainer } from 'react-toastify';
import Navbar from './components/Navbar';
import 'react-toastify/dist/ReactToastify.css';


function App() {
  return (
  
  <AuthProvider>
    <ToastContainer position="top-center" autoClose={4000} />
    <BrowserRouter>
      <Navbar />
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route element={<ProtectedRoute />}>
          <Route path="/login" element={<Login />} />
          <Route path="/companeros" element={<CompanerosClase />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/crearrol" element={<CrearRol />} />
          <Route path="/editrol" element={<EditarRol />} />
          <Route path="/inscribirmateria" element={<InscribirMateria />} />
          <Route path="/gestionmaterias" element={<GestionMaterias />} />
          <Route path="/estudiantes" element={<Estudiantes />} />
          
        </Route>
      </Routes>
    </BrowserRouter>
  </AuthProvider>

  );
}

export default App;
