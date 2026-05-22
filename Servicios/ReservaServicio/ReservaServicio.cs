using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.ReservaDto;
using BookBackend.Repositorio.ReservaRepositorio;

namespace BookBackend.Servicios.ReservaServicio
{
    public class ReservaServicio : IReservaServicio
    {
        private readonly IReservaRepositorio _reservaRepo;

        public ReservaServicio(IReservaRepositorio reservaRepo)
        {
            _reservaRepo = reservaRepo;
        }

        public async Task<List<ReservaRespuestaDto>> GetAllAsync()
        {
            var reservas = await _reservaRepo.GetAllAsync(incluirPropiedades: "Usuario,Libro,EstadoReserva");
            return reservas.Select(MapToDto).ToList();
        }

        public async Task<ReservaRespuestaDto?> GetByIdAsync(int id)
        {
            var reserva = await _reservaRepo.GetReservaWithDetailsAsync(id);
            return reserva is null ? null : MapToDto(reserva);
        }

        public async Task<List<ReservaRespuestaDto>> GetByUsuarioIdAsync(int usuarioId)
        {
            var reservas = await _reservaRepo.GetByUsuarioIdAsync(usuarioId);
            return reservas.Select(MapToDto).ToList();
        }

        public async Task<List<ReservaRespuestaDto>> GetPendientesAsync()
        {
            var reservas = await _reservaRepo.GetPendientesAsync();
            return reservas.Select(MapToDto).ToList();
        }

        public async Task<ReservaRespuestaDto> CreateAsync(CrearReservaDto crearDto)
        {
            var pendiente = await _reservaRepo.ExistsAsync(r =>
                r.UsuarioId == crearDto.UsuarioId &&
                r.LibroId == crearDto.LibroId &&
                r.EstadoReservaId == 1);

            if (pendiente)
                throw new InvalidOperationException("Ya tienes una reserva pendiente para este libro.");

            var reserva = new Reserva
            {
                UsuarioId = crearDto.UsuarioId,
                LibroId = crearDto.LibroId,
                FechaReserva = DateTime.UtcNow,
                FechaExpiracion = crearDto.FechaExpiracion,
                EstadoReservaId = 1
            };

            await _reservaRepo.AddAsync(reserva);
            var creada = await _reservaRepo.GetReservaWithDetailsAsync(reserva.Id);
            return MapToDto(creada!);
        }

        public async Task<ReservaRespuestaDto?> CumplirAsync(int id)
        {
            var reserva = await _reservaRepo.GetReservaWithDetailsAsync(id);
            if (reserva is null || reserva.EstadoReservaId != 1)
                return null;

            reserva.EstadoReservaId = 2;
            await _reservaRepo.UpdateAsync(reserva);
            return MapToDto(reserva);
        }

        public async Task<ReservaRespuestaDto?> CancelarAsync(int id)
        {
            var reserva = await _reservaRepo.GetReservaWithDetailsAsync(id);
            if (reserva is null || reserva.EstadoReservaId != 1)
                return null;

            reserva.EstadoReservaId = 3;
            await _reservaRepo.UpdateAsync(reserva);
            return MapToDto(reserva);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reserva = await _reservaRepo.GetByIdAsync(id);
            if (reserva is null) return false;

            await _reservaRepo.DeleteAsync(reserva);
            return true;
        }

        private static ReservaRespuestaDto MapToDto(Reserva reserva)
        {
            return new ReservaRespuestaDto
            {
                Id = reserva.Id,
                UsuarioId = reserva.UsuarioId,
                UsuarioNombre = reserva.Usuario?.Nombre,
                UsuarioEmail = reserva.Usuario?.Email,
                LibroId = reserva.LibroId,
                LibroTitulo = reserva.Libro?.Titulo,
                FechaReserva = reserva.FechaReserva,
                FechaExpiracion = reserva.FechaExpiracion,
                EstadoReservaId = reserva.EstadoReservaId,
                EstadoReserva = reserva.EstadoReserva?.Nombre
            };
        }
    }
}
