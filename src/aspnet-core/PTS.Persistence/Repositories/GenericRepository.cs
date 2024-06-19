using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace PTS.Persistence.Repositories
{
	public class GenericRepository<T>(DbContext dbContext) : IGenericRepository<T> where T : BaseAuditableEntity
	{
		public DbSet<T> DbSet => dbContext.Set<T>();

		public IQueryable<T> Entities => DbSet;
		public IQueryable<T> EntitiesNoTracking => DbSet.AsNoTracking();

        public async Task<T> AddAsync(T entity)
        {
            await dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task AddRangeAsync(IEnumerable<T> entities)
		{
			await DbSet.AddRangeAsync(entities);
		}

		public Task RemoveAsync(T entity)
		{
			DbSet.Remove(entity);
			return Task.CompletedTask;
		}

		public Task RemoveManyAsync(IEnumerable<T> entities)
		{
			DbSet.RemoveRange(entities);

			return Task.CompletedTask;
		}

		public async Task UpdateAsync(object id, T entity)
		{
			T entityExist = await ExistsAsync(id);

			if (entityExist != null)
			{
                dbContext.Entry(entity).State = EntityState.Detached;

                dbContext.Entry(entityExist).State = EntityState.Modified;

                dbContext.Entry(entityExist).CurrentValues.SetValues(entity);
			}
		}

		public  Task UpdateFieldsAsync(T entity, params Expression<Func<T, object>>[] updatedProperties)
		{
            var dbEntry = dbContext.Entry(entity);

            foreach (var includeProperty in updatedProperties)
            {
                dbEntry.Property(includeProperty).IsModified = true;
            }
            return Task.CompletedTask;
        }

		public async Task<T> FindOneAsync(List<Expression<Func<T, bool>>> predicates, params string[] memberNames)
		{
			IQueryable<T> query = DbSet;

			foreach (var predicate in predicates)
			{
				query = query.Where(predicate);
			}

			if (memberNames != null && memberNames.Length > 0)
			{
				query = query.Select(SelectProperties(memberNames));
			}

			return await query.FirstOrDefaultAsync();
		}

		public async Task<IEnumerable<T>> FindByAsync(List<Expression<Func<T, bool>>> predicates, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int rowAmount, params string[] memberNames)
		{
			IQueryable<T> query = DbSet;

			foreach (var predicate in predicates)
			{
				query = query.Where(predicate);
			}

			query = orderBy(query);

            if (memberNames != null && memberNames.Length > 0)
            {
                query = query.Select(SelectProperties(memberNames));
            }

            return await query.AsNoTracking().Take(rowAmount).ToListAsync();
		}

        public async Task<(IEnumerable<T> entities, int count)> FindByGetCountAsync(List<Expression<Func<T, bool>>> predicates, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int rowAmount, params string[] memberNames)
        {
            IQueryable<T> query = DbSet;

            foreach (var predicate in predicates)
            {
                query = query.Where(predicate);
            }

            query = orderBy(query);

            if (memberNames != null && memberNames.Length > 0)
            {
                query = query.Select(SelectProperties(memberNames));
            }

            var count = await query.CountAsync();

            var entities = await query.AsNoTracking().Take(rowAmount).ToListAsync();

            return (entities, count);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params string[] memberNames)
		{
			IQueryable<T> query = DbSet;

			query = orderBy(query);

			return await query.ToListAsync();
		}

		private async Task<T> ExistsAsync(object id)
		{
			var entityExist = await DbSet.FindAsync(id);

			if (entityExist != null)
			{
				DbSet.Entry(entityExist).State = EntityState.Detached;
			}

			return entityExist;
		}
        

        public Task AddManyAsync(List<T> entities)
        {
            dbContext.Set<T>().AddRange(entities);
            return Task.CompletedTask;
        }
        public static Expression<Func<T, T>> SelectProperties(params string[] memberNames)
		{
			var parameter = Expression.Parameter(typeof(T), "entity");

			var bindings = memberNames
				.Select(name => Expression.PropertyOrField(parameter, name))
				.Select(member => Expression.Bind(member.Member, member));

			var body = Expression.MemberInit(Expression.New(typeof(T)), bindings);

			var selector = Expression.Lambda<Func<T, T>>(body, parameter);

			return selector;
		}
        public Task DeleteAsync(T entity)
        {
            dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }
        public Task DeleteManyAsync(List<T> entities)
        {
            dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync(bool IsNoTracking = true)
        {
            if (IsNoTracking) return await dbContext.Set<T>().AsNoTracking().ToListAsync();

            return await dbContext.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(object id, bool IsNoTracking = true)
        {
            if (IsNoTracking)
            {

                var entity = await dbContext.Set<T>().FindAsync(id);

                if (entity != null)
                {
                    dbContext.Set<T>().Entry(entity).State = EntityState.Detached;
                }

                return entity;
            }

            return await dbContext.Set<T>().FindAsync(id);
        }
        public Task<int> ExecNoneQuerySql(string sql, params object[] parameters)
		{
			var result = dbContext.Database.ExecuteSqlRaw(sql, parameters);

			return Task.FromResult(result);
		}
        public IQueryable<T> ExecQuerySql(string sql, params object[] parameters)
        {
            var result = dbContext.Database.SqlQueryRaw<T>(sql, parameters);

            return result;
        }
        public async Task<IEnumerable<T>> ExecuteFuntion(FormattableString sql)
		{
			return await dbContext.Set<T>().FromSql(sql).ToListAsync();
		}
    }
}
