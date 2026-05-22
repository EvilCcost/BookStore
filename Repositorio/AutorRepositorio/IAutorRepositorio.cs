using BookBackend.Modelos;

namespace BookBackend.Repositorio.AutorRepositorio
{
    public interface IAutorRepositorio : IRepositorio<Autor>
    {
        Task<List<Autor>> SearchByNombreAsync(string nombre);
        Task<Autor?> GetAutorWithLibrosAsync(int id);
    }
}
