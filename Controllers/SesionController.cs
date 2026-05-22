using BookBackend.Modelos.DTOs.SesionDto;
using BookBackend.Servicios.SesionServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/sesiones")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class SesionController : ControllerBase
    {
        private readonly ISesionServicio _sesionServicio;

        public SesionController(ISesionServicio sesionServicio)
        {
            _sesionServicio = sesionServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<SesionRespuestaDto>>> GetAll()
        {
            return Ok(await _sesionServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SesionRespuestaDto>> GetById(int id)
        {
            var sesion = await _sesionServicio.GetByIdAsync(id);
            if (sesion is null) return NotFound();
            return Ok(sesion);
        }

        [HttpGet("por-usuario/{usuarioId:int}")]
        public async Task<ActionResult<List<SesionRespuestaDto>>> GetByUsuarioId(int usuarioId)
        {
            return Ok(await _sesionServicio.GetByUsuarioIdAsync(usuarioId));
        }

        [HttpPost("{id:int}/revocar")]
        public async Task<ActionResult> Revocar(int id)
        {
            var revocado = await _sesionServicio.RevocarAsync(id);
            if (!revocado) return NotFound();
            return NoContent();
        }
    }
}
