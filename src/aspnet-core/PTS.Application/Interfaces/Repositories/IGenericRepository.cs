
using Microsoft.EntityFrameworkCore;
using PTS.Domain.Common.Interfaces;
using System.Linq.Expressions;

namespace PTS.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        IQueryable<T> Entities { get; }
        DbSet<T> DbSet { get; }

        Task<T> GetByIdAsync(object id, bool IsNoTracking = true);
        Task<List<T>> GetAllAsync(bool IsNoTracking = true);
        Task<T> AddAsync(T entity);
        Task AddManyAsync(List<T> entities);
        Task UpdateAsync(object id, T entity);
        Task UpdateFieldsAsync(T entity, params Expression<Func<T, object>>[] includeProperties);
        Task DeleteAsync(T entity);
        Task DeleteManyAsync(List<T> entities);
		Task<int> ExecNoneQuerySql(string sql, params object[] parameters);
        IQueryable<T> ExecQuerySql(string sql, params object[] parameters);

    }
}
