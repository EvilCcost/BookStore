using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.CatalogoDto;
using BookBackend.Repositorio.CatalogoRepositorio;

namespace BookBackend.Servicios.CatalogoServicio
{
    public class CondicionEjemplarService : ICondicionEjemplarService
    {
        private readonly ICondicionEjemplarRepositorio _repo;

        public CondicionEjemplarService(ICondicionEjemplarRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<List<CondicionEjemplarRespuestaDto>> GetAllAsync()
        {
            return (await _repo.GetAllAsync()).Select(MapToDto).ToList();
        }

        public async Task<CondicionEjemplarRespuestaDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : MapToDto(entity);
        }

        private static CondicionEjemplarRespuestaDto MapToDto(CondicionEjemplar e) =>
            new() { Id = e.Id, Nombre = e.Nombre };
    }
}
