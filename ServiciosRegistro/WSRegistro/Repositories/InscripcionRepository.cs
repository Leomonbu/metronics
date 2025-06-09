using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WSRegistro.Data;
using WSRegistro.Models;

namespace WSRegistro.Repositories
{
    public interface I_InscripcionRepository
    {
        Task AddAsync(Inscripcion student);
        Task<Inscripcion?> GetByIdInscripAsync(int idInscripcion);
        Task SaveAsync();
        Task<Inscripcion> GetByIdAsync(int idInscripcion);
        Task<(List<Inscripcion> Inscripciones, int TotalCount)> GetAllPagedAsync(int page, int pageSize);
        Task<List<Inscripcion>> GetAllAsync();
        Task<List<Inscripcion>> GetAllIDAsync(int idEstudiante);
        Task<List<Semestre>> GetSemester();
        void Delete(Inscripcion Estudiante);
    }


    public class InscripcionRepository : I_InscripcionRepository
    {
        private readonly AppBDContext _context;

        public InscripcionRepository(AppBDContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Inscripcion inscript)
        {
            await _context.Inscripcion.AddAsync(inscript);
        }

        public async Task<Inscripcion?> GetByIdInscripAsync(int idInscripcion)
        {
            return await _context.Inscripcion.FirstOrDefaultAsync(s => s.IdInscripcion == idInscripcion);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Inscripcion> GetByIdAsync(int idInscripcion)
        {
            return await _context.Inscripcion.FindAsync(idInscripcion);
        }

        public async Task<(List<Inscripcion> Inscripciones, int TotalCount)> GetAllPagedAsync(int page, int pageSize)
        {
            var query = _context.Inscripcion.AsNoTracking();

            var total = await query.CountAsync();
            var inscripciones = await query
                .OrderBy(e => e.IdEstudiante)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (inscripciones, total);
        }

        public async Task<List<Inscripcion>> GetAllIDAsync(int idEstudiante)
        {
            var query = _context.Inscripcion.AsNoTracking();
            var total = await query.CountAsync();
            var inscripciones = await query
                .Where(e => e.IdEstudiante == idEstudiante)
                .OrderBy(e => e.IdEstudiante)
                .ToListAsync();

            return (inscripciones);
        }

        public async Task<List<Inscripcion>> GetAllAsync()
        {
            var query = _context.Inscripcion.AsNoTracking();
            var inscripciones = await query
                .OrderBy(e => e.IdEstudiante)
               .ToListAsync();

            return (inscripciones);
        }

        public async Task<List<Semestre>> GetSemester()
        {
            var query = _context.Semestre.AsNoTracking();
            var semestre = await query
                .OrderBy(e => e.IdSemestre)
               .ToListAsync();

            return (semestre);
        }

        public void Delete(Inscripcion inscripcion)
        {
            _context.Inscripcion.Remove(inscripcion);
        }

    }
}
