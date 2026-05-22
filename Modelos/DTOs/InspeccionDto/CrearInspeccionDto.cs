using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.InspeccionDto
{
    public class CrearInspeccionDto
    {
        [Required]
        public int PrestamoId { get; set; }

        [Required]
        public int EjemplarId { get; set; }

        public int? InspeccionadoPorUsuarioId { get; set; }

        public int TipoInspeccionId { get; set; } = 1;

        [Required, MaxLength(200)]
        public string CondicionRegistrada { get; set; } = null!;

        [MaxLength(500)]
        public string? Notas { get; set; }
    }
}
