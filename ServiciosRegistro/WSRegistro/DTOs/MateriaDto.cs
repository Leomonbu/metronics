namespace WSRegistro.DTOs
{
    public class MateriaDto
    {
        public int IdMateria { get; set; }

        public required string NombreMateria { get; set; }

        public int Creditos { get; set; }

        public int EstadoMateria { get; set; }

        public int IdProfesor { get; set; }
    }
}
