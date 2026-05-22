using BookBackend.Modelos;

namespace BookBackend.Repositorio.RolRepositorio
{
    public interface IRolRepositorio : IRepositorio<Rol>
    {
        Task<Rol?> GetRolWithUsersAsync(int id);
    }
}
