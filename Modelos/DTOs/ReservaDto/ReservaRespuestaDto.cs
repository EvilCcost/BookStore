namespace BookBackend.Modelos.DTOs.ReservaDto
{
    public class ReservaRespuestaDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? UsuarioEmail { get; set; }
        public int LibroId { get; set; }
        public string? LibroIsbn { get; set; }
        public string? LibroEditorial { get; set; }
        public string? LibroTitulo { get; set; }
        public DateTime FechaReserva { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int EstadoReservaId { get; set; }
        public string? EstadoReserva { get; set; }
    }
}
