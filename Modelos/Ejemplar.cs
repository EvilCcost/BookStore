using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    [Index(nameof(CodigoBarras), IsUnique = true)]
    public class Ejemplar
    {
        [Key]
        public int Id { get; set; }

        public int LibroId { get; set; }

        [Required, MaxLength(30)]
        public string CodigoBarras { get; set; } = null!;

        [MaxLength(50)]
        public string? UbicacionEstante { get; set; }

        public int EstadoEjemplarId { get; set; }

        public int? CondicionEjemplarId { get; set; }

        [ForeignKey(nameof(LibroId))]
        [InverseProperty(nameof(Libro.Ejemplares))]
        public Libro Libro { get; set; } = null!;

        [ForeignKey(nameof(EstadoEjemplarId))]
        public EstadoEjemplar EstadoEjemplar { get; set; } = null!;

        [ForeignKey(nameof(CondicionEjemplarId))]
        public CondicionEjemplar? CondicionEjemplar { get; set; }

        [InverseProperty(nameof(DetallePrestamo.Ejemplar))]
        public ICollection<DetallePrestamo> DetallePrestamos { get; set; } = new List<DetallePrestamo>();

        [InverseProperty(nameof(Inspeccion.Ejemplar))]
        public ICollection<Inspeccion> Inspecciones { get; set; } = new List<Inspeccion>();

        [InverseProperty(nameof(DetalleVenta.Ejemplar))]
        public ICollection<DetalleVenta> DetalleVentas { get; set; } = new List<DetalleVenta>();
    }
}
