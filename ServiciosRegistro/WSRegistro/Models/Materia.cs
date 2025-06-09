using System.ComponentModel.DataAnnotations;

namespace WSRegistro.Models
{
    public class Materia
    {
        [Key]
        public int IdMateria { get; set; }

        /// <summary>
        /// Long max 70
        /// </summary>
        public required string NombreMateria { get; set; }

        public int Creditos { get; set; }

        public int EstadoMateria { get; set; }

        public int IdProfesor { get; set; }
    }
}
