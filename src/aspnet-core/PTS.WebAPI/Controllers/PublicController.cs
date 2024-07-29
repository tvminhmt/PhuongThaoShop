
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Features.Voucher.Queries;
using PTS.Application.Features.ProductDetail.Queries;
using PTS.Application.Features.Bill.Commands;
using PTS.Application.Features.Bill.Queries;

namespace PTS.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublicController : ControllerBase
	{
		private readonly IMediator _mediator;
		public PublicController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet("GetListVoucher")]
		public async Task<IActionResult> GetListVoucher()
		{
			return Ok(await _mediator.Send(new PVoucherGetAllQuery()));
		}
		[HttpPost("GetListProduct")]
		public async Task<IActionResult> GetListProduct(PProductDetailGetPageQuery query)
		{
			return Ok(await _mediator.Send(query));
		}
		[HttpPost("GetProductById")]
		public async Task<IActionResult> GetProductById(PProductDetailGetByIdQuery query)
		{
			return Ok(await _mediator.Send(query));
		}
		[HttpPost("CreateBill")]
        public async Task<IActionResult> CreateBill(PBillCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("GetBill")]
        public async Task<IActionResult> GetBill(PBillGetByCodeQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
