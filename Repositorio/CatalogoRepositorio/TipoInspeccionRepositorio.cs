using BookBackend.Data;
using BookBackend.Modelos;

namespace BookBackend.Repositorio.CatalogoRepositorio
{
    public class TipoInspeccionRepositorio : Repositorio<TipoInspeccion>, ITipoInspeccionRepositorio
    {
        public TipoInspeccionRepositorio(BookStoreContext db) : base(db)
        {
        }
    }
}
