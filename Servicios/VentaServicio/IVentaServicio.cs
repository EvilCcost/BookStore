using BookBackend.Modelos.DTOs.VentaDto;

namespace BookBackend.Servicios.VentaServicio
{
    public interface IVentaServicio
    {
        Task<List<VentaRespuestaDto>> GetAllAsync();
        Task<VentaRespuestaDto?> GetByIdAsync(int id);
        Task<List<VentaRespuestaDto>> GetByUsuarioIdAsync(int? usuarioId);
        Task<List<VentaRespuestaDto>> GetByFechaRangeAsync(DateTime desde, DateTime hasta);
        Task<VentaRespuestaDto> CreateAsync(CrearVentaDto crearDto);
        Task<VentaRespuestaDto?> CancelarAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
