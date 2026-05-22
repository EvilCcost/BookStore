using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.UsuarioRepositorio
{
    public class UsuarioRepositorio : Repositorio<Usuario>, IUsuarioRepositorio
    {
        private readonly BookStoreContext _db;

        public UsuarioRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _db.Usuarios
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> GetUsuarioWithRolesAsync(int id)
        {
            return await _db.Usuarios
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Usuario>> GetActiveUsuariosAsync()
        {
            return await _db.Usuarios
                .Where(u => u.Activo)
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .ToListAsync();
        }
    }
}
