using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSRegistro.DTOs;
using WSRegistro.Services;

namespace WSRegistro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Estudiantes_RolController : ControllerBase
    {
        private readonly Estudiante_rolServices _service;

        public Estudiantes_RolController(Estudiante_rolServices service)
        {
            _service = service;
        }

        [Authorize]
        // PUT: api/Estudiantes_Rol/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstudiantes_Rol(int id, [FromBody] Estudiante_rolDto estudiante)
        {
            var updated = await _service.UpdateRolAsync(id, estudiante);
            if (!updated)
                return NotFound(new { message = "Estudiante no configurado" });

            return Ok(new { message = "Modifico rol exitosamente" });
        }

        [Authorize]
        // POST: api/Estudiantes_Rol
        [HttpPost]
        public async Task<ActionResult> PostEstudiantes_Rol([FromBody] Estudiante_rolDto estudiante)
        {
            await _service.RegisterStudentRolAsync(estudiante);
            return Ok(new { message = "Registro rol exitosamente" });
        }

    }
}
