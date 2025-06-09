namespace WSRegistro.DTOs
{
    public class EstudianteDto
    {
        public int TipoDocumento { get; set; }

        public long DocumentoEstudiante { get; set; }

        /// <summary>
        /// Long max 70
        /// </summary>
        public required string NombresEstudiante { get; set; }

        /// <summary>
        /// Long max 70
        /// </summary>
        public required string ApellidosEstudiante { get; set; }

        /// <summary>
        /// Long max 250
        /// </summary>
        public required string EmailEstudiante { get; set; }

        public required string Password { get; set; }

        public DateTime Fecha_nacimiento { get; set; }

        public int IdRol { get; set; }

        public int EstadoEstudiante { get; set; }
    }
}
