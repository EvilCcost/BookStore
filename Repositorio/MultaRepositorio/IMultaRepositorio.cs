using BookBackend.Modelos;

namespace BookBackend.Repositorio.MultaRepositorio
{
    public interface IMultaRepositorio : IRepositorio<Multa>
    {
        Task<List<Multa>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<Multa>> GetPendientesAsync();
    }
}
