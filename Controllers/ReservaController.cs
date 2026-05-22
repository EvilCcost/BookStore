using BookBackend.Modelos.DTOs.ReservaDto;
using BookBackend.Servicios.ReservaServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/reservas")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario")]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaServicio _reservaServicio;

        public ReservaController(IReservaServicio reservaServicio)
        {
            _reservaServicio = reservaServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReservaRespuestaDto>>> GetAll()
        {
            return Ok(await _reservaServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReservaRespuestaDto>> GetById(int id)
        {
            var reserva = await _reservaServicio.GetByIdAsync(id);
            if (reserva is null) return NotFound();
            return Ok(reserva);
        }

        [HttpGet("por-usuario/{usuarioId:int}")]
        public async Task<ActionResult<List<ReservaRespuestaDto>>> GetByUsuarioId(int usuarioId)
        {
            return Ok(await _reservaServicio.GetByUsuarioIdAsync(usuarioId));
        }

        [HttpGet("pendientes")]
        public async Task<ActionResult<List<ReservaRespuestaDto>>> GetPendientes()
        {
            return Ok(await _reservaServicio.GetPendientesAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<ReservaRespuestaDto>> Create([FromBody] CrearReservaDto crearDto)
        {
            try
            {
                var reserva = await _reservaServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = reserva.Id }, reserva);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id:int}/cumplir")]
        [Authorize(Roles = "Administrador,Bibliotecario")]
        public async Task<ActionResult<ReservaRespuestaDto>> Cumplir(int id)
        {
            var reserva = await _reservaServicio.CumplirAsync(id);
            if (reserva is null) return NotFound();
            return Ok(reserva);
        }

        [HttpPost("{id:int}/cancelar")]
        [Authorize(Roles = "Administrador,Cliente")]
        public async Task<ActionResult<ReservaRespuestaDto>> Cancelar(int id)
        {
            var reserva = await _reservaServicio.CancelarAsync(id);
            if (reserva is null) return NotFound();
            return Ok(reserva);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _reservaServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
