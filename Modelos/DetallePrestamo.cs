using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class DetallePrestamo
    {
        [Key]
        public int Id { get; set; }

        public int PrestamoId { get; set; }
        public int EjemplarId { get; set; }

        [ForeignKey(nameof(PrestamoId))]
        [InverseProperty(nameof(Prestamo.DetallePrestamos))]
        public Prestamo Prestamo { get; set; } = null!;

        [ForeignKey(nameof(EjemplarId))]
        [InverseProperty(nameof(Ejemplar.DetallePrestamos))]
        public Ejemplar Ejemplar { get; set; } = null!;
    }
}
