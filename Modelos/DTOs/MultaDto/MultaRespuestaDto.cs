namespace BookBackend.Modelos.DTOs.MultaDto
{
    public class MultaRespuestaDto
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public int? PrestamoId { get; set; }
        public decimal Monto { get; set; }
        public string Motivo { get; set; } = null!;
        public bool Pagada { get; set; }
        public DateTime FechaEmision { get; set; }
    }
}
