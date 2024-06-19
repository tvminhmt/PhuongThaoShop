
using PTS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Interfaces.Repositories;
using PTS.Data;

namespace PTS.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Create(RoleEntity obj)
        {
        var checkName = await _dbContext.RoleEntity.AnyAsync(x => x.Name == obj.Name);

            if (obj == null || checkName == true)
            {
                return false;
            }
            try
            {
                await _dbContext.RoleEntity.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var role = await _dbContext.RoleEntity.FindAsync(id);
            if (role == null)
            {
                return false;
            }
            try
            {
            //    role.Status = 0;
                _dbContext.RoleEntity.Update(role);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<RoleEntity>> GetList()
        {
            return await _dbContext.RoleEntity.ToListAsync();
        }

        public async Task<RoleEntity> GetById(int id)
        {
            return await _dbContext.RoleEntity.FindAsync(id);
        }

        public async Task<bool> Update(RoleEntity obj)
        {
            var role = await _dbContext.RoleEntity.FindAsync(obj.Id);
            if (role == null)
            {
                return false;
            }
            try
            {
                role.Name = obj.Name;
                _dbContext.RoleEntity.Update(role);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
