using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.CarnetDto
{
    public class ActualizarCarnetDto
    {
        [MaxLength(20)]
        public string? Codigo { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public bool? Activo { get; set; }
    }
}
