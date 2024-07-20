using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ScreenController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _unitOfWork._screenRepository.GetList());
        }
        [HttpPost("GetPaged")]
        public async Task<IActionResult> GetPaged(PagedRequestDto request)
        {
            return Ok(await _unitOfWork._screenRepository.GetPagedAsync(request));
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork._screenRepository.GetById(id));
        }
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
