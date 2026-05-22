using BookBackend.Data;
using BookBackend.Modelos;

namespace BookBackend.Repositorio.CatalogoRepositorio
{
    public class EstadoReservaRepositorio : Repositorio<EstadoReserva>, IEstadoReservaRepositorio
    {
        public EstadoReservaRepositorio(BookStoreContext db) : base(db)
        {
        }
    }
}
