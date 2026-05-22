using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos
{
    public class EstadoReserva
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = null!;

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
