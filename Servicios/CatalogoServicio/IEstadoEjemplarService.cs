using BookBackend.Modelos.DTOs.CatalogoDto;

namespace BookBackend.Servicios.CatalogoServicio
{
    public interface IEstadoEjemplarService
    {
        Task<List<EstadoEjemplarRespuestaDto>> GetAllAsync();
        Task<EstadoEjemplarRespuestaDto?> GetByIdAsync(int id);
    }
}
