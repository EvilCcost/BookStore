using BookBackend.Modelos.DTOs.CategoriaDto;

namespace BookBackend.Servicios.CategoriaServicio
{
    public interface ICategoriaServicio
    {
        Task<List<CategoriaRespuestaDto>> GetAllAsync();
        Task<CategoriaRespuestaDto?> GetByIdAsync(int id);
        Task<List<CategoriaRespuestaDto>> SearchByNombreAsync(string nombre);
        Task<CategoriaRespuestaDto> CreateAsync(CrearCategoriaDto crearDto);
        Task<CategoriaRespuestaDto?> UpdateAsync(int id, ActualizarCategoriaDto actualizarDto);
        Task<bool> DeleteAsync(int id);
    }
}
