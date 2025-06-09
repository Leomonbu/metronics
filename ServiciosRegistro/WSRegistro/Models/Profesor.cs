using System.ComponentModel.DataAnnotations;

namespace WSRegistro.Models
{
    public class Profesor
    {
        [Key]
        public int IdProfesor { get; set; }

        /// <summary>
        /// Long max 100
        /// </summary>
        public required string NombreProfesor { get; set; }

        /// <summary>
        /// Long max 100
        /// </summary>
        public required string ApellidosProfesor { get; set; }

        public int EstadoProfesor { get; set; }
    }
}
