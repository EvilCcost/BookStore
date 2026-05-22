using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.CategoriaRepositorio
{
    public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
    {
        private readonly BookStoreContext _db;

        public CategoriaRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Categoria>> SearchByNombreAsync(string nombre)
        {
            return await _db.Categorias
                .Where(c => c.Nombre.Contains(nombre))
                .ToListAsync();
        }
    }
}
