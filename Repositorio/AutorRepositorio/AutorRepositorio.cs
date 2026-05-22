using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.AutorRepositorio
{
    public class AutorRepositorio : Repositorio<Autor>, IAutorRepositorio
    {
        private readonly BookStoreContext _db;

        public AutorRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<List<Autor>> SearchByNombreAsync(string nombre)
        {
            return await _db.Autores
                .Where(a => a.Nombre.Contains(nombre))
                .ToListAsync();
        }

        public async Task<Autor?> GetAutorWithLibrosAsync(int id)
        {
            return await _db.Autores
                .Include(a => a.LibroAutores)
                    .ThenInclude(la => la.Libro)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
