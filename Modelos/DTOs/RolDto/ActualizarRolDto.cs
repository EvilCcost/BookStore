using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.RolDto
{
    public class ActualizarRolDto
    {
        [Required, MaxLength(50)]
        public string Nombre { get; set; } = null!;

        [MaxLength(200)]
        public string? Descripcion { get; set; }
    }
}
