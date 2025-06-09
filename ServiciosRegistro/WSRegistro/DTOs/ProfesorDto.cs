namespace WSRegistro.DTOs
{
    public class ProfesorDto
    {
        public int IdProfesor { get; set; }

        public required string NombreProfesor { get; set; }
                
        public required string ApellidosProfesor { get; set; }

        public int EstadoProfesor { get; set; }
    }
}
