using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.SesionDto;
using BookBackend.Repositorio.SesionRepositorio;

namespace BookBackend.Servicios.SesionServicio
{
    public class SesionServicio : ISesionServicio
    {
        private readonly ISesionRepositorio _sesionRepo;

        public SesionServicio(ISesionRepositorio sesionRepo)
        {
            _sesionRepo = sesionRepo;
        }

        public async Task<List<SesionRespuestaDto>> GetAllAsync()
        {
            var sesiones = await _sesionRepo.GetAllAsync(incluirPropiedades: "Usuario");
            return sesiones.Select(MapToDto).ToList();
        }

        public async Task<SesionRespuestaDto?> GetByIdAsync(int id)
        {
            var sesion = await _sesionRepo.GetSesionWithUserAsync(id);
            return sesion is null ? null : MapToDto(sesion);
        }

        public async Task<List<SesionRespuestaDto>> GetByUsuarioIdAsync(int usuarioId)
        {
            var sesiones = await _sesionRepo.GetByUsuarioIdAsync(usuarioId);
            return sesiones.Select(MapToDto).ToList();
        }

        public async Task<bool> RevocarAsync(int id)
        {
            var sesion = await _sesionRepo.GetByIdAsync(id);
            if (sesion is null) return false;

            sesion.Revocada = true;
            await _sesionRepo.UpdateAsync(sesion);
            return true;
        }

        private static SesionRespuestaDto MapToDto(Sesion sesion)
        {
            return new SesionRespuestaDto
            {
                Id = sesion.Id,
                UsuarioId = sesion.UsuarioId,
                UsuarioNombre = sesion.Usuario?.Nombre,
                UsuarioEmail = sesion.Usuario?.Email,
                FechaCreacion = sesion.FechaCreacion,
                FechaExpiracion = sesion.FechaExpiracion,
                Revocada = sesion.Revocada
            };
        }
    }
}
