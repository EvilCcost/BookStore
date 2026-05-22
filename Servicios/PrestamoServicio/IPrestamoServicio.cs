using BookBackend.Modelos.DTOs.PrestamoDto;

namespace BookBackend.Servicios.PrestamoServicio
{
    public interface IPrestamoServicio
    {
        Task<List<PrestamoRespuestaDto>> GetAllAsync();
        Task<PrestamoRespuestaDto?> GetByIdAsync(int id);
        Task<List<PrestamoRespuestaDto>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<PrestamoRespuestaDto>> GetActivosAsync();
        Task<List<PrestamoRespuestaDto>> GetVencidosAsync();
        Task<PrestamoRespuestaDto> CreateAsync(CrearPrestamoDto crearDto);
        Task<PrestamoRespuestaDto?> DevolverAsync(int id, DateTime fechaDevolucion);
        Task<PrestamoRespuestaDto?> CancelarAsync(int id);
        Task<bool> DeleteAsync(int id);
    }
}
