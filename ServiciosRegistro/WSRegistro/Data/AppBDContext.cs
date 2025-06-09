using Microsoft.EntityFrameworkCore;
using WSRegistro.Models;

namespace WSRegistro.Data
{
    public class AppBDContext: DbContext
    {
        public AppBDContext(DbContextOptions<AppBDContext> options): base(options)
        {
            
        }
               
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Estudiante_Roles> Estudiante_Roles { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<CompEstudiante> CompEstudiante { get; set; }
        public DbSet<Materia> Materia { get; set; }
        public DbSet<Profesor> Profesor { get; set; }
        public DbSet<Semestre> Semestre { get; set; } = default!;
        public DbSet<Inscripcion> Inscripcion { get; set; } = default!;
    }
}
