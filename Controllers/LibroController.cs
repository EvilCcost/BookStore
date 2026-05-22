using BookBackend.Modelos.DTOs.LibroDto;
using BookBackend.Servicios.LibroServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario,Cliente")]
    public class LibroController : ControllerBase
    {
        private readonly ILibroServicio _libroServicio;

        public LibroController(ILibroServicio libroServicio)
        {
            _libroServicio = libroServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<LibroRespuestaDto>>> GetAll()
        {
            return Ok(await _libroServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<LibroRespuestaDto>> GetById(int id)
        {
            var libro = await _libroServicio.GetByIdAsync(id);
            if (libro is null)
                return NotFound();
            return Ok(libro);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<List<LibroRespuestaDto>>> Search([FromQuery] string titulo)
        {
            return Ok(await _libroServicio.SearchByTituloAsync(titulo));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<LibroRespuestaDto>> Create([FromBody] CrearLibroDto crearDto)
        {
            try
            {
                var libro = await _libroServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = libro.Id }, libro);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador,Bibliotecario")]
        public async Task<ActionResult<LibroRespuestaDto>> Update(int id, [FromBody] ActualizarLibroDto actualizarDto)
        {
            try
            {
                var libro = await _libroServicio.UpdateAsync(id, actualizarDto);
                if (libro is null)
                    return NotFound();
                return Ok(libro);
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
            var eliminado = await _libroServicio.DeleteAsync(id);
            if (!eliminado)
                return NotFound();
            return NoContent();
        }
    }
}
