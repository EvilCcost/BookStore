using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class Venta
    {
        [Key]
        public int Id { get; set; }

        public int? UsuarioId { get; set; }

        public DateTime FechaVenta { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        public int EstadoVentaId { get; set; }

        public int MetodoPagoId { get; set; }

        [MaxLength(50)]
        public string? Comprobante { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }

        [ForeignKey(nameof(EstadoVentaId))]
        public EstadoVenta EstadoVenta { get; set; } = null!;

        [ForeignKey(nameof(MetodoPagoId))]
        public MetodoPago MetodoPago { get; set; } = null!;

        [InverseProperty(nameof(DetalleVenta.Venta))]
        public ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();
    }
}
