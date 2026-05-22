namespace BookBackend.Modelos.DTOs.PrestamoDto
{
    public class DetallePrestamoResumenDto
    {
        public int Id { get; set; }
        public int EjemplarId { get; set; }
        public string? CodigoBarras { get; set; }
    }

    public class PrestamoRespuestaDto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public string? UsuarioEmail { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaDevolucionEsperada { get; set; }
        public DateTime? FechaDevolucionReal { get; set; }
        public int EstadoPrestamoId { get; set; }
        public string? EstadoPrestamo { get; set; }
        public List<DetallePrestamoResumenDto> DetallePrestamos { get; set; } = new();
    }
}
