using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.MultaDto
{
    public class CrearMultaDto
    {
        public int? UsuarioId { get; set; }
        public int? PrestamoId { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required, MaxLength(100)]
        public string Motivo { get; set; } = null!;
    }
}
