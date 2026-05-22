using BookBackend.Modelos.DTOs.InspeccionDto;
using BookBackend.Servicios.InspeccionServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/inspecciones")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario")]
    public class InspeccionController : ControllerBase
    {
        private readonly IInspeccionServicio _inspeccionServicio;

        public InspeccionController(IInspeccionServicio inspeccionServicio)
        {
            _inspeccionServicio = inspeccionServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<InspeccionRespuestaDto>>> GetAll()
        {
            return Ok(await _inspeccionServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<InspeccionRespuestaDto>> GetById(int id)
        {
            var inspeccion = await _inspeccionServicio.GetByIdAsync(id);
            if (inspeccion is null) return NotFound();
            return Ok(inspeccion);
        }

        [HttpGet("por-prestamo/{prestamoId:int}")]
        public async Task<ActionResult<List<InspeccionRespuestaDto>>> GetByPrestamoId(int prestamoId)
        {
            return Ok(await _inspeccionServicio.GetByPrestamoIdAsync(prestamoId));
        }

        [HttpGet("por-ejemplar/{ejemplarId:int}")]
        public async Task<ActionResult<List<InspeccionRespuestaDto>>> GetByEjemplarId(int ejemplarId)
        {
            return Ok(await _inspeccionServicio.GetByEjemplarIdAsync(ejemplarId));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<InspeccionRespuestaDto>> Create([FromBody] CrearInspeccionDto crearDto)
        {
            try
            {
                var inspeccion = await _inspeccionServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = inspeccion.Id }, inspeccion);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<InspeccionRespuestaDto>> Update(int id, [FromBody] ActualizarInspeccionDto actualizarDto)
        {
            try
            {
                var inspeccion = await _inspeccionServicio.UpdateAsync(id, actualizarDto);
                if (inspeccion is null) return NotFound();
                return Ok(inspeccion);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _inspeccionServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
