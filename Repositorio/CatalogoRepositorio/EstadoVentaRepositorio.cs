using BookBackend.Data;
using BookBackend.Modelos;

namespace BookBackend.Repositorio.CatalogoRepositorio
{
    public class EstadoVentaRepositorio : Repositorio<EstadoVenta>, IEstadoVentaRepositorio
    {
        public EstadoVentaRepositorio(BookStoreContext db) : base(db)
        {
        }
    }
}
