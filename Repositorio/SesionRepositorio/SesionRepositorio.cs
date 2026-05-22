using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.SesionRepositorio
{
    public class SesionRepositorio : Repositorio<Sesion>, ISesionRepositorio
    {
        private readonly BookStoreContext _db;

        public SesionRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Sesion>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _db.Sesiones
                .Include(s => s.Usuario)
                .Where(s => s.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<Sesion?> GetSesionWithUserAsync(int id)
        {
            return await _db.Sesiones
                .Include(s => s.Usuario)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
