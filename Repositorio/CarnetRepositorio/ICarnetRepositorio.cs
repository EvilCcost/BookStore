using BookBackend.Modelos;

namespace BookBackend.Repositorio.CarnetRepositorio
{
    public interface ICarnetRepositorio : IRepositorio<Carnet>
    {
        Task<Carnet?> GetByCodigoAsync(string codigo);
        Task<Carnet?> GetByUsuarioIdAsync(int usuarioId);
        Task<List<Carnet>> GetActivosAsync();
    }
}
