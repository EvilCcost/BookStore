namespace BookBackend.Modelos.DTOs.LibroDto
{
    public class LibroRespuestaDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string? Isbn { get; set; }
        public string? Editorial { get; set; }
        public int? AnioPublicacion { get; set; }
        public int CantidadEjemplares { get; set; }
        public int EjemplaresDisponibles { get; set; }
        public List<AutorResumenDto> Autores { get; set; } = new();
        public List<CategoriaResumenDto> Categorias { get; set; } = new();
    }
}
