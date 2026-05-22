using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    [PrimaryKey(nameof(LibroId), nameof(CategoriaId))]
    public class LibroCategoria
    {
        public int LibroId { get; set; }
        public int CategoriaId { get; set; }

        [ForeignKey(nameof(LibroId))]
        [InverseProperty(nameof(Libro.LibroCategorias))]
        public Libro Libro { get; set; } = null!;

        [ForeignKey(nameof(CategoriaId))]
        [InverseProperty(nameof(Categoria.LibroCategorias))]
        public Categoria Categoria { get; set; } = null!;
    }
}
