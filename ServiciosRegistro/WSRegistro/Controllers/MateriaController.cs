using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSRegistro.Models;
using WSRegistro.Services;

namespace WSRegistro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaController : ControllerBase
    {
        private readonly MateriaService _service;

        public MateriaController(MateriaService service)
        {
            _service = service;
        }

        [Authorize]
        // GET: api/Materia
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Materia>>> GetMaterias()
        {
            return await _service.GetAllAsync();
        }
    }
}
