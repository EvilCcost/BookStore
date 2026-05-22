using BookBackend.Modelos.DTOs.CarnetDto;

namespace BookBackend.Servicios.CarnetServicio
{
    public interface ICarnetServicio
    {
        Task<List<CarnetRespuestaDto>> GetAllAsync();
        Task<CarnetRespuestaDto?> GetByIdAsync(int id);
        Task<CarnetRespuestaDto?> GetByCodigoAsync(string codigo);
        Task<CarnetRespuestaDto?> GetByUsuarioIdAsync(int usuarioId);
        Task<List<CarnetRespuestaDto>> GetActivosAsync();
        Task<CarnetRespuestaDto> CreateAsync(CrearCarnetDto crearDto);
        Task<CarnetRespuestaDto?> RenovarAsync(int id, DateTime nuevaFechaVencimiento);
        Task<CarnetRespuestaDto?> UpdateAsync(int id, ActualizarCarnetDto actualizarDto);
        Task<bool> DeleteAsync(int id);
    }
}
