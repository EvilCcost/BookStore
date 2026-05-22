using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.UsuarioDto
{
    public class LoginUsuarioDto
    {

        [Required] public string Email { get; set; } = null!;
        [Required] public string Password { get; set; } = null!;
    }
}
