using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSRegistro.DTOs;
using WSRegistro.Models;
using WSRegistro.Services;

namespace WSRegistro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly EstudianteService _service;

        public EstudiantesController(EstudianteService service)
        {
            _service = service;
        }

        [Authorize]
        // GET: api/Estudiantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiante()
        {
            return await _service.GetAllAsync();
        }

        [Authorize]
        // GET: api/Estudiantes/Paginado
        [HttpGet("Paginado")]
        public async Task<IActionResult> GetEstudiantesPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "Parámetros de paginación inválidos" });

            var (estudiantes, total) = await _service.GetPagedAsync(page, pageSize);

            var result = new
            {
                TotalItems = total,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Items = estudiantes.Select(e => new
                {
                    e.IdEstudiante,
                    e.TipoDocumento,
                    e.DocumentoEstudiante,
                    e.NombresEstudiante,
                    e.ApellidosEstudiante,
                    e.EmailEstudiante,
                })
            };

            return Ok(result);
        }

        [Authorize]
        // GET: api/Estudiantes/ComClass
        [HttpGet("ComClass")]
        public async Task<IActionResult> GetEstudiantesComClassPaged([FromQuery] int idEstudiante, [FromQuery] int idMateria, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "Parámetros de paginación inválidos" });

            var (companeros, total) = await _service.GetPagedComAsync(page, pageSize, idEstudiante, idMateria);

            var result = new
            {
                TotalItems = total,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Items = companeros.Select(e => new
                {
                    e.IdInscripcion,
                    e.NombreMateria,
                    e.NombreEstudiante
                })
            };

            return Ok(result);
        }


        [Authorize]
        // GET: api/Estudiantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId != id)
                return Forbid("No puedes acceder a otro estudiante.");

            var estudiante = await _service.GetByIdAsync(id);

            if (estudiante == null)
                return NotFound();

            return Ok(new
            {
                estudiante.IdEstudiante,
                estudiante.TipoDocumento,
                estudiante.DocumentoEstudiante,
                estudiante.NombresEstudiante,
                estudiante.ApellidosEstudiante,
                estudiante.EmailEstudiante
            });
        }

        [Authorize]
        // GET: api/Estudiantes/perfil/info@email.com
        [HttpGet("perfil/{email}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(string email)
        {
            var estudiante = await _service.StudentEmailAsync(email);

            if (estudiante == null)
                return NotFound();

            return Ok(new
            {
                estudiante.IdEstudiante,
                estudiante.TipoDocumento,
                estudiante.DocumentoEstudiante,
                estudiante.NombresEstudiante,
                estudiante.ApellidosEstudiante,
                estudiante.EmailEstudiante,
                estudiante.IdRol
            });
        }

        [Authorize]
        //GET /api/Estudiantes/companeros/5
        [HttpGet("companeros/{idMateria}")]
        public async Task<IActionResult> GetClassmates(int idMateria)
        {
            // Extraer el ID del estudiante desde el token
            var idEstudiante = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var classmates = await _service.GetClassmatesAsync(idEstudiante, idMateria);
            return Ok(classmates);
        }

        [Authorize]
        // PUT: api/Estudiantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiante(int id, [FromBody] EstudianteDto estudiante, [FromServices] IValidator<EstudianteDto> validator)
        {
            var validationResult = await validator.ValidateAsync(estudiante);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var updated = await _service.UpdateAsync(id, estudiante);
            if (!updated)
                return NotFound(new { message = "Estudiante no encontrado" });

            return NoContent();
        }
 
        // POST: api/Estudiantes
        [HttpPost]
        public async Task<ActionResult> PostEstudiante([FromBody] EstudianteDto estudiante, [FromServices] IValidator<EstudianteDto> validator)
        {
            var validationResult = await validator.ValidateAsync(estudiante);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            await _service.RegisterStudentAsync(estudiante);
            return Ok(new { message = "Registro exitoso" });
        }

        // POST: api/Estudiantes/Login
        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto estudiante)
        {
            var token = await _service.LoginAsync(estudiante);

            if (token == null)
                return Unauthorized(new { message = "Credenciales inválidas" });

            return Ok(new { token, estudiante.EmailEstudiante });
        }

        [Authorize]
        // DELETE: api/Estudiantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Estudiante no encontrado" });

            return NoContent();
        }

    }
}
