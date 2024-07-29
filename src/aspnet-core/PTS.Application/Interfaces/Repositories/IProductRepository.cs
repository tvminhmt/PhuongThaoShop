using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
namespace PTS.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<bool> Create(ProductEntity obj);
        Task<bool> Update(ProductEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<ProductEntity>> GetList();
        Task<PagedResultDto<ProductDto>> GetPagedAsync(PagedRequestDto request);
        Task<ProductEntity> GetById(int id);
    }
}
