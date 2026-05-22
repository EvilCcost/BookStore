using BookBackend.Modelos.DTOs.ReservaDto;

namespace BookBackend.Servicios.ReservaServicio
{
    public interface IReservaServicio
    {
        Task<List<ReservaRespuestaDto>> GetAllAsync();
        Task<ReservaRespuestaDto?> GetByIdAsync(int id);
        Task<List<ReservaRespuestaDto>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<ReservaRespuestaDto>> GetPendientesAsync();
        Task<ReservaRespuestaDto> CreateAsync(CrearReservaDto crearDto);
        Task<ReservaRespuestaDto?> CumplirAsync(int id);
        Task<ReservaRespuestaDto?> CancelarAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
