using Abp.Application.Services.Dto;
using PTS.Base.Application.Dto;
using PTS.Core.Dto;
using PTS.Domain.Entities;
namespace PTS.Core.Repositories
{
    public interface IProductDetailRepository
    {
        Task<ServiceResponse> Create(ProductDetailEntity obj);
        Task<ServiceResponse> CreateMany(List<ProductDetailEntity> list);
        Task<ServiceResponse> Update(ProductDetailEntity obj);
        Task<ServiceResponse> Delete(int id);
        Task<IEnumerable<ProductDetailEntity>> GetListAsync();
        Task<IEnumerable<ProductDetailDto>> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? search, decimal? from, decimal? to, string? sortBy, int? page,
            string? productType, string? namufacturer, string? ram, string? CPU, string? cardVGA);
        Task<IEnumerable<ProductDetailDto>> PublicGetList(GetProductDetailRequest request);
        Task<PagedResultDto<ProductDetailDto>> GetPagedAsync(PagedRequestDto request);
        Task<bool> UpdateSoLuong(int id, int soLuong);
        Task<ProductDetailDto> GetById(int id);
    }
}
