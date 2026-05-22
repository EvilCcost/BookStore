using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.CatalogoDto;
using BookBackend.Repositorio.CatalogoRepositorio;

namespace BookBackend.Servicios.CatalogoServicio
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly IMetodoPagoRepositorio _repo;

        public MetodoPagoService(IMetodoPagoRepositorio repo)
        {
            _repo = repo;
        }

        public async Task<List<MetodoPagoRespuestaDto>> GetAllAsync()
        {
            return (await _repo.GetAllAsync()).Select(MapToDto).ToList();
        }

        public async Task<MetodoPagoRespuestaDto?> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity is null ? null : MapToDto(entity);
        }

        private static MetodoPagoRespuestaDto MapToDto(MetodoPago e) =>
            new() { Id = e.Id, Nombre = e.Nombre };
    }
}
