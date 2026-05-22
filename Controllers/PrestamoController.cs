using BookBackend.Modelos.DTOs.PrestamoDto;
using BookBackend.Servicios.PrestamoServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/prestamos")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario")]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoServicio _prestamoServicio;

        public PrestamoController(IPrestamoServicio prestamoServicio)
        {
            _prestamoServicio = prestamoServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<PrestamoRespuestaDto>>> GetAll()
        {
            return Ok(await _prestamoServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrestamoRespuestaDto>> GetById(int id)
        {
            var prestamo = await _prestamoServicio.GetByIdAsync(id);
            if (prestamo is null) return NotFound();
            return Ok(prestamo);
        }

        [HttpGet("por-usuario/{usuarioId:int}")]
        public async Task<ActionResult<List<PrestamoRespuestaDto>>> GetByUsuarioId(int usuarioId)
        {
            return Ok(await _prestamoServicio.GetByUsuarioIdAsync(usuarioId));
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<PrestamoRespuestaDto>>> GetActivos()
        {
            return Ok(await _prestamoServicio.GetActivosAsync());
        }

        [HttpGet("vencidos")]
        public async Task<ActionResult<List<PrestamoRespuestaDto>>> GetVencidos()
        {
            return Ok(await _prestamoServicio.GetVencidosAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PrestamoRespuestaDto>> Create([FromBody] CrearPrestamoDto crearDto)
        {
            try
            {
                var prestamo = await _prestamoServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = prestamo.Id }, prestamo);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id:int}/devolver")]
        [Authorize(Roles = "Administrador,Bibliotecario")]
        public async Task<ActionResult<PrestamoRespuestaDto>> Devolver(int id, [FromBody] DateTime fechaDevolucion)
        {
            var prestamo = await _prestamoServicio.DevolverAsync(id, fechaDevolucion);
            if (prestamo is null) return NotFound();
            return Ok(prestamo);
        }

        [HttpPost("{id:int}/cancelar")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<PrestamoRespuestaDto>> Cancelar(int id)
        {
            var prestamo = await _prestamoServicio.CancelarAsync(id);
            if (prestamo is null) return NotFound();
            return Ok(prestamo);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _prestamoServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
