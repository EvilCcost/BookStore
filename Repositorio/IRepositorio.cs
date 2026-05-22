using System.Linq.Expressions;

namespace BookBackend.Repositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null);
        Task<T?> GetByIdAsync(int id);
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filtro, string? incluirPropiedades = null);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filtro);
        Task<int> CountAsync(Expression<Func<T, bool>>? filtro = null);
    }
}
