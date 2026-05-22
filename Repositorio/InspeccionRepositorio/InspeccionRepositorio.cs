using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.InspeccionRepositorio
{
    public class InspeccionRepositorio : Repositorio<Inspeccion>, IInspeccionRepositorio
    {
        private readonly BookStoreContext _db;

        public InspeccionRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Inspeccion>> GetByPrestamoIdAsync(int prestamoId)
        {
            return await _db.Inspecciones
                .Include(i => i.TipoInspeccion)
                .Include(i => i.Ejemplar)
                .Include(i => i.InspeccionadoPor)
                .Where(i => i.PrestamoId == prestamoId)
                .ToListAsync();
        }

        public async Task<List<Inspeccion>> GetByEjemplarIdAsync(int ejemplarId)
        {
            return await _db.Inspecciones
                .Include(i => i.TipoInspeccion)
                .Include(i => i.InspeccionadoPor)
                .Where(i => i.EjemplarId == ejemplarId)
                .ToListAsync();
        }

        public async Task<Inspeccion?> GetInspeccionWithDetailsAsync(int id)
        {
            return await _db.Inspecciones
                .Include(i => i.TipoInspeccion)
                .Include(i => i.Ejemplar)
                .Include(i => i.Prestamo)
                .Include(i => i.InspeccionadoPor)
                .FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
