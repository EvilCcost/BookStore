using BookBackend.Modelos;

namespace BookBackend.Repositorio.SesionRepositorio
{
    public interface ISesionRepositorio : IRepositorio<Sesion>
    {
        Task<List<Sesion>> GetByUsuarioIdAsync(int usuarioId);
        Task<Sesion?> GetSesionWithUserAsync(int id);
    }
}
