using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class DetalleVenta
    {
        [Key]
        public int Id { get; set; }

        public int VentaId { get; set; }
        public int EjemplarId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PrecioUnitario { get; set; }

        [ForeignKey(nameof(VentaId))]
        [InverseProperty(nameof(Venta.DetalleVentas))]
        public Venta Venta { get; set; } = null!;

        [ForeignKey(nameof(EjemplarId))]
        [InverseProperty(nameof(Ejemplar.DetalleVentas))]
        public Ejemplar Ejemplar { get; set; } = null!;
    }
}
