using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.InspeccionDto;
using BookBackend.Repositorio.InspeccionRepositorio;

namespace BookBackend.Servicios.InspeccionServicio
{
    public class InspeccionServicio : IInspeccionServicio
    {
        private readonly IInspeccionRepositorio _inspeccionRepo;

        public InspeccionServicio(IInspeccionRepositorio inspeccionRepo)
        {
            _inspeccionRepo = inspeccionRepo;
        }

        public async Task<List<InspeccionRespuestaDto>> GetAllAsync()
        {
            var inspecciones = await _inspeccionRepo.GetAllAsync(incluirPropiedades: "TipoInspeccion,Ejemplar,InspeccionadoPor");
            return inspecciones.Select(MapToDto).ToList();
        }

        public async Task<InspeccionRespuestaDto?> GetByIdAsync(int id)
        {
            var inspeccion = await _inspeccionRepo.GetInspeccionWithDetailsAsync(id);
            return inspeccion is null ? null : MapToDto(inspeccion);
        }

        public async Task<List<InspeccionRespuestaDto>> GetByPrestamoIdAsync(int prestamoId)
        {
            var inspecciones = await _inspeccionRepo.GetByPrestamoIdAsync(prestamoId);
            return inspecciones.Select(MapToDto).ToList();
        }

        public async Task<List<InspeccionRespuestaDto>> GetByEjemplarIdAsync(int ejemplarId)
        {
            var inspecciones = await _inspeccionRepo.GetByEjemplarIdAsync(ejemplarId);
            return inspecciones.Select(MapToDto).ToList();
        }

        public async Task<InspeccionRespuestaDto> CreateAsync(CrearInspeccionDto crearDto)
        {
            var inspeccion = new Inspeccion
            {
                PrestamoId = crearDto.PrestamoId,
                EjemplarId = crearDto.EjemplarId,
                InspeccionadoPorUsuarioId = crearDto.InspeccionadoPorUsuarioId,
                FechaInspeccion = DateTime.UtcNow,
                TipoInspeccionId = crearDto.TipoInspeccionId,
                CondicionRegistrada = crearDto.CondicionRegistrada,
                Notas = crearDto.Notas
            };

            await _inspeccionRepo.AddAsync(inspeccion);
            var creada = await _inspeccionRepo.GetInspeccionWithDetailsAsync(inspeccion.Id);
            return MapToDto(creada!);
        }

        public async Task<InspeccionRespuestaDto?> UpdateAsync(int id, ActualizarInspeccionDto actualizarDto)
        {
            var inspeccion = await _inspeccionRepo.GetInspeccionWithDetailsAsync(id);
            if (inspeccion is null) return null;

            if (actualizarDto.TipoInspeccionId.HasValue)
                inspeccion.TipoInspeccionId = actualizarDto.TipoInspeccionId.Value;

            if (actualizarDto.CondicionRegistrada is not null)
                inspeccion.CondicionRegistrada = actualizarDto.CondicionRegistrada;

            if (actualizarDto.Notas is not null)
                inspeccion.Notas = actualizarDto.Notas;

            await _inspeccionRepo.UpdateAsync(inspeccion);
            return MapToDto(inspeccion);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inspeccion = await _inspeccionRepo.GetByIdAsync(id);
            if (inspeccion is null) return false;

            await _inspeccionRepo.DeleteAsync(inspeccion);
            return true;
        }

        private static InspeccionRespuestaDto MapToDto(Inspeccion inspeccion)
        {
            return new InspeccionRespuestaDto
            {
                Id = inspeccion.Id,
                PrestamoId = inspeccion.PrestamoId,
                EjemplarId = inspeccion.EjemplarId,
                CodigoBarras = inspeccion.Ejemplar?.CodigoBarras,
                InspeccionadoPorUsuarioId = inspeccion.InspeccionadoPorUsuarioId,
                InspeccionadoPorNombre = inspeccion.InspeccionadoPor?.Nombre,
                FechaInspeccion = inspeccion.FechaInspeccion,
                TipoInspeccionId = inspeccion.TipoInspeccionId,
                TipoInspeccion = inspeccion.TipoInspeccion?.Nombre,
                CondicionRegistrada = inspeccion.CondicionRegistrada,
                Notas = inspeccion.Notas
            };
        }
    }
}
