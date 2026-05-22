using BookBackend.Modelos.DTOs.CatalogoDto;

namespace BookBackend.Servicios.CatalogoServicio
{
    public interface ICondicionEjemplarService
    {
        Task<List<CondicionEjemplarRespuestaDto>> GetAllAsync();
        Task<CondicionEjemplarRespuestaDto?> GetByIdAsync(int id);
    }
}
