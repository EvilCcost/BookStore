using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.MultaRepositorio
{
    public class MultaRepositorio : Repositorio<Multa>, IMultaRepositorio
    {
        private readonly BookStoreContext _db;

        public MultaRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Multa>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _db.Multas
                .Include(m => m.Usuario)
                .Include(m => m.Prestamo)
                .Where(m => m.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<List<Multa>> GetPendientesAsync()
        {
            return await _db.Multas
                .Include(m => m.Usuario)
                .Include(m => m.Prestamo)
                .Where(m => !m.Pagada)
                .ToListAsync();
        }
    }
}
