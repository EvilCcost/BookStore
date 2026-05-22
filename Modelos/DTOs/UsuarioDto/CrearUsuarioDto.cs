using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.UsuarioDto
{
    public class CrearUsuarioDto
    {
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [Required, MaxLength(150)]
        public string Email { get; set; } = null!;

        [Required, MinLength(6), MaxLength(100)]
        public string Password { get; set; } = null!;

        public int[]? RolIds { get; set; }
    }
}
