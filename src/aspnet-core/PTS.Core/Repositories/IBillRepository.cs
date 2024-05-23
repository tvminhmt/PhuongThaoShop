using PTS.Core.Dto;
using PTS.Domain.Entities;

namespace PTS.Core.Repositories
{
    public interface IBillRepository
    {
        Task<bool> Create(BillEntity obj);
        Task<bool> Update(BillEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<BillEntity>> GetAll();
        Task<IEnumerable<BillDetailDto>> GetBillDetailByInvoiceCode(string invoiceCode);
        Task<BillDto> GetBillByInvoiceCode(string invoiceCode);
        Task<IEnumerable<BillDetailDto>> GetBillDetail(string username);
    }
}
