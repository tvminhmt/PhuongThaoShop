using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
{
    public interface IManufacturerRepository
    {
        Task<bool> Create(ManufacturerEntity obj);
        Task<bool> Update(ManufacturerEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<ManufacturerEntity>> GetList();
        Task<ManufacturerEntity> GetById(int id);
        Task<PagedResultDto<ManufacturerDto>> GetPagedAsync(PagedRequestDto request);

    }
}
