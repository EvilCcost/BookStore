using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.EjemplarDto
{
    public class ActualizarEjemplarDto
    {
        [MaxLength(30)]
        public string? CodigoBarras { get; set; }

        [MaxLength(50)]
        public string? UbicacionEstante { get; set; }

        public int? EstadoEjemplarId { get; set; }

        public int? CondicionEjemplarId { get; set; }
    }
}
