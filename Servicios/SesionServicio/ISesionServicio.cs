using BookBackend.Modelos.DTOs.SesionDto;

namespace BookBackend.Servicios.SesionServicio
{
    public interface ISesionServicio
    {
        Task<List<SesionRespuestaDto>> GetAllAsync();
        Task<SesionRespuestaDto?> GetByIdAsync(int id);
        Task<List<SesionRespuestaDto>> GetByUsuarioIdAsync(int usuarioId);
        Task<bool> RevocarAsync(int id);
    }
}
