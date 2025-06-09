using Microsoft.EntityFrameworkCore;
using WSRegistro.Data;
using WSRegistro.Models;

namespace WSRegistro.Repositories
{
    public interface IMateriaRepository
    {
        Task<List<Materia>> GetAllAsync();
    }


    public class MateriaRepository : IMateriaRepository
    {
        private readonly AppBDContext _context;

        public MateriaRepository(AppBDContext context)
        {
            _context = context;
        }

        public async Task<List<Materia>> GetAllAsync()
        {
            var query = _context.Materia.AsNoTracking();
            var materias = await query
                .OrderBy(e => e.IdProfesor)
               .ToListAsync();

            return (materias);
        }
    }
}
