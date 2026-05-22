using BookBackend.Modelos.DTOs.MultaDto;

namespace BookBackend.Servicios.MultaServicio
{
    public interface IMultaServicio
    {
        Task<List<MultaRespuestaDto>> GetAllAsync();
        Task<MultaRespuestaDto?> GetByIdAsync(int id);
        Task<List<MultaRespuestaDto>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<MultaRespuestaDto>> GetPendientesAsync();
        Task<MultaRespuestaDto> CreateAsync(CrearMultaDto crearDto);
        Task<MultaRespuestaDto?> PagarAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
