using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.InspeccionDto
{
    public class ActualizarInspeccionDto
    {
        public int? TipoInspeccionId { get; set; }

        [MaxLength(200)]
        public string? CondicionRegistrada { get; set; }

        [MaxLength(500)]
        public string? Notas { get; set; }
    }
}
