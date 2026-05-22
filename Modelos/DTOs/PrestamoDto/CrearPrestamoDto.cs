using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.PrestamoDto
{
    public class CrearPrestamoDto
    {
        [Required]
        public int UsuarioId { get; set; }

        public DateTime FechaDevolucionEsperada { get; set; }

        [Required, MinLength(1)]
        public int[] EjemplarIds { get; set; } = Array.Empty<int>();
    }
}
