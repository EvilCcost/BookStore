using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos
{
    public class TipoInspeccion
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } = null!;

        public ICollection<Inspeccion> Inspecciones { get; set; } = new List<Inspeccion>();
    }
}
