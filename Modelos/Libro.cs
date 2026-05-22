using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookBackend.Modelos
{
    [Index(nameof(Isbn), IsUnique = true)]
    public class Libro
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Titulo { get; set; } = null!;

        [MaxLength(20)]
        public string? Isbn { get; set; }

        [MaxLength(100)]
        public string? Editorial { get; set; }

        public int? AnioPublicacion { get; set; }

        [NotMapped]
        public int CantidadEjemplares => Ejemplares?.Count ?? 0;

        [NotMapped]
        public int EjemplaresDisponibles => Ejemplares?.Count(e => e.EstadoEjemplarId == 1) ?? 0;

        [InverseProperty(nameof(LibroAutor.Libro))]
        public ICollection<LibroAutor> LibroAutores { get; set; } = new List<LibroAutor>();

        [InverseProperty(nameof(LibroCategoria.Libro))]
        public ICollection<LibroCategoria> LibroCategorias { get; set; } = new List<LibroCategoria>();

        [InverseProperty(nameof(Ejemplar.Libro))]
        public ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();

        [InverseProperty(nameof(Reserva.Libro))]
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
