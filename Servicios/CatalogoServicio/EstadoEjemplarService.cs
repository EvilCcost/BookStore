using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.CatalogoDto;
using BookBackend.Repositorio.CatalogoRepositorio;

namespace BookBackend.Servicios.CatalogoServicio
{
    public class EstadoEjemplarService : IEstadoEjemplarService
    {
        private readonly IEstadoEjemplarRepositorio _repo;

        public EstadoEjemplarService(IEstadoEjemplarRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<List<EstadoEjemplarRespuestaDto>> GetAllAsync()
        {
            return (await _repo.GetAllAsync()).Select(MapToDto).ToList();
        }

        public async Task<EstadoEjemplarRespuestaDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : MapToDto(entity);
        }

        private static EstadoEjemplarRespuestaDto MapToDto(EstadoEjemplar e) =>
            new() { Id = e.Id, Nombre = e.Nombre };
    }
}
