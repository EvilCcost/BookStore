using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    [PrimaryKey(nameof(LibroId), nameof(AutorId))]
    public class LibroAutor
    {
        public int LibroId { get; set; }
        public int AutorId { get; set; }

        [ForeignKey(nameof(LibroId))]
        [InverseProperty(nameof(Libro.LibroAutores))]
        public Libro Libro { get; set; } = null!;

        [ForeignKey(nameof(AutorId))]
        [InverseProperty(nameof(Autor.LibroAutores))]
        public Autor Autor { get; set; } = null!;
    }
}
