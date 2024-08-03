using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
{
    public interface IProductTypeRepository
    {
        Task<bool> Create(ProductTypeEntity obj);
        Task<bool> Update(ProductTypeEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<ProductTypeEntity>> GetList();
        Task<PagedResultDto<ProductTypeDto>> GetPagedAsync(PagedRequestDto request);
        Task<ProductTypeEntity> GetById(int id);
    }
}
