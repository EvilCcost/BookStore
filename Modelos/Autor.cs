using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class Autor
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [MaxLength(50)]
        public string? Nacionalidad { get; set; }

        [InverseProperty(nameof(LibroAutor.Autor))]
        public ICollection<LibroAutor> LibroAutores { get; set; } = new List<LibroAutor>();
    }
}
