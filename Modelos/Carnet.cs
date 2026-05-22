using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    [Index(nameof(Codigo), IsUnique = true)]
    public class Carnet
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public int UsuarioId { get; set; } 

        [Required, MaxLength(20)]
        public string Codigo { get; set; } = null!;

        public DateTime FechaEmision { get; set; } = DateTime.UtcNow;

        public DateTime FechaVencimiento { get; set; }

        public bool Activo { get; set; } = true;

        public Usuario Usuario { get; set; } = null!;
    }
}
