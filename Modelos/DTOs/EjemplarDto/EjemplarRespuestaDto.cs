namespace BookBackend.Modelos.DTOs.EjemplarDto
{
    public class EjemplarRespuestaDto
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public string? LibroTitulo { get; set; }
        public string CodigoBarras { get; set; } = null!;
        public string? UbicacionEstante { get; set; }
        public int EstadoEjemplarId { get; set; }
        public string? EstadoEjemplar { get; set; }
        public int? CondicionEjemplarId { get; set; }
        public string? CondicionEjemplar { get; set; }
    }
}
