using System;
using WSRegistro.Data;
using WSRegistro.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Drawing.Printing;
using System.Data;
using WSRegistro.DTOs;


namespace WSRegistro.Repositories
{
    public interface IEstudianteRepository
    {
        Task AddAsync(Estudiante student);
        Task<Estudiante?> GetByEmailAsync(string email);
        Task SaveAsync();
        Task<Estudiante> GetByIdAsync(int idEstudiante);
        Task<(List<Estudiante> Estudiantes, int TotalCount)> GetAllPagedAsync(int page, int pageSize);
        Task<(List<CompEstudianteDto> Estudiantes, int TotalCount)> GetAllPagedComClassAsync(int page, int pageSize, int idEstudiante, int idMateria);
        Task<List<Estudiante>> GetAllAsync();
        Task<List<CompEstudianteDto>> ObtenerCompanerosEstudiantesAsync(int idEstudiante, int idMateria);
        void Delete(Estudiante Estudiante);
    }

    public class EstudianteRepository : IEstudianteRepository
    {
        private readonly AppBDContext _context;

        public EstudianteRepository(AppBDContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(Estudiante student)
        {
            await _context.Estudiante.AddAsync(student);
        }

        public async Task<Estudiante?> GetByEmailAsync(string email)
        {
            return await _context.Estudiante.FirstOrDefaultAsync(s => s.EmailEstudiante == email);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Estudiante> GetByIdAsync(int idEstudiante) 
        {
            return await _context.Estudiante.FindAsync(idEstudiante);
        }

        public async Task<(List<Estudiante> Estudiantes, int TotalCount) > GetAllPagedAsync(int page, int pageSize)
        {
            var query = _context.Estudiante.AsNoTracking();

            var total = await query.CountAsync();
            var estudiantes = await query
                .OrderBy(e => e.IdEstudiante)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (estudiantes, total);
        }

        public async Task<(List<CompEstudianteDto> Estudiantes, int TotalCount)> GetAllPagedComClassAsync(int page, int pageSize, int idEstudiante, int idMateria)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SelectClassmatesUser";
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "@studentId";
            param1.Value = idEstudiante;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "@materiaId";
            param2.Value = idMateria;
            command.Parameters.Add(param2);

            var classmates = new List<CompEstudianteDto>();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                classmates.Add(new CompEstudianteDto
                {
                    IdInscripcion = reader.GetInt32(reader.GetOrdinal("IdInscripcion")),
                    NombreMateria = reader.GetString(reader.GetOrdinal("NombreMateria")),
                    NombreEstudiante = reader.GetString(reader.GetOrdinal("NombreEstudiante"))
                });
            }

            var total = classmates.Count();
            var estudiantes =  classmates
                .OrderBy(e => e.IdInscripcion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();

            return (estudiantes, total);
        }

        public async Task<List<Estudiante>> GetAllAsync()
        {
            var query = _context.Estudiante.AsNoTracking();
            var estudiantes = await query
                .OrderBy(e => e.IdEstudiante)
               .ToListAsync();

            return (estudiantes);
        }

        public async Task<List<CompEstudianteDto>> ObtenerCompanerosEstudiantesAsync(int idEstudiante, int idMateria)
        {
            var connection = _context.Database.GetDbConnection();
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SelectClassmatesUser";
            command.CommandType = CommandType.StoredProcedure;

            var param1 = command.CreateParameter();
            param1.ParameterName = "@studentId";
            param1.Value = idEstudiante;
            command.Parameters.Add(param1);

            var param2 = command.CreateParameter();
            param2.ParameterName = "@materiaId";
            param2.Value = idMateria;
            command.Parameters.Add(param2);

            var classmates = new List<CompEstudianteDto>();

            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                classmates.Add(new CompEstudianteDto
                {
                    IdInscripcion = reader.GetInt32(reader.GetOrdinal("IdInscripcion")),
                    NombreMateria = reader.GetString(reader.GetOrdinal("NombreMateria")),
                    NombreEstudiante = reader.GetString(reader.GetOrdinal("NombreEstudiante"))
                });
            }

            return classmates;
        }

        public void Delete(Estudiante Estudiante)
        {
            _context.Estudiante.Remove(Estudiante);
        }
    }
}
