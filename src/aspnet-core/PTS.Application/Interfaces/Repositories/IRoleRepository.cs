using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task<bool> Create(RoleEntity obj);
        Task<bool> Update(RoleEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<RoleEntity>> GetList();
        Task<RoleEntity> GetById(int id);
    }
}
