using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
{
    public interface IScreenRepository
    {
        Task<bool> Create(ScreenEntity obj);
        Task<bool> Update(ScreenEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<ScreenEntity>> GetList();
        Task<PagedResultDto<ScreenDto>> GetPagedAsync(PagedRequestDto request);
        Task<ScreenEntity> GetById(int id);

        //Task<bool> Create(Voucher obj 
        Task<bool> Create(VoucherEntity obj);
        Task<bool> Update(VoucherEntity obj);
        Task<bool> Delete(int id);
        Task<bool> UpdateSL(string codeVoucher);
        Task<List<VoucherEntity>> GetAll();
        Task<VoucherEntity> GetByCode(string codeVoucher);
        Task<bool> Duyet(int Id);
        Task<bool> HuyDuyet(int Id);
    }
}
