using BookBackend.Modelos.DTOs.CatalogoDto;

namespace BookBackend.Servicios.CatalogoServicio
{
    public interface IEstadoReservaService
    {
        Task<List<EstadoReservaRespuestaDto>> GetAllAsync();
        Task<EstadoReservaRespuestaDto?> GetByIdAsync(int id);
    }
}
