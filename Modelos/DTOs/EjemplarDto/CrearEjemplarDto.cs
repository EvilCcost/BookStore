using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.EjemplarDto
{
    public class CrearEjemplarDto
    {
        [Required]
        public int LibroId { get; set; }

        [Required, MaxLength(30)]
        public string CodigoBarras { get; set; } = null!;

        [MaxLength(50)]
        public string? UbicacionEstante { get; set; }

        public int EstadoEjemplarId { get; set; } = 1;

        public int? CondicionEjemplarId { get; set; } = 1;
    }
}
