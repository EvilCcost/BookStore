using BookBackend.Data;
using BookBackend.Modelos;

namespace BookBackend.Repositorio.CatalogoRepositorio
{
    public class MetodoPagoRepositorio : Repositorio<MetodoPago>, IMetodoPagoRepositorio
    {
        public MetodoPagoRepositorio(BookStoreContext db) : base(db)
        {
        }
    }
}
