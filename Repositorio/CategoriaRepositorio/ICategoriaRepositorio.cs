using BookBackend.Modelos;

namespace BookBackend.Repositorio.CategoriaRepositorio
{
    public interface ICategoriaRepositorio : IRepositorio<Categoria>
    {
        Task<List<Categoria>> SearchByNombreAsync(string nombre);
    }
}
