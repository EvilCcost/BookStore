using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.RolRepositorio
{
    public class RolRepositorio : Repositorio<Rol>, IRolRepositorio
    {
        private readonly BookStoreContext _db;

        public RolRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Rol?> GetRolWithUsersAsync(int id)
        {
            return await _db.Roles
                .Include(r => r.UsuarioRoles)
                    .ThenInclude(ur => ur.Usuario)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}
