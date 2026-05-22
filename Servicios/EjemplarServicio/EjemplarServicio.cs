using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.EjemplarDto;
using BookBackend.Repositorio.EjemplarRepositorio;

namespace BookBackend.Servicios.EjemplarServicio
{
    public class EjemplarServicio : IEjemplarServicio
    {
        private readonly IEjemplarRepositorio _ejemplarRepo;

        public EjemplarServicio(IEjemplarRepositorio ejemplarRepo)
        {
            _ejemplarRepo = ejemplarRepo;
        }

        public async Task<List<EjemplarRespuestaDto>> GetAllAsync()
        {
            var ejemplares = await _ejemplarRepo.GetAllAsync(incluirPropiedades: "EstadoEjemplar,CondicionEjemplar,Libro");
            return ejemplares.Select(MapToDto).ToList();
        }

        public async Task<EjemplarRespuestaDto?> GetByIdAsync(int id)
        {
            var ejemplar = await _ejemplarRepo.GetEjemplarWithDetailsAsync(id);
            return ejemplar is null ? null : MapToDto(ejemplar);
        }

        public async Task<EjemplarRespuestaDto?> GetByCodigoBarrasAsync(string codigoBarras)
        {
            var ejemplar = await _ejemplarRepo.GetByCodigoBarrasAsync(codigoBarras);
            return ejemplar is null ? null : MapToDto(ejemplar);
        }

        public async Task<List<EjemplarRespuestaDto>> GetByLibroIdAsync(int libroId)
        {
            var ejemplares = await _ejemplarRepo.GetByLibroIdAsync(libroId);
            return ejemplares.Select(MapToDto).ToList();
        }

        public async Task<List<EjemplarRespuestaDto>> GetDisponiblesAsync()
        {
            var ejemplares = await _ejemplarRepo.GetDisponiblesAsync();
            return ejemplares.Select(MapToDto).ToList();
        }

        public async Task<EjemplarRespuestaDto> CreateAsync(CrearEjemplarDto crearDto)
        {
            var codigoExiste = await _ejemplarRepo.ExistsAsync(e => e.CodigoBarras == crearDto.CodigoBarras);
            if (codigoExiste)
                throw new InvalidOperationException("El código de barras ya está registrado.");

            var ejemplar = new Ejemplar
            {
                LibroId = crearDto.LibroId,
                CodigoBarras = crearDto.CodigoBarras,
                UbicacionEstante = crearDto.UbicacionEstante,
                EstadoEjemplarId = crearDto.EstadoEjemplarId,
                CondicionEjemplarId = crearDto.CondicionEjemplarId
            };

            await _ejemplarRepo.AddAsync(ejemplar);
            var creado = await _ejemplarRepo.GetEjemplarWithDetailsAsync(ejemplar.Id);
            return MapToDto(creado!);
        }

        public async Task<EjemplarRespuestaDto?> UpdateAsync(int id, ActualizarEjemplarDto actualizarDto)
        {
            var ejemplar = await _ejemplarRepo.GetEjemplarWithDetailsAsync(id);
            if (ejemplar is null) return null;

            if (actualizarDto.CodigoBarras is not null)
            {
                var codigoExiste = await _ejemplarRepo.ExistsAsync(e => e.CodigoBarras == actualizarDto.CodigoBarras && e.Id != id);
                if (codigoExiste)
                    throw new InvalidOperationException("El código de barras ya está en uso.");
                ejemplar.CodigoBarras = actualizarDto.CodigoBarras;
            }

            if (actualizarDto.UbicacionEstante is not null)
                ejemplar.UbicacionEstante = actualizarDto.UbicacionEstante;

            if (actualizarDto.EstadoEjemplarId.HasValue)
                ejemplar.EstadoEjemplarId = actualizarDto.EstadoEjemplarId.Value;

            if (actualizarDto.CondicionEjemplarId.HasValue)
                ejemplar.CondicionEjemplarId = actualizarDto.CondicionEjemplarId.Value;

            await _ejemplarRepo.UpdateAsync(ejemplar);
            return MapToDto(ejemplar);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ejemplar = await _ejemplarRepo.GetByIdAsync(id);
            if (ejemplar is null) return false;

            await _ejemplarRepo.DeleteAsync(ejemplar);
            return true;
        }

        private static EjemplarRespuestaDto MapToDto(Ejemplar ejemplar)
        {
            return new EjemplarRespuestaDto
            {
                Id = ejemplar.Id,
                LibroId = ejemplar.LibroId,
                LibroTitulo = ejemplar.Libro?.Titulo,
                CodigoBarras = ejemplar.CodigoBarras,
                UbicacionEstante = ejemplar.UbicacionEstante,
                EstadoEjemplarId = ejemplar.EstadoEjemplarId,
                EstadoEjemplar = ejemplar.EstadoEjemplar?.Nombre,
                CondicionEjemplarId = ejemplar.CondicionEjemplarId,
                CondicionEjemplar = ejemplar.CondicionEjemplar?.Nombre
            };
        }
    }
}
