using BookBackend.Modelos.DTOs.CatalogoDto;

namespace BookBackend.Servicios.CatalogoServicio
{
    public interface IEstadoVentaService
    {
        Task<List<EstadoVentaRespuestaDto>> GetAllAsync();
        Task<EstadoVentaRespuestaDto?> GetByIdAsync(int id);
    }
}
