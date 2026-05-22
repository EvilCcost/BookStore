using BookBackend.Modelos;

namespace BookBackend.Repositorio.VentaRepositorio
{
    public interface IVentaRepositorio : IRepositorio<Venta>
    {
        Task<Venta?> GetVentaWithDetailsAsync(int id);
        Task<List<Venta>> GetByUsuarioIdAsync(int? usuarioId);
        Task<List<Venta>> GetByFechaRangeAsync(DateTime desde, DateTime hasta);
    }
}
