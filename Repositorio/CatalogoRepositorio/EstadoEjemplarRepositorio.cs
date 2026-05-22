using BookBackend.Data;
using BookBackend.Modelos;

namespace BookBackend.Repositorio.CatalogoRepositorio
{
    public class EstadoEjemplarRepositorio : Repositorio<EstadoEjemplar>, IEstadoEjemplarRepositorio
    {
        public EstadoEjemplarRepositorio(BookStoreContext db) : base(db)
        {
        }
    }
}
