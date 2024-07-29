using Microsoft.AspNetCore.Http;
using PTS.Domain.Model;

namespace PTS.Core.Services
{ 
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }
}
