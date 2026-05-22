using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos
{
    public class EstadoEjemplar
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = null!;

        public ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();
    }
}
