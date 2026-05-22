using BookBackend.Modelos.DTOs.AutorDto;

namespace BookBackend.Servicios.AutorServicio
{
    public interface IAutorServicio
    {
        Task<List<AutorRespuestaDto>> GetAllAsync();
        Task<AutorRespuestaDto?> GetByIdAsync(int id);
        Task<List<AutorRespuestaDto>> SearchByNombreAsync(string nombre);
        Task<AutorRespuestaDto> CreateAsync(CrearAutorDto crearDto);
        Task<AutorRespuestaDto?> UpdateAsync(int id, ActualizarAutorDto actualizarDto);
        Task<bool> DeleteAsync(int id);
    }
}
