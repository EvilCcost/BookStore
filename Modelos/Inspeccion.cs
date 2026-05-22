using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class Inspeccion
    {
        [Key]
        public int Id { get; set; }

        public int PrestamoId { get; set; }
        public int EjemplarId { get; set; }
        public int? InspeccionadoPorUsuarioId { get; set; }

        public DateTime FechaInspeccion { get; set; } = DateTime.UtcNow;

        public int TipoInspeccionId { get; set; }

        [Required, MaxLength(200)]
        public string CondicionRegistrada { get; set; } = null!;

        [MaxLength(500)]
        public string? Notas { get; set; }

        [ForeignKey(nameof(PrestamoId))]
        [InverseProperty(nameof(Prestamo.Inspecciones))]
        public Prestamo Prestamo { get; set; } = null!;

        [ForeignKey(nameof(EjemplarId))]
        [InverseProperty(nameof(Ejemplar.Inspecciones))]
        public Ejemplar Ejemplar { get; set; } = null!;

        [ForeignKey(nameof(InspeccionadoPorUsuarioId))]
        public Usuario? InspeccionadoPor { get; set; }

        [ForeignKey(nameof(TipoInspeccionId))]
        public TipoInspeccion TipoInspeccion { get; set; } = null!;
    }
}
