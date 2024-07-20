using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using MediatR;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenController : BaseController
    {
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(ScreenDto objDto)
        {
            var obj = _mapper.Map<ScreenEntity>(objDto);
            if (objDto.Id > 0)
            {
                return Ok(await _unitOfWork._screenRepository.Update(obj));
            }
            else
            {
                return Ok(await _unitOfWork._screenRepository.Create(obj));
            }
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _unitOfWork._screenRepository.Delete(id));
        }

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
