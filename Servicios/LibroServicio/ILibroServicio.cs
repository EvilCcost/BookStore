using BookBackend.Modelos.DTOs.LibroDto;

namespace BookBackend.Servicios.LibroServicio
{
    public interface ILibroServicio
    {
        Task<List<LibroRespuestaDto>> GetAllAsync();
        Task<LibroRespuestaDto?> GetByIdAsync(int id);
        Task<List<LibroRespuestaDto>> SearchByTituloAsync(string titulo);
        Task<LibroRespuestaDto> CreateAsync(CrearLibroDto crearDto);
        Task<LibroRespuestaDto?> UpdateAsync(int id, ActualizarLibroDto actualizarDto);
        Task<bool> DeleteAsync(int id);
    }
}
