namespace BookBackend.Modelos.DTOs.AutorDto
{
    public class AutorRespuestaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Nacionalidad { get; set; }
    }
}
