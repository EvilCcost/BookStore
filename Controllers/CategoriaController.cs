using BookBackend.Modelos.DTOs.CategoriaDto;
using BookBackend.Servicios.CategoriaServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bibliotecario")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaServicio _categoriaServicio;

        public CategoriaController(ICategoriaServicio categoriaServicio)
        {
            _categoriaServicio = categoriaServicio;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoriaRespuestaDto>>> GetAll()
        {
            return Ok(await _categoriaServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoriaRespuestaDto>> GetById(int id)
        {
            var categoria = await _categoriaServicio.GetByIdAsync(id);
            if (categoria is null) return NotFound();
            return Ok(categoria);
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<List<CategoriaRespuestaDto>>> Search([FromQuery] string nombre)
        {
            return Ok(await _categoriaServicio.SearchByNombreAsync(nombre));
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CategoriaRespuestaDto>> Create([FromBody] CrearCategoriaDto crearDto)
        {
            try
            {
                var categoria = await _categoriaServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CategoriaRespuestaDto>> Update(int id, [FromBody] ActualizarCategoriaDto actualizarDto)
        {
            try
            {
                var categoria = await _categoriaServicio.UpdateAsync(id, actualizarDto);
                if (categoria is null) return NotFound();
                return Ok(categoria);
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
            var eliminado = await _categoriaServicio.DeleteAsync(id);
            if (!eliminado) return NotFound();
            return NoContent();
        }
    }
}
