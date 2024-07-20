using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using PTS.WebAPI.Controllers;
using PTS.Host.Model.Base;
using MediatR;

namespace PTS.WebAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamController : BaseController
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
            if(await _unitOfWork._ramRepository.Delete(id))
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
        //[AllowAnonymous]
        //[HttpGet("PGetBillByInvoiceCode")]
        //public async Task<IActionResult> PGetBillByInvoiceCode(string invoiceCode)
        //{

        //    //string? apiKey = _config.GetSection("ApiKey").Value;
        //    //if (apiKey == null)
        //    //{
        //    //    return Unauthorized();
        //    //}

        //    //var keyDomain = Request.Headers["Key-Domain"].FirstOrDefault();
        //    //if (keyDomain != apiKey.ToLower())
        //    //{
        //    //    return Unauthorized();
        //    //}
        //    var result = await _billService.PGetBillByInvoiceCode(invoiceCode);
        //   // Log.Information("GetBill => {@_reponse}", result);
        //    if (result.IsSuccess)
        //    {
        //        return Ok(result);
        //    }
        //    return BadRequest(result);
        //    // }

        //}
        //[AllowAnonymous]
        //[HttpGet("GetListBill")]
        //public async Task<IActionResult> GetListBill(string? phoneNumber)
        //{
        //    return Ok(await _billService.GetAllBill(phoneNumber));
        //}
        //[AllowAnonymous]
        //[HttpGet("GetBillDetailByInvoiceCode")]
        //public async Task<IActionResult> GetBillDetail(string invoiceCode)
        //{
        //    return Ok(await _billService.GetBillDetailByInvoiceCode(invoiceCode));
        //}
        //[AllowAnonymous]
        //[HttpPost("CreateBill")]
        //public async Task<IActionResult> CreateBill(RequestBillDto request)
        //{
        //    var reponse = await _billService.CreateBill(request);
        //    if (reponse.IsSuccess)
        //    {
        //        return Ok(reponse.Message);
        //    }
        //    return BadRequest("");
        //}
        //[AllowAnonymous]
        //[HttpPut("UpdateBill")]
        //public async Task<IActionResult> UpdateBill(BillEntity bill)
        //{
        //    if (await _billRepository.Update(bill))
        //    {
        //        return Ok($"Cập nhật hóa đơn {bill.InvoiceCode} thành công");
        //    }
        //    return BadRequest("Cập nhật thất bại");
        //}
        //[AllowAnonymous]
        //[HttpGet("GetRevenueStatistics")]
        //public IActionResult GetRevenueStatistics()
        //{
        //    var revenueData = _context.BillDetails
        //        .GroupBy(bd => new { Year = bd.Bill.CreateDate.Year, Month = bd.Bill.CreateDate.Month })
        //        .Select(g => new RevenueDto
        //        {
        //            Date = new DateTime(g.Key.Year, g.Key.Month, 1),
        //            Amount = (decimal)g.Sum(bd => bd.Quantity * bd.Price)
        //        })
        //        .ToList();

        //    return Ok(revenueData);
        //}

        //[AllowAnonymous]
        //[HttpGet("GetRevenueStatistics")]
        //public IActionResult GetRevenueStatistics(int year)
        //{
        //    var revenueData = _context.BillDetailEntity
        //        .Where(bd => bd.BillEntity.Year == year)
        //        .GroupBy(bd => new { Month = bd.Bill.CreateDate.Month })
        //        .Select(g => new RevenueDto
        //        {
        //            Date = new DateTime(year, g.Key.Month, 1),
        //            Amount = (decimal)g.Sum(bd => bd.Quantity * bd.Price)
        //        })
        //        .ToList();

        //    return Ok(revenueData);
        //}


        //[AllowAnonymous]
        //[HttpGet("GetRevenueStatisticss")]
        //public IActionResult GetRevenueStatisticss(DateTime selectedDate)
        //{
        //    var startDate = selectedDate.Date;
        //    var endDate = startDate.AddMonths(1).AddDays(-1);

        //    var dailyStatistics = _context.Bills
        //        .Where(b => b.CreateDate >= startDate && b.CreateDate <= endDate)
        //        .Join(
        //            _context.BillDetailEntis,
        //            bill => bill.Id,
        //            billDetail => billDetail.BillId,
        //            (bill, billDetail) => new
        //            {
        //                Day = bill.CreateDate.Day,
        //                Amount = billDetail.Price * billDetail.Quantity,
        //                // Add other fields as needed
        //            }
        //        )
        //        .GroupBy(result => result.Day)
        //        .Select(group => new
        //        {
        //            Day = group.Key,
        //            Amount = group.Sum(result => result.Amount),
        //            TotalOrders = group.Count(),
        //            // Add other fields as needed
        //        })
        //        .ToList();
        //    return Ok(dailyStatistics);
        //}
    }
}
