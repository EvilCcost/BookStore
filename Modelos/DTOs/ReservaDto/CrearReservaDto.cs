using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.ReservaDto
{
    public class CrearReservaDto
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public int LibroId { get; set; }

        public DateTime? FechaExpiracion { get; set; }
    }
}
