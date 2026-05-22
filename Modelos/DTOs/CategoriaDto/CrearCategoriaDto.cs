using System.ComponentModel.DataAnnotations;

namespace BookBackend.Modelos.DTOs.CategoriaDto
{
    public class CrearCategoriaDto
    {
        [Required, MaxLength(80)]
        public string Nombre { get; set; } = null!;
    }
}
