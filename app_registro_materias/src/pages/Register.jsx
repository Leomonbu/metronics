import React, { useState } from 'react';
import '../css/Register.css';
import { Link } from 'react-router-dom';
import { toast } from "react-toastify";
import { postRegister } from "../api/registoService";

function Register() {
  const [errors, setErrors] = useState({});
  const [registroExitoso, setRegistroExitoso] = useState(false);

  const [formData, setFormData] = useState({
      tipoDocumento: '',
      documentoEstudiante: '',
      nombresEstudiante: '',
      apellidosEstudiante: '',
      emailEstudiante: '',
      password: '',
      fecha_nacimiento: '',
      idRol: 2,
      estadoEstudiante: 1
  });

    const tiposDocumento = [
        { value: 1, label: 'Cédula de Ciudadanía' },
        { value: 2, label: 'Tarjeta de Identidad' },
        { value: 3, label: 'Pasaporte' },
    ];

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleDateChange = (e) => {
        setFormData(prev => ({ ...prev, fecha_nacimiento: e.target.value }));
    };

    const handleReset = () => {
        setFormData({
        tipoDocumento: '',
        documentoEstudiante: '',
        nombresEstudiante: '',
        apellidosEstudiante: '',
        emailEstudiante: '',
        password: '',
        fecha_nacimiento: '',
        });
    };

  const validateForm = () => {
    const newErrors = {};

    if (!formData.tipoDocumento) newErrors.tipoDocumento = 'Selecciona un tipo de documento';
    if (!formData.documentoEstudiante || formData.documentoEstudiante <= 0 || formData.documentoEstudiante.length < 7)
      newErrors.documentoEstudiante = 'Número de documento inválido';
    if (!formData.nombresEstudiante.trim()) newErrors.nombresEstudiante = 'Nombres requeridos';
    if (!formData.apellidosEstudiante.trim()) newErrors.apellidosEstudiante = 'Apellidos requeridos';
    if (!formData.emailEstudiante.includes('@')) newErrors.emailEstudiante = 'Email inválido';
    if (!formData.password || formData.password.length < 6)
      newErrors.password = 'La contraseña debe tener al menos 6 caracteres';
    if (!formData.fecha_nacimiento) newErrors.fecha_nacimiento = 'Selecciona tu fecha de nacimiento';

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };

  const validateField = (name, value) => {
  let message = '';

  switch (name) {
    case 'tipoDocumento':
      if (!value) message = 'Selecciona un tipo de documento';
      break;
    case 'documentoEstudiante':
      if (!value || value <= 0) message = 'Número de documento inválido';
      break;
    case 'nombresEstudiante':
      if (!value.trim()) message = 'Nombres requeridos';
      break;
    case 'apellidosEstudiante':
      if (!value.trim()) message = 'Apellidos requeridos';
      break;
    case 'emailEstudiante':
      if (!value.includes('@')) message = 'Email inválido';
      break;
    case 'password':
      if (!value || value.length < 6) message = 'La contraseña debe tener al menos 6 caracteres';
      break;
    case 'fecha_nacimiento':
      if (!value) message = 'Selecciona tu fecha de nacimiento';
      break;
    default:
      break;
  }

  setErrors(prev => ({ ...prev, [name]: message }));
};

  const handleSubmit = async (e) => {
        e.preventDefault();

        if (!validateForm()) return;

        try {
        const response = await postRegister(formData);
        
        toast.success(response.message);
        setRegistroExitoso(true);
        handleReset();
      } catch (error) {
        toast.error('Hubo un problema al registrar');
        console.error(error);
      }
  };

    return (
    <div className="register-container">
    <h2>Registro de Estudiante</h2>
    <form onSubmit={handleSubmit}>
      <div className="register-form-group">
        <label>Tipo de Documento:</label>
        <select
          name="tipoDocumento"
          value={formData.tipoDocumento}
          onChange={handleChange}
          onBlur={(e) => validateField(e.target.name, e.target.value)}
          required
        >
          <option value="">-- Selecciona una opción --</option>
          {tiposDocumento.map(tipo => (
            <option key={tipo.value} value={tipo.value}>{tipo.label}</option>
          ))}
        </select>
      </div>

      <div className="register-form-group">
        <label>Número de Documento:</label>
        <input
          type="number"
          name="documentoEstudiante"
          value={formData.documentoEstudiante}
          onChange={handleChange}
          onBlur={(e) => validateField(e.target.name, e.target.value)}
          required
        />
        {errors.documentoEstudiante && <p style={{ color: 'red' }}>{errors.documentoEstudiante}</p>}
      </div>

      <div className="register-form-group">
        <label>Nombres:</label>
        <input
          type="text"
          name="nombresEstudiante"
          value={formData.nombresEstudiante}
          onChange={handleChange}
          onBlur={(e) => validateField(e.target.name, e.target.value)}
          required
        />
        {errors.nombresEstudiante && <p style={{ color: 'red' }}>{errors.nombresEstudiante}</p>}
      </div>

      <div className="register-form-group">
        <label>Apellidos:</label>
        <input
          type="text"
          name="apellidosEstudiante"
          value={formData.apellidosEstudiante}
          onChange={handleChange}
          onBlur={(e) => validateField(e.target.name, e.target.value)}
          required
        />
        {errors.apellidosEstudiante && <p style={{ color: 'red' }}>{errors.apellidosEstudiante}</p>}
      </div>

      <div className="register-form-group">
        <label>Email:</label>
        <input
          type="email"
          name="emailEstudiante"
          value={formData.emailEstudiante}
          onChange={handleChange}
          onBlur={(e) => validateField(e.target.name, e.target.value)}
          required
        />
        {errors.emailEstudiante && <p style={{ color: 'red' }}>{errors.emailEstudiante}</p>}
      </div>

      <div className="register-form-group">
        <label>Contraseña:</label>
        <input
          type="password"
          name="password"
          value={formData.password}
          onChange={handleChange}
          onBlur={(e) => validateField(e.target.name, e.target.value)}
          required
        />
      </div>

      <div className="register-form-group">
        <label>Fecha de Nacimiento:</label>
        <input
          type="date"
          name="fecha_nacimiento"
          value={formData.fecha_nacimiento}
          onChange={handleDateChange}
          onBlur={(e) => validateField(e.target.name, e.target.value)}
          required
        />
      </div>

      <div className="register-buttons">
        <button type="button" onClick={handleReset}>Limpiar</button>
        <button type="submit">Registrar</button>
      </div>
    </form>

    {registroExitoso && (
  <div style={{ marginTop: '1.5rem', textAlign: 'center' }}>
    <Link to="/login" style={{ color: '#3f51b5', textDecoration: 'underline' }}>
      Inicia sesión aquí
    </Link>
  </div>
)}

  </div>
    );
}

export default Register;
