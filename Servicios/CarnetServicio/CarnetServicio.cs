using BookBackend.Modelos;
using BookBackend.Modelos.DTOs.CarnetDto;
using BookBackend.Repositorio.CarnetRepositorio;

namespace BookBackend.Servicios.CarnetServicio
{
    public class CarnetServicio : ICarnetServicio
    {
        private readonly ICarnetRepositorio _carnetRepo;

        public CarnetServicio(ICarnetRepositorio carnetRepo)
        {
            _carnetRepo = carnetRepo;
        }

        public async Task<List<CarnetRespuestaDto>> GetAllAsync()
        {
            var carnets = await _carnetRepo.GetAllAsync(incluirPropiedades: "Usuario");
            return carnets.Select(MapToDto).ToList();
        }

        public async Task<CarnetRespuestaDto?> GetByIdAsync(int id)
        {
            var carnet = await _carnetRepo.GetByIdAsync(id);
            return carnet is null ? null : MapToDto(carnet);
        }

        public async Task<CarnetRespuestaDto?> GetByCodigoAsync(string codigo)
        {
            var carnet = await _carnetRepo.GetByCodigoAsync(codigo);
            return carnet is null ? null : MapToDto(carnet);
        }

        public async Task<CarnetRespuestaDto?> GetByUsuarioIdAsync(int usuarioId)
        {
            var carnet = await _carnetRepo.GetByUsuarioIdAsync(usuarioId);
            return carnet is null ? null : MapToDto(carnet);
        }

        public async Task<List<CarnetRespuestaDto>> GetActivosAsync()
        {
            var carnets = await _carnetRepo.GetActivosAsync();
            return carnets.Select(MapToDto).ToList();
        }

        public async Task<CarnetRespuestaDto> CreateAsync(CrearCarnetDto crearDto)
        {
            var codigoExiste = await _carnetRepo.ExistsAsync(c => c.Codigo == crearDto.Codigo);
            if (codigoExiste)
                throw new InvalidOperationException("El código del carnet ya está registrado.");

            var carnet = new Carnet
            {
                UsuarioId = crearDto.UsuarioId,
                Codigo = crearDto.Codigo,
                FechaEmision = DateTime.UtcNow,
                FechaVencimiento = crearDto.FechaVencimiento,
                Activo = true
            };

            await _carnetRepo.AddAsync(carnet);
            return MapToDto(carnet);
        }

        public async Task<CarnetRespuestaDto?> RenovarAsync(int id, DateTime nuevaFechaVencimiento)
        {
            var carnet = await _carnetRepo.GetByIdAsync(id);
            if (carnet is null) return null;

            carnet.FechaVencimiento = nuevaFechaVencimiento;
            carnet.Activo = true;
            await _carnetRepo.UpdateAsync(carnet);
            return MapToDto(carnet);
        }

        public async Task<CarnetRespuestaDto?> UpdateAsync(int id, ActualizarCarnetDto actualizarDto)
        {
            var carnet = await _carnetRepo.GetByIdAsync(id);
            if (carnet is null) return null;

            if (actualizarDto.Codigo is not null)
            {
                var codigoExiste = await _carnetRepo.ExistsAsync(c => c.Codigo == actualizarDto.Codigo && c.Id != id);
                if (codigoExiste)
                    throw new InvalidOperationException("El código ya está en uso.");
                carnet.Codigo = actualizarDto.Codigo;
            }

            if (actualizarDto.FechaVencimiento.HasValue)
                carnet.FechaVencimiento = actualizarDto.FechaVencimiento.Value;

            if (actualizarDto.Activo.HasValue)
                carnet.Activo = actualizarDto.Activo.Value;

            await _carnetRepo.UpdateAsync(carnet);
            return MapToDto(carnet);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var carnet = await _carnetRepo.GetByIdAsync(id);
            if (carnet is null) return false;

            await _carnetRepo.DeleteAsync(carnet);
            return true;
        }

        private static CarnetRespuestaDto MapToDto(Carnet carnet)
        {
            return new CarnetRespuestaDto
            {
                Id = carnet.Id,
                UsuarioId = carnet.UsuarioId,
                UsuarioNombre = carnet.Usuario?.Nombre,
                Codigo = carnet.Codigo,
                FechaEmision = carnet.FechaEmision,
                FechaVencimiento = carnet.FechaVencimiento,
                Activo = carnet.Activo
            };
        }
    }
}
