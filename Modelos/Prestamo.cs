using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class Prestamo
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public DateTime FechaPrestamo { get; set; } = DateTime.UtcNow;

        public DateTime FechaDevolucionEsperada { get; set; }

        public DateTime? FechaDevolucionReal { get; set; }

        public int EstadoPrestamoId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty(nameof(Usuario.Prestamos))]
        public Usuario Usuario { get; set; } = null!;

        [ForeignKey(nameof(EstadoPrestamoId))]
        public EstadoPrestamo EstadoPrestamo { get; set; } = null!;

        [InverseProperty(nameof(DetallePrestamo.Prestamo))]
        public ICollection<DetallePrestamo> DetallePrestamos { get; set; } = new List<DetallePrestamo>();

        [InverseProperty(nameof(Inspeccion.Prestamo))]
        public ICollection<Inspeccion> Inspecciones { get; set; } = new List<Inspeccion>();

        [InverseProperty(nameof(Multa.Prestamo))]
        public ICollection<Multa> Multas { get; set; } = new List<Multa>();
    }
}
