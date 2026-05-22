using BookBackend.Modelos;

namespace BookBackend.Repositorio.PrestamoRepositorio
{
    public interface IPrestamoRepositorio : IRepositorio<Prestamo>
    {
        Task<Prestamo?> GetPrestamoWithDetailsAsync(int id);
        Task<List<Prestamo>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<Prestamo>> GetActivosAsync();
        Task<List<Prestamo>> GetVencidosAsync();
    }
}
