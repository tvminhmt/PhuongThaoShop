using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
{
    public interface ICpuRepository
    {
        Task<bool> Create(CpuEntity obj);
        Task<bool> Update(CpuEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<CpuEntity>> GetList();
        Task<PagedResultDto<CpuDto>> GetPagedAsync(PagedRequestDto request);
        Task<CpuEntity> GetById(int id);
    }
}
