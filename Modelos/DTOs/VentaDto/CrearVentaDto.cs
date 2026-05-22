using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.VentaDto
{
    public class CrearVentaDetalleDto
    {
        [Required]
        public int EjemplarId { get; set; }

        [Required]
        public decimal PrecioUnitario { get; set; }
    }

    public class CrearVentaDto
    {
        public int? UsuarioId { get; set; }

        public int MetodoPagoId { get; set; } = 1;

        [MaxLength(50)]
        public string? Comprobante { get; set; }

        [Required, MinLength(1)]
        public List<CrearVentaDetalleDto> Detalles { get; set; } = new();
    }
}
