using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.CategoriaDto;
using BookBackend.Repositorio.CategoriaRepositorio;

namespace BookBackend.Servicios.CategoriaServicio
{
    public class CategoriaServicio : ICategoriaServicio
    {
        private readonly ICategoriaRepositorio _categoriaRepo;

        public CategoriaServicio(ICategoriaRepositorio categoriaRepo)
        {
            _categoriaRepo = categoriaRepo;
        }

        public async Task<List<CategoriaRespuestaDto>> GetAllAsync()
        {
            var categorias = await _categoriaRepo.GetAllAsync();
            return categorias.Select(MapToDto).ToList();
        }

        public async Task<CategoriaRespuestaDto?> GetByIdAsync(int id)
        {
            var categoria = await _categoriaRepo.GetByIdAsync(id);
            return categoria is null ? null : MapToDto(categoria);
        }

        public async Task<List<CategoriaRespuestaDto>> SearchByNombreAsync(string nombre)
        {
            var categorias = await _categoriaRepo.SearchByNombreAsync(nombre);
            return categorias.Select(MapToDto).ToList();
        }

        public async Task<CategoriaRespuestaDto> CreateAsync(CrearCategoriaDto crearDto)
        {
            var existe = await _categoriaRepo.ExistsAsync(c => c.Nombre == crearDto.Nombre);
            if (existe)
                throw new InvalidOperationException("La categoría ya existe.");

            var categoria = new Categoria { Nombre = crearDto.Nombre };
            await _categoriaRepo.AddAsync(categoria);
            return MapToDto(categoria);
        }

        public async Task<CategoriaRespuestaDto?> UpdateAsync(int id, ActualizarCategoriaDto actualizarDto)
        {
            var categoria = await _categoriaRepo.GetByIdAsync(id);
            if (categoria is null) return null;

            var nombreDuplicado = await _categoriaRepo.ExistsAsync(c => c.Nombre == actualizarDto.Nombre && c.Id != id);
            if (nombreDuplicado)
                throw new InvalidOperationException("El nombre ya está en uso por otra categoría.");

            categoria.Nombre = actualizarDto.Nombre;
            await _categoriaRepo.UpdateAsync(categoria);
            return MapToDto(categoria);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = await _categoriaRepo.GetByIdAsync(id);
            if (categoria is null) return false;

            await _categoriaRepo.DeleteAsync(categoria);
            return true;
        }

        private static CategoriaRespuestaDto MapToDto(Categoria categoria)
        {
            return new CategoriaRespuestaDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre
            };
        }
    }
}
