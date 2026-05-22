using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.PrestamoRepositorio
{
    public class PrestamoRepositorio : Repositorio<Prestamo>, IPrestamoRepositorio
    {
        private readonly BookStoreContext _db;

        public PrestamoRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Prestamo?> GetPrestamoWithDetailsAsync(int id)
        {
            return await _db.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.EstadoPrestamo)
                .Include(p => p.DetallePrestamos)
                    .ThenInclude(dp => dp.Ejemplar)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Prestamo>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _db.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.EstadoPrestamo)
                .Include(p => p.DetallePrestamos)
                    .ThenInclude(dp => dp.Ejemplar)
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<List<Prestamo>> GetActivosAsync()
        {
            return await _db.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.EstadoPrestamo)
                .Include(p => p.DetallePrestamos)
                    .ThenInclude(dp => dp.Ejemplar)
                .Where(p => p.EstadoPrestamoId == 1)
                .ToListAsync();
        }

        public async Task<List<Prestamo>> GetVencidosAsync()
        {
            return await _db.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.EstadoPrestamo)
                .Include(p => p.DetallePrestamos)
                    .ThenInclude(dp => dp.Ejemplar)
                .Where(p => p.EstadoPrestamoId == 1 && p.FechaDevolucionEsperada < DateTime.UtcNow)
                .ToListAsync();
        }
    }
}
