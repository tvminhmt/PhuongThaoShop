
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Features.Address.Commands;
using PTS.Application.Features.Address.Queries;
using PTS.Application.Features.Voucher.Commands;
using PTS.Application.Features.Voucher.Queries;

namespace PTS.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    public class AddressController : BaseController
    {
        public AddressController()
        {

        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new AddressGetAllQuery()));
        }

        //[HttpPost("GetPage")]
        //public async Task<IActionResult> GetPage(AddressGetPageQuery query)
        //{
        //	return Ok(await _mediator.Send(query));
        //}
        [HttpPost("GetByUserId")]
        public async Task<IActionResult> GetByUserId(AddressGetByUserIdQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpPost("CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(AddressCreateOrUpdateCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAddress(AddressDeleteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
