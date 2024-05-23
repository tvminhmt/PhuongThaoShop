using System.Linq.Expressions;

namespace PTS.Core.Repositories
{
     public interface IAllRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetPagedAsync(int page, int pageSize, Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetByIdAsync(int id);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
