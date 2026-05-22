using BookBackend.Modelos;

namespace BookBackend.Repositorio.InspeccionRepositorio
{
    public interface IInspeccionRepositorio : IRepositorio<Inspeccion>
    {
        Task<List<Inspeccion>> GetByPrestamoIdAsync(int prestamoId);
        Task<List<Inspeccion>> GetByEjemplarIdAsync(int ejemplarId);
        Task<Inspeccion?> GetInspeccionWithDetailsAsync(int id);
    }
}
