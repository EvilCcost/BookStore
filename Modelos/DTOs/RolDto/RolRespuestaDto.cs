namespace BookBackend.Modelos.DTOs.RolDto
{
    public class RolRespuestaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
