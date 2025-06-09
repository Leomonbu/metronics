using System.ComponentModel.DataAnnotations;

namespace WSRegistro.Models
{
    public class Semestre
    {
        [Key]
        public int IdSemestre { get; set; }

        /// <summary>
        /// Long max 70, Ej: "2025-I", "2025-II"
        /// </summary>
        public required string NombreSemestre { get; set; }

        public DateTime FechaInicial { get; set; }

        public DateTime FechaFinal { get; set; }
    }
}
