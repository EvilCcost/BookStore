using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.VentaRepositorio
{
    public class VentaRepositorio : Repositorio<Venta>, IVentaRepositorio
    {
        private readonly BookStoreContext _db;

        public VentaRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Venta?> GetVentaWithDetailsAsync(int id)
        {
            return await _db.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.EstadoVenta)
                .Include(v => v.MetodoPago)
                .Include(v => v.DetalleVentas)
                    .ThenInclude(dv => dv.Ejemplar)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Venta>> GetByUsuarioIdAsync(int? usuarioId)
        {
            return await _db.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.EstadoVenta)
                .Include(v => v.MetodoPago)
                .Include(v => v.DetalleVentas)
                    .ThenInclude(dv => dv.Ejemplar)
                .Where(v => v.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<List<Venta>> GetByFechaRangeAsync(DateTime desde, DateTime hasta)
        {
            return await _db.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.EstadoVenta)
                .Include(v => v.MetodoPago)
                .Include(v => v.DetalleVentas)
                    .ThenInclude(dv => dv.Ejemplar)
                .Where(v => v.FechaVenta >= desde && v.FechaVenta <= hasta)
                .ToListAsync();
        }
    }
}
