using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.CatalogoDto;
using BookBackend.Repositorio.CatalogoRepositorio;

namespace BookBackend.Servicios.CatalogoServicio
{
    public class EstadoVentaService : IEstadoVentaService
    {
        private readonly IEstadoVentaRepositorio _repo;

        public EstadoVentaService(IEstadoVentaRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<List<EstadoVentaRespuestaDto>> GetAllAsync()
        {
            return (await _repo.GetAllAsync()).Select(MapToDto).ToList();
        }

        public async Task<EstadoVentaRespuestaDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : MapToDto(entity);
        }

        private static EstadoVentaRespuestaDto MapToDto(EstadoVenta e) =>
            new() { Id = e.Id, Nombre = e.Nombre };
    }
}
