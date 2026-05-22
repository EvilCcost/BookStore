using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.MultaDto;
using BookBackend.Repositorio.MultaRepositorio;

namespace BookBackend.Servicios.MultaServicio
{
    public class MultaServicio : IMultaServicio
    {
        private readonly IMultaRepositorio _multaRepo;

        public MultaServicio(IMultaRepositorio multaRepo)
        {
            _multaRepo = multaRepo;
        }

        public async Task<List<MultaRespuestaDto>> GetAllAsync()
        {
            var multas = await _multaRepo.GetAllAsync(incluirPropiedades: "Usuario,Prestamo");
            return multas.Select(MapToDto).ToList();
        }

        public async Task<MultaRespuestaDto?> GetByIdAsync(int id)
        {
            var multa = await _multaRepo.GetByIdAsync(id);
            return multa is null ? null : MapToDto(multa);
        }

        public async Task<List<MultaRespuestaDto>> GetByUsuarioIdAsync(int usuarioId)
        {
            var multas = await _multaRepo.GetByUsuarioIdAsync(usuarioId);
            return multas.Select(MapToDto).ToList();
        }

        public async Task<List<MultaRespuestaDto>> GetPendientesAsync()
        {
            var multas = await _multaRepo.GetPendientesAsync();
            return multas.Select(MapToDto).ToList();
        }

        public async Task<MultaRespuestaDto> CreateAsync(CrearMultaDto crearDto)
        {
            var multa = new Multa
            {
                UsuarioId = crearDto.UsuarioId,
                PrestamoId = crearDto.PrestamoId,
                Monto = crearDto.Monto,
                Motivo = crearDto.Motivo,
                Pagada = false,
                FechaEmision = DateTime.UtcNow
            };

            await _multaRepo.AddAsync(multa);
            return MapToDto(multa);
        }

        public async Task<MultaRespuestaDto?> PagarAsync(int id)
        {
            var multa = await _multaRepo.GetByIdAsync(id);
            if (multa is null) return null;

            multa.Pagada = true;
            await _multaRepo.UpdateAsync(multa);
            return MapToDto(multa);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var multa = await _multaRepo.GetByIdAsync(id);
            if (multa is null) return false;

            await _multaRepo.DeleteAsync(multa);
            return true;
        }

        private static MultaRespuestaDto MapToDto(Multa multa)
        {
            return new MultaRespuestaDto
            {
                Id = multa.Id,
                UsuarioId = multa.UsuarioId,
                UsuarioNombre = multa.Usuario?.Nombre,
                PrestamoId = multa.PrestamoId,
                Monto = multa.Monto,
                Motivo = multa.Motivo,
                Pagada = multa.Pagada,
                FechaEmision = multa.FechaEmision
            };
        }
    }
}
