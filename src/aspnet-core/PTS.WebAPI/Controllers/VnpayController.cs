using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Features.Bill.Commands;
using PTS.Core.Services;
using PTS.Domain.Model;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VnpayController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IVnPayService _vnPayService;

        public VnpayController(IVnPayService vnPayService, IMediator mediator)
        {
            _vnPayService = vnPayService;
            _mediator = mediator;
        }
        [HttpPost("CreatePayment")]
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Ok(new { paymentUrl = url });
        }
        [HttpGet("PaymentCallback")]
        public async Task<IActionResult> PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response.VnPayResponseCode == "00")
            {
               var result = await _mediator.Send(new BillEditByCodeCommand { Code = response.BillCode });
                if(result.Succeeded)
                {
                    return Redirect($"http://localhost:4200/hoa-don?codeBill={response.BillCode}"); 
                }
                else
                {
                    return Redirect($"http://localhost:4200/hoa-don?codeBill={response.BillCode}");
                }
              
            }
            else
            {
                return Redirect("http://localhost:4200/thanh-toan-that-bai"); 
            }
        }

    }
}
