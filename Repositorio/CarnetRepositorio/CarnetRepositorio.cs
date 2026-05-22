using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.CarnetRepositorio
{
    public class CarnetRepositorio : Repositorio<Carnet>, ICarnetRepositorio
    {
        private readonly BookStoreContext _db;

        public CarnetRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Carnet?> GetByCodigoAsync(string codigo)
        {
            return await _db.Carnets
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.Codigo == codigo);
        }

        public async Task<Carnet?> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _db.Carnets
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId);
        }

        public async Task<List<Carnet>> GetActivosAsync()
        {
            return await _db.Carnets
                .Include(c => c.Usuario)
                .Where(c => c.Activo && c.FechaVencimiento > DateTime.UtcNow)
                .ToListAsync();
        }
    }
}
