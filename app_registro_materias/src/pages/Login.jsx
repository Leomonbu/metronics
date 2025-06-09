import { useFormik } from "formik";
import * as Yup from "yup";
import { useAuth } from "../auth/AuthProvider";
import { toast } from "react-toastify";
import '../css/Login.css';
import { Link, useNavigate } from 'react-router-dom';
import { postLogin } from "../api/registoService";

const Login = () => {
    const { login } = useAuth();
    const navigate = useNavigate();
    const formik = useFormik({
    initialValues: {
        emailEstudiante: '',
        password: ''
    },
    validationSchema: Yup.object({
        emailEstudiante: Yup.string()
        .email('El formato del correo no es válido')
        .required('El correo es obligatorio'),
        password: Yup.string()
        .min(6, 'La contraseña debe tener al menos 6 caracteres')
        .required('La contraseña es obligatoria'),
    }),
    onSubmit: async (values) => {
    try {
        const res = await postLogin(values);
        
        login(res.token);
        navigate('/dashboard');
    } catch (err) {
        if (err.response && err.response.status === 401) {
            toast.error("Correo o contraseña incorrectos.");
        } else if (err.response && err.response.status === 400) {
            toast.error("Datos inválidos enviados.");
        } else {
        toast.error("Error en el servidor.");
        }
    }
    },
});

return (
    <div className="login-container">
    <h2>Iniciar Sesión</h2>
    <form onSubmit={formik.handleSubmit}>
        <div className="login-form-group">
            <label className="form-label">Correo electrónico</label>
            <input
                name="emailEstudiante"
                type="email"
            className={`form-control ${formik.touched.emailEstudiante && formik.errors.emailEstudiante ? 'is-invalid' : ''}`}
            {...formik.getFieldProps("emailEstudiante")}
        />
        {formik.touched.emailEstudiante && formik.errors.emailEstudiante && (
            <div className="invalid-feedback">{formik.errors.emailEstudiante}</div>
        )}
        </div>

        <div className="login-form-group">
            <label className="form-label">Contraseña</label>
            <input
                name="password"
                type="password"
                className={`form-control ${formik.touched.password && formik.errors.password ? 'is-invalid' : ''}`}
            {...formik.getFieldProps("password")}
        />
        {formik.touched.password && formik.errors.password && (
            <div className="invalid-feedback">{formik.errors.password}</div>
        )}
        </div>

        <button type="submit" className="login-button">Ingresar</button>
    </form>
    <div className="login-register-link">
        <hr/>
        <p>¿No tienes una cuenta? &nbsp;&nbsp;<Link to="/register">Regístrate aquí</Link></p>
    </div>
    </div>
    );
};

export default Login;