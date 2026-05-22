using BookBackend.Modelos.DTOs.CatalogoDto;

namespace BookBackend.Servicios.CatalogoServicio
{
    public interface ITipoInspeccionService
    {
        Task<List<TipoInspeccionRespuestaDto>> GetAllAsync();
        Task<TipoInspeccionRespuestaDto?> GetByIdAsync(int id);
    }
}
