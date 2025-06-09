using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WSRegistro.DTOs;
using WSRegistro.Models;
using WSRegistro.Repositories;

namespace WSRegistro.Services
{
    public class EstudianteService
    {
        private readonly IEstudianteRepository _repo;
        private readonly IPasswordHasher<Estudiante> _hasher;
        private readonly JwtService _jwtService;

        public EstudianteService(IEstudianteRepository repo, IPasswordHasher<Estudiante> hasher, JwtService jwtService)
        {
            _repo = repo;
            _hasher = hasher;
            _jwtService = jwtService;
        }

        public async Task RegisterStudentAsync(EstudianteDto dto)
        {
            if (await _repo.GetByEmailAsync(dto.EmailEstudiante) is not null)
                throw new Exception("El correo ya está registrado.");

            var student = new Estudiante
            {
                TipoDocumento = dto.TipoDocumento,
                DocumentoEstudiante = dto.DocumentoEstudiante,
                NombresEstudiante = dto.NombresEstudiante,
                ApellidosEstudiante = dto.ApellidosEstudiante,
                EmailEstudiante = dto.EmailEstudiante,
                Password_Hash = dto.Password,
                Fecha_nacimiento = dto.Fecha_nacimiento,
                IdRol = dto.IdRol,
                EstadoEstudiante = dto.EstadoEstudiante
            };

            student.Password_Hash = _hasher.HashPassword(student, dto.Password);

            await _repo.AddAsync(student);
            await _repo.SaveAsync();
        }

        public async Task<Estudiante?> StudentEmailAsync(string email)
        {
            var estudianteFound = await _repo.GetByEmailAsync(email);

            var studentInf = new Estudiante
            {
                IdEstudiante = estudianteFound.IdEstudiante,
                TipoDocumento = estudianteFound.TipoDocumento,
                DocumentoEstudiante = estudianteFound.DocumentoEstudiante,
                NombresEstudiante = estudianteFound.NombresEstudiante,
                ApellidosEstudiante = estudianteFound.ApellidosEstudiante,
                EmailEstudiante = email,
                Password_Hash = "",
                Fecha_nacimiento = DateTime.Now,
                IdRol = estudianteFound.IdRol,
                EstadoEstudiante = estudianteFound.EstadoEstudiante
            };

            return studentInf;
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var estudiante = await _repo.GetByEmailAsync(dto.EmailEstudiante);

            if (estudiante == null) return null;

            var result = _hasher.VerifyHashedPassword(estudiante, estudiante.Password_Hash, dto.Password);
            if (result == PasswordVerificationResult.Failed) return null;

            return _jwtService.GenerateToken(estudiante);
        }

        public async Task<Estudiante?> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<(List<Estudiante> Estudiantes, int TotalCount)> GetPagedAsync(int page, int pageSize)
        {
            return await _repo.GetAllPagedAsync(page, pageSize);
        }

        public async Task<(List<CompEstudianteDto> Estudiantes, int TotalCount)> GetPagedComAsync(int page, int pageSize, int idEstudiante, int idMateria)
        {
            return await _repo.GetAllPagedComClassAsync(page, pageSize, idEstudiante, idMateria);
        }

        public async Task<List<Estudiante>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<List<CompEstudianteDto>> GetClassmatesAsync(int idEstudiante, int idMateria)
        {
            return await _repo.ObtenerCompanerosEstudiantesAsync(idEstudiante, idMateria);
        }

        public async Task<bool> UpdateAsync(int id, EstudianteDto dto)
        {
            var estudiante = await _repo.GetByIdAsync(id);
            if (estudiante == null) return false;

            estudiante.NombresEstudiante = dto.NombresEstudiante;
            estudiante.ApellidosEstudiante = dto.ApellidosEstudiante;
            estudiante.EmailEstudiante = dto.EmailEstudiante;

            await _repo.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var estudiante = await _repo.GetByIdAsync(id);
            if (estudiante == null) return false;

            _repo.Delete(estudiante);
            return true;
        }

    }
}
