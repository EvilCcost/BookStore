using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class Sesion
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        [Required, MaxLength(500)]
        public string RefreshToken { get; set; } = null!;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime FechaExpiracion { get; set; }

        public bool Revocada { get; set; } = false;

        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty(nameof(Usuario.Sesiones))]
        public Usuario Usuario { get; set; } = null!;
    }
}
