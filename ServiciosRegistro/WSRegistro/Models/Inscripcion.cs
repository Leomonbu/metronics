using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;

namespace WSRegistro.Models
{
    public class Inscripcion
    {
        [Key]
        public int IdInscripcion { get; set; }

        public int IdEstudiante { get; set; }

        public int IdMateria { get; set; }

        public int IdSemestre { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public int EstadoInscripcion { get; set; }
    }
}
