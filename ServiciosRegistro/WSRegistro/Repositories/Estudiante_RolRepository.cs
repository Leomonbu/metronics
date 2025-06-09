using Microsoft.EntityFrameworkCore;
using WSRegistro.Data;
using WSRegistro.Models;

namespace WSRegistro.Repositories
{
    public interface IEstudiante_RolRepository
    {
        Task AddRolAsync(Estudiante_Roles student);
        Task<Estudiante_Roles> GetByIdRolAsync(int idEstudiante);
        Task SaveConfAsync();
    }

    public class Estudiante_RolRepository : IEstudiante_RolRepository
    {
        private readonly AppBDContext _context;

        public Estudiante_RolRepository(AppBDContext context)
        {
            _context = context;
        }

        public async Task SaveConfAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task AddRolAsync(Estudiante_Roles student)
        {
            await _context.Estudiante_Roles.AddAsync(student);
        }

        public async Task<Estudiante_Roles> GetByIdRolAsync(int idEstudiante)
        {
            return await _context.Estudiante_Roles.FindAsync(idEstudiante);
        }
    }
}
