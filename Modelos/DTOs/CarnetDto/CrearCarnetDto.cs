using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.CarnetDto
{
    public class CrearCarnetDto
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required, MaxLength(20)]
        public string Codigo { get; set; } = null!;

        public DateTime FechaVencimiento { get; set; }
    }
}
