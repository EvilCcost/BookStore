using BookBackend.Modelos;
using BookBackend.Modelos.DTOs;
using BookBackend.Modelos.DTOs.LibroDto;
using BookBackend.Repositorio;
using BookBackend.Repositorio.LibroRepositorio;

namespace BookBackend.Servicios.LibroServicio
{
    public class LibroServicio : ILibroServicio
    {
        private readonly ILibroRepositorio _libroRepo;
        private readonly IRepositorio<LibroAutor> _libroAutorRepo;
        private readonly IRepositorio<LibroCategoria> _libroCategoriaRepo;
        private readonly IRepositorio<Autor> _autorRepo;
        private readonly IRepositorio<Categoria> _categoriaRepo;
        private readonly IRepositorio<Ejemplar> _ejemplarRepo;

        public LibroServicio(
            ILibroRepositorio libroRepo,
            IRepositorio<LibroAutor> libroAutorRepo,
            IRepositorio<LibroCategoria> libroCategoriaRepo,
            IRepositorio<Autor> autorRepo,
            IRepositorio<Categoria> categoriaRepo,
            IRepositorio<Ejemplar> ejemplarRepo)
        {
            _libroRepo = libroRepo;
            _libroAutorRepo = libroAutorRepo;
            _libroCategoriaRepo = libroCategoriaRepo;
            _autorRepo = autorRepo;
            _categoriaRepo = categoriaRepo;
            _ejemplarRepo = ejemplarRepo;
        }

        public async Task<List<LibroRespuestaDto>> GetAllAsync()
        {
            var libros = await _libroRepo.GetAllAsync(incluirPropiedades: "LibroAutores.Autor,LibroCategorias.Categoria,Ejemplares");
            return libros.Select(MapToDto).ToList();
        }

        public async Task<LibroRespuestaDto?> GetByIdAsync(int id)
        {
            var libro = await _libroRepo.GetLibroWithDetailsAsync(id);
            return libro is null ? null : MapToDto(libro);
        }

        public async Task<List<LibroRespuestaDto>> SearchByTituloAsync(string titulo)
        {
            var libros = await _libroRepo.SearchByTituloAsync(titulo);
            return libros.Select(MapToDto).ToList();
        }

        public async Task<LibroRespuestaDto> CreateAsync(CrearLibroDto crearDto)
        {
            if (crearDto.Isbn is not null)
            {
                var isbnExiste = await _libroRepo.ExistsAsync(l => l.Isbn == crearDto.Isbn);
                if (isbnExiste)
                    throw new InvalidOperationException("El ISBN ya está registrado.");
            }

            var libro = new Libro
            {
                Titulo = crearDto.Titulo,
                Isbn = crearDto.Isbn,
                Editorial = crearDto.Editorial,
                AnioPublicacion = crearDto.AnioPublicacion
            };

            await _libroRepo.AddAsync(libro);

            foreach (var autorId in crearDto.AutorIds)
            {
                var autorExiste = await _autorRepo.ExistsAsync(a => a.Id == autorId);
                if (autorExiste)
                {
                    await _libroAutorRepo.AddAsync(new LibroAutor
                    {
                        LibroId = libro.Id,
                        AutorId = autorId
                    });
                }
            }

            foreach (var categoriaId in crearDto.CategoriaIds)
            {
                var catExiste = await _categoriaRepo.ExistsAsync(c => c.Id == categoriaId);
                if (catExiste)
                {
                    await _libroCategoriaRepo.AddAsync(new LibroCategoria
                    {
                        LibroId = libro.Id,
                        CategoriaId = categoriaId
                    });
                }
            }

            if (crearDto.CantidadEjemplares > 0)
            {
                for (int i = 0; i < crearDto.CantidadEjemplares; i++)
                {
                    await _ejemplarRepo.AddAsync(new Ejemplar
                    {
                        LibroId = libro.Id,
                        CodigoBarras = $"{libro.Id}-{i + 1:D3}",
                        EstadoEjemplarId = 1,
                        CondicionEjemplarId = 1
                    });
                }
            }

            var libroCompleto = await _libroRepo.GetLibroWithDetailsAsync(libro.Id);
            return MapToDto(libroCompleto!);
        }

