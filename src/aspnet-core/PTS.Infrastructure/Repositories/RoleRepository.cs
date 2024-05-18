
using PTS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PTS.Core.Repositories;
using PTS.Data;

namespace PTS.Infrastructure.Repositories
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
        var checkRoleName = await _dbContext.RoleEntity.AnyAsync(x => x.RoleName == obj.RoleName);

            if (obj == null || checkRoleName == true)
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
                role.IsDeleted = true;
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
            return await _dbContext.RoleEntity.Where(a=>!a.IsDeleted).ToListAsync();
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
                role.RoleName = obj.RoleName;
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
