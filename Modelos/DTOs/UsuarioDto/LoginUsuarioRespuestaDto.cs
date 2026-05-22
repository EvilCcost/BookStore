using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.UsuarioDto
{
    public class LoginUsuarioRespuestaDto
    {
        [Required]
        public string Token { get; set; } = null!;
        [Required]
        public UsuarioRespuestaDto Usuario { get; set; } = null!;
        [Required]
        public DateTime Expiracion { get; set; }

    }
}
