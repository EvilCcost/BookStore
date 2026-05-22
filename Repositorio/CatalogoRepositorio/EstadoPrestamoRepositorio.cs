using BookBackend.Data;
using BookBackend.Modelos;

namespace BookBackend.Repositorio.CatalogoRepositorio
{
    public class EstadoPrestamoRepositorio : Repositorio<EstadoPrestamo>, IEstadoPrestamoRepositorio
    {
        public EstadoPrestamoRepositorio(BookStoreContext db) : base(db)
        {
        }
    }
}
