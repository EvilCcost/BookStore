namespace BookBackend.Modelos.DTOs.SesionDto
{
    public class SesionRespuestaDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? UsuarioEmail { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public bool Revocada { get; set; }
    }
}
