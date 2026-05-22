using BookBackend.Modelos.DTOs.CarnetDto;
using BookBackend.Servicios.CarnetServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/carnets")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario")]
    public class CarnetController : ControllerBase
    {
        private readonly ICarnetServicio _carnetServicio;

        public CarnetController(ICarnetServicio carnetServicio)
        {
            _carnetServicio = carnetServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarnetRespuestaDto>>> GetAll()
        {
            return Ok(await _carnetServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarnetRespuestaDto>> GetById(int id)
        {
            var carnet = await _carnetServicio.GetByIdAsync(id);
            if (carnet is null) return NotFound();
            return Ok(carnet);
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<CarnetRespuestaDto>> GetByCodigo(string codigo)
        {
            var carnet = await _carnetServicio.GetByCodigoAsync(codigo);
            if (carnet is null) return NotFound();
            return Ok(carnet);
        }

        [HttpGet("por-usuario/{usuarioId:int}")]
        public async Task<ActionResult<CarnetRespuestaDto>> GetByUsuarioId(int usuarioId)
        {
            var carnet = await _carnetServicio.GetByUsuarioIdAsync(usuarioId);
            if (carnet is null) return NotFound();
            return Ok(carnet);
        }

        [HttpGet("activos")]
        public async Task<ActionResult<List<CarnetRespuestaDto>>> GetActivos()
        {
            return Ok(await _carnetServicio.GetActivosAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CarnetRespuestaDto>> Create([FromBody] CrearCarnetDto crearDto)
        {
            try
            {
                var carnet = await _carnetServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = carnet.Id }, carnet);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPost("{id:int}/renovar")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CarnetRespuestaDto>> Renovar(int id, [FromBody] DateTime nuevaFechaVencimiento)
        {
            var carnet = await _carnetServicio.RenovarAsync(id, nuevaFechaVencimiento);
            if (carnet is null) return NotFound();
            return Ok(carnet);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CarnetRespuestaDto>> Update(int id, [FromBody] ActualizarCarnetDto actualizarDto)
        {
            try
            {
                var carnet = await _carnetServicio.UpdateAsync(id, actualizarDto);
                if (carnet is null) return NotFound();
                return Ok(carnet);
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
            var eliminado = await _carnetServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
