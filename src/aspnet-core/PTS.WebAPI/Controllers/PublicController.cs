using PTS.Application.Features.PhapDienDocs.Fields.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Features.Voucher.Queries;
using PTS.Application.Features.ProductDetail.Queries;

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
	}
}
