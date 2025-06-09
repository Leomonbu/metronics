using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSRegistro.Data;
using WSRegistro.DTOs;
using WSRegistro.Models;
using WSRegistro.Services;

namespace WSRegistro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InscripcionsController : ControllerBase
    {
        private readonly InscripcionService  _service;

        public InscripcionsController(InscripcionService service)
        {
            _service = service;
        }

        [Authorize]
        // GET: api/Inscripciones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inscripcion>>> GetInscripciones()
        {
            return await _service.GetAllAsync();
        }

        [Authorize]
        // GET: api/Inscripciones/Semestre
        [HttpGet("Semestre")]
        public async Task<ActionResult<IEnumerable<Semestre>>> GetSemestres()
        {
            return await _service.GetSemester();
        }

        [Authorize]
        // GET: api/Inscripciones/Paginado
        [HttpGet("Paginado")]
        public async Task<IActionResult> GetInscripcionesPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page <= 0 || pageSize <= 0)
                return BadRequest(new { message = "Parámetros de paginación inválidos" });

            var (inscripciones, total) = await _service.GetPagedAsync(page, pageSize);

            var result = new
            {
                TotalItems = total,
                Page = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(total / (double)pageSize),
                Items = inscripciones.Select(e => new
                {
                    e.IdInscripcion,
                    e.IdEstudiante,
                    e.IdMateria,
                    e.IdSemestre,
                    e.EstadoInscripcion
                })
            };

            return Ok(result);
        }


        [Authorize]
        // GET: api/Inscripciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inscripcion>> GetInscripcion(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId != id)
                return Forbid("No puedes acceder a otro estudiante.");

            var inscripcion = await _service.GetByIdAsync(id);

            if (inscripcion == null)
                return NotFound();

            return Ok(new
            {
                inscripcion.IdEstudiante,
                inscripcion.IdInscripcion,
                inscripcion.IdMateria,
                inscripcion.IdSemestre,
                inscripcion.EstadoInscripcion
            });
        }

        [Authorize]
        // GET: api/Inscripciones/Inscrito/5
        [HttpGet("Inscrito/{id}")]
        public async Task<ActionResult<IEnumerable<Inscripcion>>> GetInscripcionID(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            if (userId != id)
                return Forbid("No puedes acceder a otro estudiante.");

            var inscripciones = await _service.GetAllIDAsync(id);

            if (inscripciones == null)
                return NotFound();

            return inscripciones;
        }

        [Authorize]
        // PUT: api/Inscripciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInscripcion(int id, [FromBody] InscripcionDto inscripcion, [FromServices] IValidator<InscripcionDto> validator)
        {
            var validationResult = await validator.ValidateAsync(inscripcion);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            var updated = await _service.UpdateAsync(id, inscripcion);
            if (!updated)
                return NotFound(new { message = "Estudiante no encontrado" });

            return NoContent();
        }

        // POST: api/Inscripciones
        [HttpPost]
        public async Task<ActionResult> PostInscripcion([FromBody] InscripcionDto inscripcion, [FromServices] IValidator<InscripcionDto> validator)
        {
            var validationResult = await validator.ValidateAsync(inscripcion);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
            }

            await _service.RegisterInscripAsync(inscripcion);
            return Ok(new { message = "Registro exitoso" });
        }

        [Authorize]
        // DELETE: api/Inscripciones/5
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
