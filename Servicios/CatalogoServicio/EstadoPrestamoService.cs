using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.CatalogoDto;
using BookBackend.Repositorio.CatalogoRepositorio;

namespace BookBackend.Servicios.CatalogoServicio
{
    public class EstadoPrestamoService : IEstadoPrestamoService
    {
        private readonly IEstadoPrestamoRepositorio _repo;

        public EstadoPrestamoService(IEstadoPrestamoRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<List<EstadoPrestamoRespuestaDto>> GetAllAsync()
        {
            return (await _repo.GetAllAsync()).Select(MapToDto).ToList();
        }

        public async Task<EstadoPrestamoRespuestaDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : MapToDto(entity);
        }

        private static EstadoPrestamoRespuestaDto MapToDto(EstadoPrestamo e) =>
            new() { Id = e.Id, Nombre = e.Nombre };
    }
}
