
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Features.Voucher.Commands;
using PTS.Application.Features.Voucher.Queries;

namespace PTS.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
    public class VoucherController : BaseController
    {
        private readonly IMediator _mediator;
        public VoucherController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new VoucherGetAllQuery()));
        }

        [HttpPost("GetPage")]
        public async Task<IActionResult> GetPage(VoucherGetPageQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost("GetById")]
        public async Task<IActionResult> GetById(VoucherGetByIdQuery query)
        {
            return Ok(await _mediator.Send(query));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(VoucherCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("Update")]
        public async Task<IActionResult> Update(VoucherEditCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteVoucher(VoucherDeleteCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
