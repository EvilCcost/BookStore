using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    public class Categoria
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(80)]
        public string Nombre { get; set; } = null!;

        [InverseProperty(nameof(LibroCategoria.Categoria))]
        public ICollection<LibroCategoria> LibroCategorias { get; set; } = new List<LibroCategoria>();
    }
}
