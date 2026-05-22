using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos
{
    public class MetodoPago
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = null!;

        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}