        public async Task<LibroRespuestaDto?> UpdateAsync(int id, ActualizarLibroDto actualizarDto)
        {
            var libro = await _libroRepo.GetByIdAsync(id);
            if (libro is null) return null;

            if (actualizarDto.Isbn is not null)
            {
                var isbnExiste = await _libroRepo.ExistsAsync(l => l.Isbn == actualizarDto.Isbn && l.Id != id);
                if (isbnExiste)
                    throw new InvalidOperationException("El ISBN ya está en uso por otro libro.");
            }

            libro.Titulo = actualizarDto.Titulo;
            libro.Isbn = actualizarDto.Isbn;
            libro.Editorial = actualizarDto.Editorial;
            libro.AnioPublicacion = actualizarDto.AnioPublicacion;

            await _libroRepo.UpdateAsync(libro);

            var autoresActuales = await _libroAutorRepo.GetAllAsync(la => la.LibroId == id);
            foreach (var item in autoresActuales)
                await _libroAutorRepo.DeleteAsync(item);

            foreach (var autorId in actualizarDto.AutorIds)
            {
                var autorExiste = await _autorRepo.ExistsAsync(a => a.Id == autorId);
                if (autorExiste)
                {
                    await _libroAutorRepo.AddAsync(new LibroAutor
                    {
                        LibroId = id,
                        AutorId = autorId
                    });
                }
            }

            var categoriasActuales = await _libroCategoriaRepo.GetAllAsync(lc => lc.LibroId == id);
            foreach (var item in categoriasActuales)
                await _libroCategoriaRepo.DeleteAsync(item);

            foreach (var categoriaId in actualizarDto.CategoriaIds)
            {
                var catExiste = await _categoriaRepo.ExistsAsync(c => c.Id == categoriaId);
                if (catExiste)
                {
                    await _libroCategoriaRepo.AddAsync(new LibroCategoria
                    {
                        LibroId = id,
                        CategoriaId = categoriaId
                    });
                }
            }

            var libroCompleto = await _libroRepo.GetLibroWithDetailsAsync(id);
            return MapToDto(libroCompleto!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var libro = await _libroRepo.GetByIdAsync(id);
            if (libro is null) return false;

            var ejemplares = await _ejemplarRepo.GetAllAsync(e => e.LibroId == id);
            foreach (var ejemplar in ejemplares)
                await _ejemplarRepo.DeleteAsync(ejemplar);

            var autores = await _libroAutorRepo.GetAllAsync(la => la.LibroId == id);
            foreach (var item in autores)
                await _libroAutorRepo.DeleteAsync(item);

            var categorias = await _libroCategoriaRepo.GetAllAsync(lc => lc.LibroId == id);
            foreach (var item in categorias)
                await _libroCategoriaRepo.DeleteAsync(item);

            await _libroRepo.DeleteAsync(libro);
            return true;
        }

        private static LibroRespuestaDto MapToDto(Libro libro)
        {
            return new LibroRespuestaDto
            {
                Id = libro.Id,
                Titulo = libro.Titulo,
                Isbn = libro.Isbn,
                Editorial = libro.Editorial,
                AnioPublicacion = libro.AnioPublicacion,
                CantidadEjemplares = libro.CantidadEjemplares,
                EjemplaresDisponibles = libro.EjemplaresDisponibles,
                Autores = libro.LibroAutores?.Select(la => new AutorResumenDto
                {
                    Id = la.Autor.Id,
                    Nombre = la.Autor.Nombre
                }).ToList() ?? new List<AutorResumenDto>(),
                Categorias = libro.LibroCategorias?.Select(lc => new CategoriaResumenDto
                {
                    Id = lc.Categoria.Id,
                    Nombre = lc.Categoria.Nombre
                }).ToList() ?? new List<CategoriaResumenDto>()
            };
        }
    }
}
