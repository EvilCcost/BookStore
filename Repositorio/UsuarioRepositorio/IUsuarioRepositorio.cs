using BookBackend.Modelos;

namespace BookBackend.Repositorio.UsuarioRepositorio
{
    public interface IUsuarioRepositorio : IRepositorio<Usuario>
    {
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario?> GetUsuarioWithRolesAsync(int id);
        Task<List<Usuario>> GetActiveUsuariosAsync();
        
    }
}
