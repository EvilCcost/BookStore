namespace BookBackend.Modelos.DTOs.InspeccionDto
{
    public class InspeccionRespuestaDto
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public int EjemplarId { get; set; }
        public string? CodigoBarras { get; set; }
        public int? InspeccionadoPorUsuarioId { get; set; }
        public string? InspeccionadoPorNombre { get; set; }
        public DateTime FechaInspeccion { get; set; }
        public int TipoInspeccionId { get; set; }
        public string? TipoInspeccion { get; set; }
        public string CondicionRegistrada { get; set; } = null!;
        public string? Notas { get; set; }
    }
}
