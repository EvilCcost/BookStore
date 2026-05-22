using BookBackend.Modelos.DTOs.MultaDto;
using BookBackend.Servicios.MultaServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/multas")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario")]
    public class MultaController : ControllerBase
    {
        private readonly IMultaServicio _multaServicio;

        public MultaController(IMultaServicio multaServicio)
        {
            _multaServicio = multaServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<MultaRespuestaDto>>> GetAll()
        {
            return Ok(await _multaServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MultaRespuestaDto>> GetById(int id)
        {
            var multa = await _multaServicio.GetByIdAsync(id);
            if (multa is null) return NotFound();
            return Ok(multa);
        }

        [HttpGet("por-usuario/{usuarioId:int}")]
        public async Task<ActionResult<List<MultaRespuestaDto>>> GetByUsuarioId(int usuarioId)
        {
            return Ok(await _multaServicio.GetByUsuarioIdAsync(usuarioId));
        }

        [HttpGet("pendientes")]
        public async Task<ActionResult<List<MultaRespuestaDto>>> GetPendientes()
        {
            return Ok(await _multaServicio.GetPendientesAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<MultaRespuestaDto>> Create([FromBody] CrearMultaDto crearDto)
        {
            try
            {
                var multa = await _multaServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = multa.Id }, multa);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id:int}/pagar")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<MultaRespuestaDto>> Pagar(int id)
        {
            var multa = await _multaServicio.PagarAsync(id);
            if (multa is null) return NotFound();
            return Ok(multa);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _multaServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
