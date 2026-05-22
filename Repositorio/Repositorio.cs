using System.Linq.Expressions;
using BookBackend.Data;
using Microsoft.EntityFrameworkCore;

namespace BookBackend.Repositorio
{
    public class Repositorio<T> : IRepositorio<T> where T : class
    {
        private readonly BookStoreContext _db;
        internal DbSet<T> _dbSet;

        public Repositorio(BookStoreContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filtro = null, string? incluirPropiedades = null)
        {
            IQueryable<T> query = _dbSet;

            if (filtro is not null)
                query = query.Where(filtro);

            if (incluirPropiedades is not null)
            {
                foreach (var propiedad in incluirPropiedades.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    query = query.Include(propiedad);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filtro, string? incluirPropiedades = null)
        {
            IQueryable<T> query = _dbSet.Where(filtro);

            if (incluirPropiedades is not null)
            {
                foreach (var propiedad in incluirPropiedades.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    query = query.Include(propiedad);
                }
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> filtro)
        {
            return await _dbSet.AnyAsync(filtro);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>>? filtro = null)
        {
            if (filtro is null)
                return await _dbSet.CountAsync();

            return await _dbSet.CountAsync(filtro);
        }
    }
}
