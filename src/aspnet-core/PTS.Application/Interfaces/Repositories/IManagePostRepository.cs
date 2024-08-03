using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
{
    public interface IManagePostRepository
    {
        Task<bool> Create(ManagePostEntity obj);
        Task<bool> Update(ManagePostEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<ManagePostEntity>> GetList();
        Task<PagedResultDto<ManagePostDto>> GetPagedAsync(PagedRequestDto request);
        Task<ManagePostEntity> GetById(int id);
    }
}
