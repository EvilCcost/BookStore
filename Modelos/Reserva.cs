using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public int LibroId { get; set; }

        public DateTime FechaReserva { get; set; } = DateTime.UtcNow;
        public DateTime? FechaExpiracion { get; set; }

        public int EstadoReservaId { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        [InverseProperty(nameof(Usuario.Reservas))]
        public Usuario Usuario { get; set; } = null!;

        [ForeignKey(nameof(LibroId))]
        [InverseProperty(nameof(Libro.Reservas))]
        public Libro Libro { get; set; } = null!;

        [ForeignKey(nameof(EstadoReservaId))]
        public EstadoReserva EstadoReserva { get; set; } = null!;
    }
}
