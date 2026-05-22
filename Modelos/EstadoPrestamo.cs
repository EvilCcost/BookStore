using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos
{
    public class EstadoPrestamo
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = null!;

        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
