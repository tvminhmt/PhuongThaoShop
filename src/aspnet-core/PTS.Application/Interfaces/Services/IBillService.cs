using PTS.Application.Dto;
using PTS.Application.Features.Bill.Commands;
using PTS.Application.Features.Bill.DTOs;
using PTS.Domain.Entities;
using PTS.Domain.Model.Base;

namespace PTS.Core.Services
{
    public interface IBillService
    {
        //  Task<ResponseDto> CreateBill(RequestBillDto request);
        Task<ApiResult<BillDto>> CreateBill(PBillCreateCommand command);
        Task<ResponseDto> PGetBillByInvoiceCode(string invoiceCode);
        Task<ResponseDto> GetBillDetailByInvoiceCode(string invoiceCode);
        // Task<ResponseDto> GetAllBill();
        Task<ResponseDto> GetAllBill(string? phoneNumber);
    }
}
