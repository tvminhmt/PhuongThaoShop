using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Create(UserEntity obj);
        Task<bool> Update(UserEntity obj);
        Task<bool> Delete(int Id);
        Task<List<UserEntity>> GetAllUsers();
        Task<UserEntity> GetUserById(int id);
        Task<UserEntity> GetUserByUserName(string UserName);
    }
}
