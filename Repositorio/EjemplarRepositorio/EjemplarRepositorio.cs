using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.EjemplarRepositorio
{
    public class EjemplarRepositorio : Repositorio<Ejemplar>, IEjemplarRepositorio
    {
        private readonly BookStoreContext _db;

        public EjemplarRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Ejemplar?> GetByCodigoBarrasAsync(string codigoBarras)
        {
            return await _db.Ejemplares
                .Include(e => e.EstadoEjemplar)
                .Include(e => e.CondicionEjemplar)
                .Include(e => e.Libro)
                .FirstOrDefaultAsync(e => e.CodigoBarras == codigoBarras);
        }

        public async Task<List<Ejemplar>> GetByLibroIdAsync(int libroId)
        {
            return await _db.Ejemplares
                .Include(e => e.EstadoEjemplar)
                .Include(e => e.CondicionEjemplar)
                .Where(e => e.LibroId == libroId)
                .ToListAsync();
        }

        public async Task<List<Ejemplar>> GetDisponiblesAsync()
        {
            return await _db.Ejemplares
                .Include(e => e.EstadoEjemplar)
                .Include(e => e.CondicionEjemplar)
                .Include(e => e.Libro)
                .Where(e => e.EstadoEjemplarId == 1)
                .ToListAsync();
        }

        public async Task<Ejemplar?> GetEjemplarWithDetailsAsync(int id)
        {
            return await _db.Ejemplares
                .Include(e => e.EstadoEjemplar)
                .Include(e => e.CondicionEjemplar)
                .Include(e => e.Libro)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
