using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
{
    public interface IHardDriveRepository
    {
        Task<bool> Create(HardDriveEntity obj);
        Task<bool> Update(HardDriveEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<HardDriveEntity>> GetList();
        Task<PagedResultDto<HardDriveDto>> GetPagedAsync(PagedRequestDto request);
        Task<HardDriveEntity> GetById(int id);

    }
}
