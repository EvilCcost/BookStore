using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.AutorDto;
using BookBackend.Repositorio.AutorRepositorio;

namespace BookBackend.Servicios.AutorServicio
{
    public class AutorServicio : IAutorServicio
    {
        private readonly IAutorRepositorio _autorRepo;

        public AutorServicio(IAutorRepositorio autorRepo)
        {
            _autorRepo = autorRepo;
        }

        public async Task<List<AutorRespuestaDto>> GetAllAsync()
        {
            var autores = await _autorRepo.GetAllAsync();
            return autores.Select(MapToDto).ToList();
        }

        public async Task<AutorRespuestaDto?> GetByIdAsync(int id)
        {
            var autor = await _autorRepo.GetByIdAsync(id);
            return autor is null ? null : MapToDto(autor);
        }

        public async Task<List<AutorRespuestaDto>> SearchByNombreAsync(string nombre)
        {
            var autores = await _autorRepo.SearchByNombreAsync(nombre);
            return autores.Select(MapToDto).ToList();
        }

        public async Task<AutorRespuestaDto> CreateAsync(CrearAutorDto crearDto)
        {
            var existe = await _autorRepo.ExistsAsync(a => a.Nombre == crearDto.Nombre);
            if (existe)
                throw new InvalidOperationException("El autor ya existe.");

            var autor = new Autor
            {
                Nombre = crearDto.Nombre,
                Nacionalidad = crearDto.Nacionalidad
            };

            await _autorRepo.AddAsync(autor);
            return MapToDto(autor);
        }

        public async Task<AutorRespuestaDto?> UpdateAsync(int id, ActualizarAutorDto actualizarDto)
        {
            var autor = await _autorRepo.GetByIdAsync(id);
            if (autor is null) return null;

            var nombreDuplicado = await _autorRepo.ExistsAsync(a => a.Nombre == actualizarDto.Nombre && a.Id != id);
            if (nombreDuplicado)
                throw new InvalidOperationException("El nombre ya está en uso por otro autor.");

            autor.Nombre = actualizarDto.Nombre;
            autor.Nacionalidad = actualizarDto.Nacionalidad;

            await _autorRepo.UpdateAsync(autor);
            return MapToDto(autor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var autor = await _autorRepo.GetByIdAsync(id);
            if (autor is null) return false;

            await _autorRepo.DeleteAsync(autor);
            return true;
        }

        private static AutorRespuestaDto MapToDto(Autor autor)
        {
            return new AutorRespuestaDto
            {
                Id = autor.Id,
                Nombre = autor.Nombre,
                Nacionalidad = autor.Nacionalidad
            };
        }
    }
}
