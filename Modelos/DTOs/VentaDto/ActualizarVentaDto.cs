namespace BookBackend.Modelos.DTOs.VentaDto
{
    public class ActualizarVentaDto
    {
        public int? EstadoVentaId { get; set; }
        public int? MetodoPagoId { get; set; }
        public string? Comprobante { get; set; }
    }
}
