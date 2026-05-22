using BookBackend.Modelos;

namespace BookBackend.Repositorio.EjemplarRepositorio
{
    public interface IEjemplarRepositorio : IRepositorio<Ejemplar>
    {
        Task<Ejemplar?> GetByCodigoBarrasAsync(string codigoBarras);
        Task<List<Ejemplar>> GetByLibroIdAsync(int libroId);
        Task<List<Ejemplar>> GetDisponiblesAsync();
        Task<Ejemplar?> GetEjemplarWithDetailsAsync(int id);
    }
}
