using Abp.Application.Services.Dto;
using PTS.Core.Dto;
using PTS.Domain.Entities;
using PTS.Base.Application.Dto;

namespace PTS.Domain.Repositories
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
