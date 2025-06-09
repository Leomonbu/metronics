using System.ComponentModel.DataAnnotations;

namespace WSRegistro.Models
{
    public class CompEstudiante
    {
        [Key]
        public int? IdInscripcion { get; set; }
        public string? NombreMateria { get; set; }
        public string? NombreEstudiante { get; set; }
    }
}
