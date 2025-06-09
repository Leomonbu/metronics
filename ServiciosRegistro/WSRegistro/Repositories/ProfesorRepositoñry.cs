using Microsoft.EntityFrameworkCore;
using WSRegistro.Data;
using WSRegistro.Models;

namespace WSRegistro.Repositories
{
    public interface IProfesorRepositoñry
    {
        Task<List<Profesor>> GetAllAsync();
    }


    public class ProfesorRepositoñry : IProfesorRepositoñry
    {
        private readonly AppBDContext _context;

        public ProfesorRepositoñry(AppBDContext context)
        {
            _context = context;
        }

        public async Task<List<Profesor>> GetAllAsync()
        {
            var query = _context.Profesor.AsNoTracking();
            var profesores = await query
                .OrderBy(e => e.IdProfesor)
               .ToListAsync();

            return (profesores);
        }

    }
}
