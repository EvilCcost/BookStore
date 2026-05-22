using BookBackend.Modelos.DTOs.UsuarioDto;
using BookBackend.Servicios.UsuarioServicio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServicio _usuarioServicio;

        public UsuarioController(IUsuarioServicio usuarioServicio)
        {
            _usuarioServicio = usuarioServicio;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginUsuarioRespuestaDto>> Login([FromBody] LoginUsuarioDto logindto)
        {
            try
            {
                var result = await _usuarioServicio.LoginAsync(logindto);
                return Ok(result);
            }
            catch(UnauthorizedAccessException ex)
            {
                return Unauthorized(new { mensaje = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioRespuestaDto>>> GetAll()
        {
            return Ok(await _usuarioServicio.GetAllAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioRespuestaDto>> GetById(int id)
        {
            var usuario = await _usuarioServicio.GetByIdAsync(id);
            if (usuario is null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpGet("email/{email}")]
        public async Task<ActionResult<UsuarioRespuestaDto>> GetByEmail(string email)
        {
            var usuario = await _usuarioServicio.GetByEmailAsync(email);
            if (usuario is null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioRespuestaDto>> Create([FromBody] CrearUsuarioDto crearDto)
        {
            try
            {
                var usuario = await _usuarioServicio.CreateAsync(crearDto);
                return CreatedAtAction(nameof(GetById), new { id = usuario.Id }, usuario);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UsuarioRespuestaDto>> Update(int id, [FromBody] ActualizarUsuarioDto actualizarDto)
        {
            try
            {
                var usuario = await _usuarioServicio.UpdateAsync(id, actualizarDto);
                if (usuario is null)
                    return NotFound();
                return Ok(usuario);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensaje = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var eliminado = await _usuarioServicio.DeleteAsync(id);
            if (!eliminado)
                return NotFound();
            return NoContent();
        }

        [HttpPatch("{id:int}/estado")]
        public async Task<ActionResult> ToggleActivo(int id)
        {
            var resultado = await _usuarioServicio.ToggleActivoAsync(id);
            if (!resultado)
                return NotFound();
            return NoContent();
        }
    }
}
