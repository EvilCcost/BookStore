namespace BookBackend.Modelos.DTOs.UsuarioDto
{
    public class UsuarioRespuestaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool Activo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
