using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.UsuarioDto
{
    public class ActualizarUsuarioDto
    {
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [Required, MaxLength(150)]
        public string Email { get; set; } = null!;

        public bool Activo { get; set; } = true;

        public int[]? RolIds { get; set; }
    }
}
