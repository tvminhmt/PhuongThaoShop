
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Features.BillDetail.Commands;
using PTS.Application.Features.BillDetail.Queries;
using PTS.Application.Features.Voucher.Commands;
using PTS.Application.Features.Voucher.Queries;

namespace PTS.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class BillDetailController : BaseController
    {
        public BillDetailController()
        {

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new BillDetailGetAllQuery()));
        }

        //[HttpPost("GetPage")]
        //public async Task<IActionResult> GetPage(BillDetailGetPageQuery query)
        //{
        //	return Ok(await _mediator.Send(query));
        //}
        [HttpPost("GetByBillId")]
        public async Task<IActionResult> GetByBillId(BillDetailGetByBillIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(BillDetailCreateOrUpdateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteBillDetail(BillDetailDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
