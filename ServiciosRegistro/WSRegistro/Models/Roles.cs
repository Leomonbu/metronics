using System.ComponentModel.DataAnnotations;

namespace WSRegistro.Models
{
    public class Roles
    {
        [Key]
        public int IdRol { get; set; }

        /// <summary>
        /// Longitud max 50
        /// </summary>
        public required string NameRol { get; set; }

        /// <summary>
        /// Varchar max
        /// </summary>
        public required string DescriptionRol { get; set; }
    }
}
