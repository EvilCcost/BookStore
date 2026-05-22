using BookBackend.Modelos.DTOs.EjemplarDto;
using BookBackend.Servicios.EjemplarServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/ejemplares")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario")]
    public class EjemplarController : ControllerBase
    {
        private readonly IEjemplarServicio _ejemplarServicio;

        public EjemplarController(IEjemplarServicio ejemplarServicio)
        {
            _ejemplarServicio = ejemplarServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<EjemplarRespuestaDto>>> GetAll()
        {
            return Ok(await _ejemplarServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EjemplarRespuestaDto>> GetById(int id)
        {
            var ejemplar = await _ejemplarServicio.GetByIdAsync(id);
            if (ejemplar is null) return NotFound();
            return Ok(ejemplar);
        }

        [HttpGet("codigo-barras/{codigo}")]
        public async Task<ActionResult<EjemplarRespuestaDto>> GetByCodigoBarras(string codigo)
        {
            var ejemplar = await _ejemplarServicio.GetByCodigoBarrasAsync(codigo);
            if (ejemplar is null) return NotFound();
            return Ok(ejemplar);
        }

        [HttpGet("por-libro/{libroId:int}")]
        public async Task<ActionResult<List<EjemplarRespuestaDto>>> GetByLibroId(int libroId)
        {
            return Ok(await _ejemplarServicio.GetByLibroIdAsync(libroId));
        }

        [HttpGet("disponibles")]
        public async Task<ActionResult<List<EjemplarRespuestaDto>>> GetDisponibles()
        {
            return Ok(await _ejemplarServicio.GetDisponiblesAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<EjemplarRespuestaDto>> Create([FromBody] CrearEjemplarDto crearDto)
        {
            try
            {
                var ejemplar = await _ejemplarServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = ejemplar.Id }, ejemplar);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<EjemplarRespuestaDto>> Update(int id, [FromBody] ActualizarEjemplarDto actualizarDto)
        {
            try
            {
                var ejemplar = await _ejemplarServicio.UpdateAsync(id, actualizarDto);
                if (ejemplar is null) return NotFound();
                return Ok(ejemplar);
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
            var eliminado = await _ejemplarServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
