using BookBackend.Modelos.DTOs.RolDto;

namespace BookBackend.Servicios.RolServicio
{
    public interface IRolServicio
    {
        Task<List<RolRespuestaDto>> GetAllAsync();
        Task<RolRespuestaDto?> GetByIdAsync(int id);
        Task<RolRespuestaDto> CreateAsync(CrearRolDto crearDto);
        Task<RolRespuestaDto?> UpdateAsync(int id, ActualizarRolDto actualizarDto);
        Task<bool> DeleteAsync(int id);
    }
}
