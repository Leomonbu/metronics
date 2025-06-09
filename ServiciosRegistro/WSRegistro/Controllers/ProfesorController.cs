using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSRegistro.Models;
using WSRegistro.Services;

namespace WSRegistro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private readonly ProfesorService _service;

        public ProfesorController(ProfesorService service)
        {
            _service = service;
        }

        [Authorize]
        // GET: api/Profesor
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesores()
        {
            return await _service.GetAllAsync();
        }
    }
}
