using System.ComponentModel.DataAnnotations;

namespace WSRegistro.Models
{
    public class Estudiante_Roles
    {
        [Key]
        public int IdEstudiante { get; set; }

        public int IdRole { get; set; }
    }
}
