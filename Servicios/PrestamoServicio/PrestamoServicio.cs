using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.PrestamoDto;
using BookBackend.Repositorio;
using BookBackend.Repositorio.EjemplarRepositorio;
using BookBackend.Repositorio.PrestamoRepositorio;

namespace BookBackend.Servicios.PrestamoServicio
{
    public class PrestamoServicio : IPrestamoServicio
    {
        private readonly IPrestamoRepositorio _prestamoRepo;
        private readonly IEjemplarRepositorio _ejemplarRepo;
        private readonly IRepositorio<DetallePrestamo> _detalleRepo;

        public PrestamoServicio(
            IPrestamoRepositorio prestamoRepo,
            IEjemplarRepositorio ejemplarRepo,
            IRepositorio<DetallePrestamo> detalleRepo)
        {
            _prestamoRepo = prestamoRepo;
            _ejemplarRepo = ejemplarRepo;
            _detalleRepo = detalleRepo;
        }

        public async Task<List<PrestamoRespuestaDto>> GetAllAsync()
        {
            var prestamos = await _prestamoRepo.GetAllAsync(incluirPropiedades: "Usuario,EstadoPrestamo,DetallePrestamos.Ejemplar");
            return prestamos.Select(MapToDto).ToList();
        }

        public async Task<PrestamoRespuestaDto?> GetByIdAsync(int id)
        {
            var prestamo = await _prestamoRepo.GetPrestamoWithDetailsAsync(id);
            return prestamo is null ? null : MapToDto(prestamo);
        }

        public async Task<List<PrestamoRespuestaDto>> GetByUsuarioIdAsync(int usuarioId)
        {
            var prestamos = await _prestamoRepo.GetByUsuarioIdAsync(usuarioId);
            return prestamos.Select(MapToDto).ToList();
        }

        public async Task<List<PrestamoRespuestaDto>> GetActivosAsync()
        {
            var prestamos = await _prestamoRepo.GetActivosAsync();
            return prestamos.Select(MapToDto).ToList();
        }

        public async Task<List<PrestamoRespuestaDto>> GetVencidosAsync()
        {
            var prestamos = await _prestamoRepo.GetVencidosAsync();
            return prestamos.Select(MapToDto).ToList();
        }

        public async Task<PrestamoRespuestaDto> CreateAsync(CrearPrestamoDto crearDto)
        {
            var prestamo = new Prestamo
            {
                UsuarioId = crearDto.UsuarioId,
                FechaPrestamo = DateTime.UtcNow,
                FechaDevolucionEsperada = crearDto.FechaDevolucionEsperada,
                EstadoPrestamoId = 1
            };

            await _prestamoRepo.AddAsync(prestamo);

            foreach (var ejemplarId in crearDto.EjemplarIds)
            {
                var ejemplar = await _ejemplarRepo.GetByIdAsync(ejemplarId);
                if (ejemplar is not null && ejemplar.EstadoEjemplarId == 1)
                {
                    await _detalleRepo.AddAsync(new DetallePrestamo
                    {
                        PrestamoId = prestamo.Id,
                        EjemplarId = ejemplarId
                    });

                    ejemplar.EstadoEjemplarId = 2;
                    await _ejemplarRepo.UpdateAsync(ejemplar);
                }
            }

            var creado = await _prestamoRepo.GetPrestamoWithDetailsAsync(prestamo.Id);
            return MapToDto(creado!);
        }

        public async Task<PrestamoRespuestaDto?> DevolverAsync(int id, DateTime fechaDevolucion)
        {
            var prestamo = await _prestamoRepo.GetPrestamoWithDetailsAsync(id);
            if (prestamo is null || prestamo.EstadoPrestamoId != 1)
                return null;

            prestamo.EstadoPrestamoId = 2;
            prestamo.FechaDevolucionReal = fechaDevolucion;
            await _prestamoRepo.UpdateAsync(prestamo);

            foreach (var detalle in prestamo.DetallePrestamos)
            {
                var ejemplar = await _ejemplarRepo.GetByIdAsync(detalle.EjemplarId);
                if (ejemplar is not null)
                {
                    ejemplar.EstadoEjemplarId = 1;
                    await _ejemplarRepo.UpdateAsync(ejemplar);
                }
            }

            return MapToDto(prestamo);
        }

        public async Task<PrestamoRespuestaDto?> CancelarAsync(int id)
        {
            var prestamo = await _prestamoRepo.GetPrestamoWithDetailsAsync(id);
            if (prestamo is null || prestamo.EstadoPrestamoId != 1)
                return null;

            prestamo.EstadoPrestamoId = 4;
            await _prestamoRepo.UpdateAsync(prestamo);

            foreach (var detalle in prestamo.DetallePrestamos)
            {
                var ejemplar = await _ejemplarRepo.GetByIdAsync(detalle.EjemplarId);
                if (ejemplar is not null)
                {
                    ejemplar.EstadoEjemplarId = 1;
                    await _ejemplarRepo.UpdateAsync(ejemplar);
                }
            }

            return MapToDto(prestamo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var prestamo = await _prestamoRepo.GetPrestamoWithDetailsAsync(id);
            if (prestamo is null) return false;

            foreach (var detalle in prestamo.DetallePrestamos)
                await _detalleRepo.DeleteAsync(detalle);

            await _prestamoRepo.DeleteAsync(prestamo);
            return true;
        }

        private static PrestamoRespuestaDto MapToDto(Prestamo prestamo)
        {
            return new PrestamoRespuestaDto
            {
                Id = prestamo.Id,
                UsuarioId = prestamo.UsuarioId,
                UsuarioNombre = prestamo.Usuario?.Nombre,
                UsuarioEmail = prestamo.Usuario?.Email,
                FechaPrestamo = prestamo.FechaPrestamo,
                FechaDevolucionEsperada = prestamo.FechaDevolucionEsperada,
                FechaDevolucionReal = prestamo.FechaDevolucionReal,
                EstadoPrestamoId = prestamo.EstadoPrestamoId,
                EstadoPrestamo = prestamo.EstadoPrestamo?.Nombre,
                DetallePrestamos = prestamo.DetallePrestamos?.Select(dp => new DetallePrestamoResumenDto
                {
                    Id = dp.Id,
                    EjemplarId = dp.EjemplarId,
                    CodigoBarras = dp.Ejemplar?.CodigoBarras
                }).ToList() ?? new List<DetallePrestamoResumenDto>()
            };
        }
    }
}
