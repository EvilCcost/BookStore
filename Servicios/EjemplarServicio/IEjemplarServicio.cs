using BookBackend.Modelos.DTOs.EjemplarDto;

namespace BookBackend.Servicios.EjemplarServicio
{
    public interface IEjemplarServicio
    {
        Task<List<EjemplarRespuestaDto>> GetAllAsync();
        Task<EjemplarRespuestaDto?> GetByIdAsync(int id);
        Task<EjemplarRespuestaDto?> GetByCodigoBarrasAsync(string codigoBarras);
        Task<List<EjemplarRespuestaDto>> GetByLibroIdAsync(int libroId);
        Task<List<EjemplarRespuestaDto>> GetDisponiblesAsync();
        Task<EjemplarRespuestaDto> CreateAsync(CrearEjemplarDto crearDto);
        Task<EjemplarRespuestaDto?> UpdateAsync(int id, ActualizarEjemplarDto actualizarDto);
        Task<bool> DeleteAsync(int id);
    }
}
