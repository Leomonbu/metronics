import axios from "axios";

const API = axios.create({
    baseURL: process.env.REACT_APP_API_URL
});

API.interceptors.request.use((config) => {
    const token = localStorage.getItem("token");
    if (token) config.headers.Authorization = `Bearer ${token}`;
    return config;
});

//Login
export const postLogin = async (values) => {
  const response = await API.post('/Estudiantes/login', values);
  return response.data; 
};

//Register
export const postRegister = async (formData) => {
  const response = await API.post('/Estudiantes', formData);
  return response.data;
};

//CRUD materias
export const getMaterias = async () => {
  const response = await API.get('/Materia');
  return response.data;
};

export const getSemestre = async () => {
  const response = await API.get('/Inscripcions/Semestre');
  return response;
};

export const getInscripciones = async (idEstudiante) => {
  const response = await API.get(`/Inscripcions/Inscrito/${idEstudiante}`);
  return response;
};

export const postInscripcion = async (inscripcion) => {
  const response = await API.post('/Inscripcions', inscripcion);
  return response;
};

export const putInscripcion = async (id, inscripcion) => {
  const response = await API.put(`/Inscripcions/${id}`, inscripcion);
  return response;
};

export const deleteInscripciones = async (id) => {
  const response = await API.delete(`/Inscripcions/${id}`);
  return response;
};

//Dashboard
export const getInfoEstudiante = async (email) => {
  const response = await API.get(`/Estudiantes/perfil/${email}`);
  return response;
};

//Companeros
export const getEstudiantes = async (idEstudiante, idMateria, page = 1, size = 10) => {
  const response = await API.get(`/Estudiantes/ComClass?idEstudiante=${idEstudiante}&idMateria=${idMateria}&page=${page}&pageSize=${size}`);
  return response;
}
    
export const getLstEstudiantes = async (page = 1, size = 10) => {
  const response = await API.get(`/Estudiantes/Paginado?page=${page}&pageSize=${size}`);
  return response;
}
