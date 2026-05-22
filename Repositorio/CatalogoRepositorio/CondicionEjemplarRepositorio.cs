using BookBackend.Data;
using BookBackend.Modelos;

namespace BookBackend.Repositorio.CatalogoRepositorio
{
    public class CondicionEjemplarRepositorio : Repositorio<CondicionEjemplar>, ICondicionEjemplarRepositorio
    {
        public CondicionEjemplarRepositorio(BookStoreContext db) : base(db)
        {
        }
    }
}
