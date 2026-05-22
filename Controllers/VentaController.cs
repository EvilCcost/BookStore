using BookBackend.Modelos.DTOs.VentaDto;
using BookBackend.Servicios.VentaServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/ventas")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaServicio _ventaServicio;

        public VentaController(IVentaServicio ventaServicio)
        {
            _ventaServicio = ventaServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<VentaRespuestaDto>>> GetAll()
        {
            return Ok(await _ventaServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VentaRespuestaDto>> GetById(int id)
        {
            var venta = await _ventaServicio.GetByIdAsync(id);
            if (venta is null) return NotFound();
            return Ok(venta);
        }

        [HttpGet("por-usuario/{usuarioId:int}")]
        public async Task<ActionResult<List<VentaRespuestaDto>>> GetByUsuarioId(int usuarioId)
        {
            return Ok(await _ventaServicio.GetByUsuarioIdAsync(usuarioId));
        }

        [HttpGet("por-fecha")]
        public async Task<ActionResult<List<VentaRespuestaDto>>> GetByFechaRange([FromQuery] DateTime desde, [FromQuery] DateTime hasta)
        {
            return Ok(await _ventaServicio.GetByFechaRangeAsync(desde, hasta));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<VentaRespuestaDto>> Create([FromBody] CrearVentaDto crearDto)
        {
            try
            {
                var venta = await _ventaServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = venta.Id }, venta);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id:int}/cancelar")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<VentaRespuestaDto>> Cancelar(int id)
        {
            var venta = await _ventaServicio.CancelarAsync(id);
            if (venta is null) return NotFound();
            return Ok(venta);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _ventaServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
