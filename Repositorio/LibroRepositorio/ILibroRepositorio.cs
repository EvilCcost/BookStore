using BookBackend.Modelos;

namespace BookBackend.Repositorio.LibroRepositorio
{
    public interface ILibroRepositorio : IRepositorio<Libro>
    {
        Task<Libro?> GetLibroWithDetailsAsync(int id);
        Task<List<Libro>> SearchByTituloAsync(string titulo);
    }
}
