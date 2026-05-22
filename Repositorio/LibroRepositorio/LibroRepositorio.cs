using BookBackend.Data;
using BookBackend.Modelos;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio.LibroRepositorio
{
    public class LibroRepositorio : Repositorio<Libro>, ILibroRepositorio
    {
        private readonly BookStoreContext _db;

        public LibroRepositorio(BookStoreContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Libro?> GetLibroWithDetailsAsync(int id)
        {
            return await _db.Libros
                .Include(l => l.LibroAutores)
                    .ThenInclude(la => la.Autor)
                .Include(l => l.LibroCategorias)
                    .ThenInclude(lc => lc.Categoria)
                .Include(l => l.Ejemplares)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<List<Libro>> SearchByTituloAsync(string titulo)
        {
            return await _db.Libros
                .Where(l => l.Titulo.Contains(titulo))
                .Include(l => l.LibroAutores)
                    .ThenInclude(la => la.Autor)
                .Include(l => l.LibroCategorias)
                    .ThenInclude(lc => lc.Categoria)
                .Include(l => l.Ejemplares)
                .ToListAsync();
        }
    }
}
