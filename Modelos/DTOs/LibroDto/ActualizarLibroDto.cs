using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.LibroDto
{
    public class ActualizarLibroDto
    {
        [Required, MaxLength(200)]
        public string Titulo { get; set; } = null!;

        [MaxLength(20)]
        public string? Isbn { get; set; }

        [MaxLength(100)]
        public string? Editorial { get; set; }

        public int? AnioPublicacion { get; set; }

        public int CantidadEjemplares { get; set; }

        public int[] AutorIds { get; set; } = Array.Empty<int>();

        public int[] CategoriaIds { get; set; } = Array.Empty<int>();
    }
}
