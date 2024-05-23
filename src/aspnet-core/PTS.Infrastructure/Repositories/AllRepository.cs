using Microsoft.EntityFrameworkCore;
using PTS.Data;
using PTS.Data;
using PTS.Core.Repositories;
using System;
using System.Linq.Expressions;

namespace PTS.Host.Repository
{
    public class AllRepository<TEntity> : IAllRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public AllRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetPagedAsync(int page, int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(page), "Page number must be greater than 0.");
            }

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");
            }

            IQueryable<TEntity> entities = _context.Set<TEntity>().Where(predicate);
            int totalCount = await entities.CountAsync();

            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            if (page > totalPages)
            {
                page = totalPages;
            }

            var pagedEntities = await entities.Skip((page - 1) * pageSize)
                                              .Take(pageSize)
                                              .ToListAsync();

            return pagedEntities;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }
    }
}
