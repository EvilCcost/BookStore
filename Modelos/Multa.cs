using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class Multa
    {
        [Key]
        public int Id { get; set; }

        public int? UsuarioId { get; set; }
        public int? PrestamoId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }

        [Required, MaxLength(100)]
        public string Motivo { get; set; } = null!;

        public bool Pagada { get; set; } = false;
        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;

        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty(nameof(Usuario.Multas))]
        public Usuario? Usuario { get; set; }

        [ForeignKey(nameof(PrestamoId))]
        [InverseProperty(nameof(Prestamo.Multas))]
        public Prestamo? Prestamo { get; set; }
    }
}
