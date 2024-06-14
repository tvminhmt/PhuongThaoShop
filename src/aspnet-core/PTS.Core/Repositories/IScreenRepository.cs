using Abp.Application.Services.Dto;
using PTS.Base.Application.Dto;
using PTS.Core.Dto;
using PTS.Domain.Entities;

namespace PTS.Core.Repositories
{
    public interface IScreenRepository
    {
        Task<bool> Create(ScreenEntity obj);
        Task<bool> Update(ScreenEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<ScreenEntity>> GetList();
        Task<PagedResultDto<ScreenDto>> GetPagedAsync(PagedRequestDto request);
        Task<ScreenEntity> GetById(int id);
    }
}
