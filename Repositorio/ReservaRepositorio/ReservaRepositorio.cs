using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.ReservaRepositorio
{
    public class ReservaRepositorio : Repositorio<Reserva>, IReservaRepositorio
    {
        private readonly BookStoreContext _db;

        public ReservaRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Reserva?> GetReservaWithDetailsAsync(int id)
        {
            return await _db.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Libro)
                .Include(r => r.EstadoReserva)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reserva>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _db.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Libro)
                .Include(r => r.EstadoReserva)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<List<Reserva>> GetPendientesAsync()
        {
            return await _db.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.Libro)
                .Include(r => r.EstadoReserva)
                .Where(r => r.EstadoReservaId == 1)
                .ToListAsync();
        }
    }
}
