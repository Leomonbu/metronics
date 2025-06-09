using Microsoft.AspNetCore.Http.HttpResults;
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
    public class Estudiante_rolServices
    {
        private readonly IEstudiante_RolRepository _repo;
        private readonly IEstudianteRepository _repoStudent;
        private readonly IConfiguration _config;

        public Estudiante_rolServices(IEstudiante_RolRepository repo, IConfiguration config, IEstudianteRepository repoStudent)
        {
            _repo = repo;
            _config = config;
            _repoStudent = repoStudent;
        }

        public async Task<bool> RegisterStudentRolAsync(Estudiante_rolDto dto)
        {
            if (await _repo.GetByIdRolAsync(dto.IdEstudiante) is not null)
                throw new Exception("El estudiante ya está registrado.");

            var student = new Estudiante_Roles
            {
                IdEstudiante = dto.IdEstudiante,
                IdRole = dto.IdRole
            };

            var estudiante = await _repoStudent.GetByIdAsync(dto.IdEstudiante);
            if (estudiante != null)
            {
                estudiante.IdRol = dto.IdRole;
            }

            await _repo.AddRolAsync(student);
            await _repo.SaveConfAsync();
            await _repoStudent.SaveAsync();
            return true;
        }

        public async Task<bool> UpdateRolAsync(int id, Estudiante_rolDto dto)
        {
            var estudiante_rol = await _repo.GetByIdRolAsync(id);
            if (estudiante_rol == null) return false;

            var estudiante = await _repoStudent.GetByIdAsync(dto.IdEstudiante);
            if (estudiante != null)
            {
                estudiante.IdRol = dto.IdRole;
            }

            estudiante_rol.IdRole = dto.IdRole;
            
            await _repo.SaveConfAsync();
            await _repoStudent.SaveAsync();
            return true;
        }

    }
}
