using BookBackend.Modelos.DTOs.CatalogoDto;

namespace BookBackend.Servicios.CatalogoServicio
{
    public interface IMetodoPagoService
    {
        Task<List<MetodoPagoRespuestaDto>> GetAllAsync();
        Task<MetodoPagoRespuestaDto?> GetByIdAsync(int id);
    }
}
