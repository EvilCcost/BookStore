using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.AutorDto
{
    public class ActualizarAutorDto
    {
        [Required, MaxLength(100)]
        public string Nombre { get; set; } = null!;

        [MaxLength(50)]
        public string? Nacionalidad { get; set; }
    }
}
