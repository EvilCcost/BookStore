using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.CatalogoDto;
using BookBackend.Repositorio.CatalogoRepositorio;

namespace BookBackend.Servicios.CatalogoServicio
{
    public class TipoInspeccionService : ITipoInspeccionService
    {
        private readonly ITipoInspeccionRepositorio _repo;

        public TipoInspeccionService(ITipoInspeccionRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<List<TipoInspeccionRespuestaDto>> GetAllAsync()
        {
            return (await _repo.GetAllAsync()).Select(MapToDto).ToList();
        }

        public async Task<TipoInspeccionRespuestaDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : MapToDto(entity);
        }

        private static TipoInspeccionRespuestaDto MapToDto(TipoInspeccion e) =>
            new() { Id = e.Id, Nombre = e.Nombre };
    }
}
