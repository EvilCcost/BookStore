using BookBackend.Modelos.DTOs.AutorDto;
using BookBackend.Servicios.AutorServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/autores")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorServicio _autorServicio;

        public AutorController(IAutorServicio autorServicio)
        {
            _autorServicio = autorServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<AutorRespuestaDto>>> GetAll()
        {
            return Ok(await _autorServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AutorRespuestaDto>> GetById(int id)
        {
            var autor = await _autorServicio.GetByIdAsync(id);
            if (autor is null) return NotFound();
            return Ok(autor);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<List<AutorRespuestaDto>>> Search([FromQuery] string nombre)
        {
            return Ok(await _autorServicio.SearchByNombreAsync(nombre));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<AutorRespuestaDto>> Create([FromBody] CrearAutorDto crearDto)
        {
            try
            {
                var autor = await _autorServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = autor.Id }, autor);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<AutorRespuestaDto>> Update(int id, [FromBody] ActualizarAutorDto actualizarDto)
        {
            try
            {
                var autor = await _autorServicio.UpdateAsync(id, actualizarDto);
                if (autor is null) return NotFound();
                return Ok(autor);
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
            var eliminado = await _autorServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
