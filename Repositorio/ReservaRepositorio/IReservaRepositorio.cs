using BookBackend.Modelos;

namespace BookBackend.Repositorio.ReservaRepositorio
{
    public interface IReservaRepositorio : IRepositorio<Reserva>
    {
        Task<Reserva?> GetReservaWithDetailsAsync(int id);
        Task<List<Reserva>> GetByUsuarioIdAsync(int usuarioId);
        Task<List<Reserva>> GetPendientesAsync();
    }
}
