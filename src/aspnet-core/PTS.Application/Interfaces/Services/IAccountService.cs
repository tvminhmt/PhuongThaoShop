using PTS.Application.Dto;

namespace PTS.Core.Services
{
    public interface IAccountService
    {
        Task<LoginResponse> Login(string UserName, string password);
    }
}
