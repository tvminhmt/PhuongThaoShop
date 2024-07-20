
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Features.Voucher.Commands;
using PTS.Application.Features.Voucher.Queries;
using PTS.Domain.Entities;

namespace PTS.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VoucherController : BaseController
	{
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


        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public RoleController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _unitOfWork._roleRepository.GetList());
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork._roleRepository.GetById(id));
        }
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(RoleDto objDto)
        {
            var obj = _mapper.Map<RoleEntity>(objDto);
            if (objDto.Id > 0)
            {
                return Ok(await _unitOfWork._roleRepository.Update(obj));
            }
            else
            {
                return Ok(await _unitOfWork._roleRepository.Create(obj));
            }
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _unitOfWork._roleRepository.Delete(id));
        }
    }
}
