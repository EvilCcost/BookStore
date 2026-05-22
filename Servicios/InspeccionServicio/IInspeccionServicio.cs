using BookBackend.Modelos.DTOs.InspeccionDto;

namespace BookBackend.Servicios.InspeccionServicio
{
    public interface IInspeccionServicio
    {
        Task<List<InspeccionRespuestaDto>> GetAllAsync();
        Task<InspeccionRespuestaDto?> GetByIdAsync(int id);
        Task<List<InspeccionRespuestaDto>> GetByPrestamoIdAsync(int prestamoId);
        Task<List<InspeccionRespuestaDto>> GetByEjemplarIdAsync(int ejemplarId);
        Task<InspeccionRespuestaDto> CreateAsync(CrearInspeccionDto crearDto);
        Task<InspeccionRespuestaDto?> UpdateAsync(int id, ActualizarInspeccionDto actualizarDto);
        Task<bool> DeleteAsync(int id);
    }
}
