using Microsoft.AspNetCore.Identity;
using WSRegistro.DTOs;
using WSRegistro.Models;
using WSRegistro.Repositories;

namespace WSRegistro.Services
{
    public class InscripcionService
    {
        private readonly I_InscripcionRepository _repo;
        
        public InscripcionService(I_InscripcionRepository repo)
        {
            _repo = repo;
        }

        public async Task RegisterInscripAsync(InscripcionDto dto)
        {
            if (await _repo.GetByIdInscripAsync(dto.IdInscripcion) is not null)
                throw new Exception("La inscripcion ya está registrada.");

            var inscript = new Inscripcion
            {
                IdInscripcion = dto.IdInscripcion,
                IdEstudiante = dto.IdEstudiante,
                IdMateria = dto.IdMateria,
                IdSemestre = dto.IdSemestre,
                EstadoInscripcion = dto.EstadoInscripcion
            };

            await _repo.AddAsync(inscript);
            await _repo.SaveAsync();
        }

        public async Task<Inscripcion?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<(List<Inscripcion> Estudiantes, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            return await _repo.GetAllPagedAsync(page, pageSize);
        }

        public async Task<List<Inscripcion>> GetAllIDAsync(int idEstudiante)
        {
            return await _repo.GetAllIDAsync(idEstudiante);
        }

        public async Task<List<Inscripcion>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<List<Semestre>> GetSemester()
        {
            return await _repo.GetSemester();
        }

        public async Task<bool> UpdateAsync(int id, InscripcionDto dto)
        {
            var inscripcion = await _repo.GetByIdAsync(id);
            if (inscripcion == null) return false;

            inscripcion.IdEstudiante = dto.IdEstudiante;
            inscripcion.IdMateria = dto.IdMateria;
            inscripcion.IdSemestre = dto.IdSemestre;
            inscripcion.EstadoInscripcion = dto.EstadoInscripcion;
            inscripcion.FechaModificacion = DateTime.Now;

            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inscripcion = await _repo.GetByIdAsync(id);
            if (inscripcion == null) return false;

            _repo.Delete(inscripcion);
            await _repo.SaveAsync();
            return true;
        }

    }
}
