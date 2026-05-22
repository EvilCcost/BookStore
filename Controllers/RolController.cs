using BookBackend.Modelos.DTOs.RolDto;
using BookBackend.Servicios.RolServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/roles")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class RolController : ControllerBase
    {
        private readonly IRolServicio _rolServicio;

        public RolController(IRolServicio rolServicio)
        {
            _rolServicio = rolServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<RolRespuestaDto>>> GetAll()
        {
            return Ok(await _rolServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RolRespuestaDto>> GetById(int id)
        {
            var rol = await _rolServicio.GetByIdAsync(id);
            if (rol is null) return NotFound();
            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<RolRespuestaDto>> Create([FromBody] CrearRolDto crearDto)
        {
            try
            {
                var rol = await _rolServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = rol.Id }, rol);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<RolRespuestaDto>> Update(int id, [FromBody] ActualizarRolDto actualizarDto)
        {
            try
            {
                var rol = await _rolServicio.UpdateAsync(id, actualizarDto);
                if (rol is null) return NotFound();
                return Ok(rol);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _rolServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
