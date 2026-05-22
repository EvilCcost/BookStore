using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.VentaDto;
using BookBackend.Repositorio;
using BookBackend.Repositorio.EjemplarRepositorio;
using BookBackend.Repositorio.VentaRepositorio;

namespace BookBackend.Servicios.VentaServicio
{
    public class VentaServicio : IVentaServicio
    {
        private readonly IVentaRepositorio _ventaRepo;
        private readonly IEjemplarRepositorio _ejemplarRepo;
        private readonly IRepositorio<DetalleVenta> _detalleRepo;

        public VentaServicio(
            IVentaRepositorio ventaRepo,
            IEjemplarRepositorio ejemplarRepo,
            IRepositorio<DetalleVenta> detalleRepo)
        {
            _ventaRepo = ventaRepo;
            _ejemplarRepo = ejemplarRepo;
            _detalleRepo = detalleRepo;
        }

        public async Task<List<VentaRespuestaDto>> GetAllAsync()
        {
            var ventas = await _ventaRepo.GetAllAsync(incluirPropiedades: "Usuario,EstadoVenta,MetodoPago,DetalleVentas.Ejemplar");
            return ventas.Select(MapToDto).ToList();
        }

        public async Task<VentaRespuestaDto?> GetByIdAsync(int id)
        {
            var venta = await _ventaRepo.GetVentaWithDetailsAsync(id);
            return venta is null ? null : MapToDto(venta);
        }

        public async Task<List<VentaRespuestaDto>> GetByUsuarioIdAsync(int? usuarioId)
        {
            var ventas = await _ventaRepo.GetByUsuarioIdAsync(usuarioId);
            return ventas.Select(MapToDto).ToList();
        }

        public async Task<List<VentaRespuestaDto>> GetByFechaRangeAsync(DateTime desde, DateTime hasta)
        {
            var ventas = await _ventaRepo.GetByFechaRangeAsync(desde, hasta);
            return ventas.Select(MapToDto).ToList();
        }

        public async Task<VentaRespuestaDto> CreateAsync(CrearVentaDto crearDto)
        {
            decimal total = 0;
            foreach (var detalle in crearDto.Detalles)
                total += detalle.PrecioUnitario;

            var venta = new Venta
            {
                UsuarioId = crearDto.UsuarioId,
                FechaVenta = DateTime.UtcNow,
                Total = total,
                EstadoVentaId = 1,
                MetodoPagoId = crearDto.MetodoPagoId,
                Comprobante = crearDto.Comprobante
            };

            await _ventaRepo.AddAsync(venta);

            foreach (var detalleDto in crearDto.Detalles)
            {
                await _detalleRepo.AddAsync(new DetalleVenta
                {
                    VentaId = venta.Id,
                    EjemplarId = detalleDto.EjemplarId,
                    PrecioUnitario = detalleDto.PrecioUnitario
                });

                var ejemplar = await _ejemplarRepo.GetByIdAsync(detalleDto.EjemplarId);
                if (ejemplar is not null)
                {
                    ejemplar.EstadoEjemplarId = 5;
                    await _ejemplarRepo.UpdateAsync(ejemplar);
                }
            }

            var creada = await _ventaRepo.GetVentaWithDetailsAsync(venta.Id);
            return MapToDto(creada!);
        }

        public async Task<VentaRespuestaDto?> CancelarAsync(int id)
        {
            var venta = await _ventaRepo.GetVentaWithDetailsAsync(id);
            if (venta is null || venta.EstadoVentaId != 1)
                return null;

            venta.EstadoVentaId = 2;
            await _ventaRepo.UpdateAsync(venta);

            foreach (var detalle in venta.DetalleVentas)
            {
                var ejemplar = await _ejemplarRepo.GetByIdAsync(detalle.EjemplarId);
                if (ejemplar is not null)
                {
                    ejemplar.EstadoEjemplarId = 1;
                    await _ejemplarRepo.UpdateAsync(ejemplar);
                }
            }

            return MapToDto(venta);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var venta = await _ventaRepo.GetVentaWithDetailsAsync(id);
            if (venta is null) return false;

            foreach (var detalle in venta.DetalleVentas)
                await _detalleRepo.DeleteAsync(detalle);

            await _ventaRepo.DeleteAsync(venta);
            return true;
        }

        private static VentaRespuestaDto MapToDto(Venta venta)
        {
            return new VentaRespuestaDto
            {
                Id = venta.Id,
                UsuarioId = venta.UsuarioId,
                UsuarioNombre = venta.Usuario?.Nombre,
                FechaVenta = venta.FechaVenta,
                Total = venta.Total,
                EstadoVentaId = venta.EstadoVentaId,
                EstadoVenta = venta.EstadoVenta?.Nombre,
                MetodoPagoId = venta.MetodoPagoId,
                MetodoPago = venta.MetodoPago?.Nombre,
                Comprobante = venta.Comprobante,
                DetalleVentas = venta.DetalleVentas?.Select(dv => new DetalleVentaResumenDto
                {
                    Id = dv.Id,
                    EjemplarId = dv.EjemplarId,
                    CodigoBarras = dv.Ejemplar?.CodigoBarras,
                    PrecioUnitario = dv.PrecioUnitario
                }).ToList() ?? new List<DetalleVentaResumenDto>()
            };
        }
    }
}
