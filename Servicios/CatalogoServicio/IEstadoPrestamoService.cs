using BookBackend.Modelos.DTOs.CatalogoDto;

namespace BookBackend.Servicios.CatalogoServicio
{
    public interface IEstadoPrestamoService
    {
        Task<List<EstadoPrestamoRespuestaDto>> GetAllAsync();
        Task<EstadoPrestamoRespuestaDto?> GetByIdAsync(int id);
    }
}
