using BookBackend.Modelos.DTOs.UsuarioDto;

namespace BookBackend.Servicios.UsuarioServicio
{
    public interface IUsuarioServicio
    {
        Task<List<UsuarioRespuestaDto>> GetAllAsync();
        Task<UsuarioRespuestaDto?> GetByIdAsync(int id);
        Task<UsuarioRespuestaDto?> GetByEmailAsync(string email);
        Task<UsuarioRespuestaDto> CreateAsync(CrearUsuarioDto crearDto);
        Task<UsuarioRespuestaDto?> UpdateAsync(int id, ActualizarUsuarioDto actualizarDto);
        Task<LoginUsuarioRespuestaDto> LoginAsync(LoginUsuarioDto loginDto);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleActivoAsync(int id);
    }
}
