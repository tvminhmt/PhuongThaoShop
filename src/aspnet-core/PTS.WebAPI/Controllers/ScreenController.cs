using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using MediatR;
using PTS.Host.Model.Base;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public RamController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _unitOfWork._ramRepository.GetList());
        }
        [HttpPost("GetPaged")]
        public async Task<IActionResult> GetPaged(PagedRequestDto request)
        {
            return Ok(await _unitOfWork._ramRepository.GetPagedAsync(request));
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork._ramRepository.GetById(id));
        }
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(RamDto objDto)
        {
            var obj = _mapper.Map<RamEntity>(objDto);
            bool isSuccess;

            if (objDto.Id > 0)
            {
                isSuccess = await _unitOfWork._ramRepository.Update(obj);
            }
            else
            {
                isSuccess = await _unitOfWork._ramRepository.Create(obj);
            }

            if (isSuccess)
            {
                return Ok(new ApiSuccessResult<RamEntity>(obj));
            }
            else
            {
                return Ok(new ApiErrorResult<RamEntity>("Error"));
            }
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _unitOfWork._ramRepository.Delete(id))
            {
                return Ok(new ApiSuccessResult<RamEntity>());
            }
            else
            {
                return Ok(new ApiErrorResult<RamEntity>("Error"));
            }
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

        //private readonly IBillService _billService;
        //private readonly IBillRepository _billRepository;
        //private readonly IConfiguration _config;
        //private readonly ApplicationDbContext _context;
        //public BillController(IConfiguration config,
        //    IBillService billService, IBillRepository billRepository, ApplicationDbContext context)
        //{
        //    _config = config;
        //    _billService = billService;
        //    _billRepository = billRepository;
        //    _context = context;
        //}
        private readonly IBillRepository _billRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BillController(IMediator mediator, IMapper mapper, IBillRepository billRepository)
        {
            _mediator = mediator;
            _mapper = mapper;
            _billRepository = billRepository;
        }
        [HttpPost("CreateBill")]
        public async Task<IActionResult> CreateBill(CreateOrUpdateBillQuery request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPost("PGetBill")]
        public async Task<IActionResult> PGetBill(CreateOrUpdateBillQuery request)
        {
            return Ok(await _mediator.Send(request));
        }
        [HttpPost("GetListBill")]
        public async Task<IActionResult> GetListBill()
        {
            return Ok(await _mediator.Send(new GetListBillQuery()));
        }
        [HttpPost("GetBill")]
        public async Task<IActionResult> GetBill(GetPageBillDto request)
        {
            return Ok(await _billRepository.GetPage(request));
        }
    }
}
