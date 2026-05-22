using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.CatalogoDto;
using BookBackend.Repositorio.CatalogoRepositorio;

namespace BookBackend.Servicios.CatalogoServicio
{
    public class EstadoReservaService : IEstadoReservaService
    {
        private readonly IEstadoReservaRepositorio _repo;

        public EstadoReservaService(IEstadoReservaRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<List<EstadoReservaRespuestaDto>> GetAllAsync()
        {
            return (await _repo.GetAllAsync()).Select(MapToDto).ToList();
        }

        public async Task<EstadoReservaRespuestaDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : MapToDto(entity);
        }

        private static EstadoReservaRespuestaDto MapToDto(EstadoReserva e) =>
            new() { Id = e.Id, Nombre = e.Nombre };
    }
}
