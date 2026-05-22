namespace BookBackend.Modelos.DTOs.VentaDto
{
    public class DetalleVentaResumenDto
    {
        public int Id { get; set; }
        public int EjemplarId { get; set; }
        public string? CodigoBarras { get; set; }
        public decimal PrecioUnitario { get; set; }
    }

    public class VentaRespuestaDto
    {
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public string? UsuarioNombre { get; set; }
        public DateTime FechaVenta { get; set; }
        public decimal Total { get; set; }
        public int EstadoVentaId { get; set; }
        public string? EstadoVenta { get; set; }
        public int MetodoPagoId { get; set; }
        public string? MetodoPago { get; set; }
        public string? Comprobante { get; set; }
        public List<DetalleVentaResumenDto> DetalleVentas { get; set; } = new();
    }
}
