using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.RolDto;
using BookBackend.Repositorio.RolRepositorio;

namespace BookBackend.Servicios.RolServicio
{
    public class RolServicio : IRolServicio
    {
        private readonly IRolRepositorio _rolRepo;

        public RolServicio(IRolRepositorio rolRepo)
        {
            _rolRepo = rolRepo;
        }

        public async Task<List<RolRespuestaDto>> GetAllAsync()
        {
            var roles = await _rolRepo.GetAllAsync();
            return roles.Select(MapToDto).ToList();
        }

        public async Task<RolRespuestaDto?> GetByIdAsync(int id)
        {
            var rol = await _rolRepo.GetByIdAsync(id);
            return rol is null ? null : MapToDto(rol);
        }

        public async Task<RolRespuestaDto> CreateAsync(CrearRolDto crearDto)
        {
            var existe = await _rolRepo.ExistsAsync(r => r.Nombre == crearDto.Nombre);
            if (existe)
                throw new InvalidOperationException("El rol ya existe.");

            var rol = new Rol
            {
                Nombre = crearDto.Nombre,
                Descripcion = crearDto.Descripcion
            };

            await _rolRepo.AddAsync(rol);
            return MapToDto(rol);
        }

        public async Task<RolRespuestaDto?> UpdateAsync(int id, ActualizarRolDto actualizarDto)
        {
            var rol = await _rolRepo.GetByIdAsync(id);
            if (rol is null) return null;

            var nombreDuplicado = await _rolRepo.ExistsAsync(r => r.Nombre == actualizarDto.Nombre && r.Id != id);
            if (nombreDuplicado)
                throw new InvalidOperationException("El nombre ya está en uso por otro rol.");

            rol.Nombre = actualizarDto.Nombre;
            rol.Descripcion = actualizarDto.Descripcion;
            await _rolRepo.UpdateAsync(rol);
            return MapToDto(rol);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var rol = await _rolRepo.GetByIdAsync(id);
            if (rol is null) return false;

            await _rolRepo.DeleteAsync(rol);
            return true;
        }

        private static RolRespuestaDto MapToDto(Rol rol)
        {
            return new RolRespuestaDto
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion
            };
        }
    }
}
