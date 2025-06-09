namespace WSRegistro.DTOs
{
    public class InscripcionDto
    {
        public int IdInscripcion { get; set; }

        public int IdEstudiante { get; set; }

        public int IdMateria { get; set; }

        public int IdSemestre { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int EstadoInscripcion { get; set; }
    }
}
