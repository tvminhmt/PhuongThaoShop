using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTS.Application.Dto;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Services;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using PTS.WebAPI.Controllers;
using PTS.Application.Features.Cart.Commands;
using PTS.Application.Features.Cart.Queries;

namespace PTS.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CartController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [AllowAnonymous]// For client
        [HttpPost("GetCartByUser")]
        public async Task<IActionResult> GetCartByUser(string UserName)
        {
            GetCartByUserQuery query = new GetCartByUserQuery();
            query.UserName = UserName;
            return Ok(await _mediator.Send(query));
        }
        [AllowAnonymous]// For client
        [HttpDelete("DeleteCartDetail")]
        public async Task<IActionResult> DeleteCartDetail(int Id)
        {
            DeleteCartDetailQuery query = new DeleteCartDetailQuery();
            query.Id = Id;
            return Ok(await _mediator.Send(query));
        }
        [AllowAnonymous]
        [HttpPost("AddToCart")]
        public async Task<ServiceResponse> AddToCart(CreateOrUpdateCartQuery query)
        {
            return await _mediator.Send(query);
        }
        [AllowAnonymous]
        [HttpPost("UpdateQuantity")]
        public async Task<ServiceResponse> UpdateQuantity(UpdateQuantityCartItemQuery query)
        {
            return await _mediator.Send(query);
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

        public async Task<IActionResult> Create(ProductDetailCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany(List<ProductDetailEntity> list)
        {

            return BadRequest("Lỗi");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            return Ok(await _repository.Delete(id));
        }
        [HttpPost]
        public async Task<ActionResult<CreateProductDetailDto>> PostProduct([FromForm] CreateProductDetailDto product, IFormFile[] files)
        {
            if (files != null && files.Length > 0)
            {
                await SaveFiles(product, files);
            }



            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        private async Task SaveFiles(CreateProductDetailDto product, IFormFile[] files)
        {
            string[] imageProperties = { "Image1", "Image2", "Image3", "Image4", "Image5", "Image6" };

            for (int i = 0; i < files.Length && i < imageProperties.Length; i++)
            {
                var file = files[i];
                if (file != null && file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(CreateProductDetailDto).GetProperty(imageProperties[i])?.SetValue(product, $"/uploads/{fileName}");
                }
            }
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _unitOfWork._productTypeRepository.GetList());
        }
        [HttpPost("GetPaged")]
        public async Task<IActionResult> GetPaged(PagedRequestDto request)
        {
            return Ok(await _unitOfWork._productTypeRepository.GetPagedAsync(request));
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _unitOfWork._productTypeRepository.GetById(id));
        }
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(ProductTypeDto objDto)
        {
            var obj = _mapper.Map<ProductTypeEntity>(objDto);
            if (objDto.Id > 0)
            {
                return Ok(await _unitOfWork._productTypeRepository.Update(obj));
            }
            else
            {
                return Ok(await _unitOfWork._productTypeRepository.Create(obj));
            }
        }
        [HttpPost("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _unitOfWork._productTypeRepository.Delete(id));
        }

        [AllowAnonymous]
        [HttpGet("PGetProductDetail")]
        public async Task<IActionResult> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? search, decimal? from, decimal? to, string? sortBy, int? page, string? productType, string? hangsx, string? ram, string? CPU, string? cardVGA)
        {

            var listProductDetail = await _repository.PGetProductDetail(getNumber, codeProductDetail, status, search, from, to, sortBy, page, productType, hangsx, ram, CPU, cardVGA);
            _reponse.Result = listProductDetail;
            return Ok(_reponse);
        }
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(CreateProductDetailDto objDto, IFormFile[] files)
        {
            var obj = _mapper.Map<ProductDetailEntity>(objDto);
            CreateOrUpdateProductDetailQuery query = new CreateOrUpdateProductDetailQuery();
            query.ProductDetailEntity = obj;
            return Ok(await _mediator.Send(query));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductDetailCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany(List<ProductDetailEntity> list)
        {

            return BadRequest("Lỗi");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            return Ok(await _repository.Delete(id));
        }
        [HttpPost]
        public async Task<ActionResult<CreateProductDetailDto>> PostProduct([FromForm] CreateProductDetailDto product, IFormFile[] files)
        {
            if (files != null && files.Length > 0)
            {
                await SaveFiles(product, files);
            }



            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        private async Task SaveFiles(CreateProductDetailDto product, IFormFile[] files)
        {
            string[] imageProperties = { "Image1", "Image2", "Image3", "Image4", "Image5", "Image6" };

            for (int i = 0; i < files.Length && i < imageProperties.Length; i++)
            {
                var file = files[i];
                if (file != null && file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(CreateProductDetailDto).GetProperty(imageProperties[i])?.SetValue(product, $"/uploads/{fileName}");
                }
            }
        }

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

        private readonly IWebHostEnvironment _environment;
        private readonly IProductDetailRepository _repository;
        private readonly ResponseDto _reponse;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductDetailController(IWebHostEnvironment environment, IProductDetailRepository repository, IConfiguration config, IMediator mediator, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _environment = environment;
            _repository = repository;
            _reponse = new ResponseDto();
            _config = config;
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetListAsync")]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok(await _unitOfWork._productDetailRepository.GetListAsync());
        }
        [HttpPost("GetPagedAsync")]
        public async Task<IActionResult> GetPagedAsync(PagedRequestDto request)
        {
            return Ok(await _unitOfWork._productDetailRepository.GetPagedAsync(request));
        }
        [HttpPost("GetPagedq")]
        public async Task<IActionResult> GetPaged(ProductDetailGetPageQuery request)
        {
            var vouchers = await _mediator.Send(request);
            return Ok(vouchers);
        }
        [AllowAnonymous]
        [HttpPost("PublicGetList")]
        public async Task<IActionResult> PublicGetList(GetProductDetailRequest request)
        {
            return Ok(await _unitOfWork._productDetailRepository.PublicGetList(request));
        }
        [AllowAnonymous]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            _reponse.Result = await _unitOfWork._productDetailRepository.GetById(id);
            return Ok(_reponse);
        }
        [AllowAnonymous]
        [HttpGet("PGetProductDetail")]
        public async Task<IActionResult> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? search, decimal? from, decimal? to, string? sortBy, int? page, string? productType, string? hangsx, string? ram, string? CPU, string? cardVGA)
        {

            var listProductDetail = await _repository.PGetProductDetail(getNumber, codeProductDetail, status, search, from, to, sortBy, page, productType, hangsx, ram, CPU, cardVGA);
            _reponse.Result = listProductDetail;
            return Ok(_reponse);
        }
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(CreateProductDetailDto objDto, IFormFile[] files)
        {
            var obj = _mapper.Map<ProductDetailEntity>(objDto);
            CreateOrUpdateProductDetailQuery query = new CreateOrUpdateProductDetailQuery();
            query.ProductDetailEntity = obj;
            return Ok(await _mediator.Send(query));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductDetailCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany(List<ProductDetailEntity> list)
        {

            return BadRequest("Lỗi");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            return Ok(await _repository.Delete(id));
        }
        [HttpPost]
        public async Task<ActionResult<CreateProductDetailDto>> PostProduct([FromForm] CreateProductDetailDto product, IFormFile[] files)
        {
            if (files != null && files.Length > 0)
            {
                await SaveFiles(product, files);
            }



            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        private async Task SaveFiles(CreateProductDetailDto product, IFormFile[] files)
        {
            string[] imageProperties = { "Image1", "Image2", "Image3", "Image4", "Image5", "Image6" };

            for (int i = 0; i < files.Length && i < imageProperties.Length; i++)
            {
                var file = files[i];
                if (file != null && file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(CreateProductDetailDto).GetProperty(imageProperties[i])?.SetValue(product, $"/uploads/{fileName}");
                }
            }
        }

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

        private readonly IWebHostEnvironment _environment;
        private readonly IProductDetailRepository _repository;
        private readonly ResponseDto _reponse;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductDetailController(IWebHostEnvironment environment, IProductDetailRepository repository, IConfiguration config, IMediator mediator, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _environment = environment;
            _repository = repository;
            _reponse = new ResponseDto();
            _config = config;
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetListAsync")]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok(await _unitOfWork._productDetailRepository.GetListAsync());
        }
        [HttpPost("GetPagedAsync")]
        public async Task<IActionResult> GetPagedAsync(PagedRequestDto request)
        {
            return Ok(await _unitOfWork._productDetailRepository.GetPagedAsync(request));
        }
        [HttpPost("GetPagedq")]
        public async Task<IActionResult> GetPaged(ProductDetailGetPageQuery request)
        {
            var vouchers = await _mediator.Send(request);
            return Ok(vouchers);
        }
        [AllowAnonymous]
        [HttpPost("PublicGetList")]
        public async Task<IActionResult> PublicGetList(GetProductDetailRequest request)
        {
            return Ok(await _unitOfWork._productDetailRepository.PublicGetList(request));
        }
        [AllowAnonymous]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            _reponse.Result = await _unitOfWork._productDetailRepository.GetById(id);
            return Ok(_reponse);
        }
        [AllowAnonymous]
        [HttpGet("PGetProductDetail")]
        public async Task<IActionResult> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? search, decimal? from, decimal? to, string? sortBy, int? page, string? productType, string? hangsx, string? ram, string? CPU, string? cardVGA)
        {

            var listProductDetail = await _repository.PGetProductDetail(getNumber, codeProductDetail, status, search, from, to, sortBy, page, productType, hangsx, ram, CPU, cardVGA);
            _reponse.Result = listProductDetail;
            return Ok(_reponse);
        }
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(CreateProductDetailDto objDto, IFormFile[] files)
        {
            var obj = _mapper.Map<ProductDetailEntity>(objDto);
            CreateOrUpdateProductDetailQuery query = new CreateOrUpdateProductDetailQuery();
            query.ProductDetailEntity = obj;
            return Ok(await _mediator.Send(query));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductDetailCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany(List<ProductDetailEntity> list)
        {

            return BadRequest("Lỗi");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            return Ok(await _repository.Delete(id));
        }
        [HttpPost]
        public async Task<ActionResult<CreateProductDetailDto>> PostProduct([FromForm] CreateProductDetailDto product, IFormFile[] files)
        {
            if (files != null && files.Length > 0)
            {
                await SaveFiles(product, files);
            }



            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

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

        private readonly IWebHostEnvironment _environment;
        private readonly IProductDetailRepository _repository;
        private readonly ResponseDto _reponse;
        private readonly IConfiguration _config;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductDetailController(IWebHostEnvironment environment, IProductDetailRepository repository, IConfiguration config, IMediator mediator, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _environment = environment;
            _repository = repository;
            _reponse = new ResponseDto();
            _config = config;
            _mediator = mediator;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        [HttpGet("GetListAsync")]
        public async Task<IActionResult> GetListAsync()
        {
            return Ok(await _unitOfWork._productDetailRepository.GetListAsync());
        }
        [HttpPost("GetPagedAsync")]
        public async Task<IActionResult> GetPagedAsync(PagedRequestDto request)
        {
            return Ok(await _unitOfWork._productDetailRepository.GetPagedAsync(request));
        }
        [HttpPost("GetPagedq")]
        public async Task<IActionResult> GetPaged(ProductDetailGetPageQuery request)
        {
            var vouchers = await _mediator.Send(request);
            return Ok(vouchers);
        }
        [AllowAnonymous]
        [HttpPost("PublicGetList")]
        public async Task<IActionResult> PublicGetList(GetProductDetailRequest request)
        {
            return Ok(await _unitOfWork._productDetailRepository.PublicGetList(request));
        }
        [AllowAnonymous]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            _reponse.Result = await _unitOfWork._productDetailRepository.GetById(id);
            return Ok(_reponse);
        }
        [AllowAnonymous]
        [HttpGet("PGetProductDetail")]
        public async Task<IActionResult> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? search, decimal? from, decimal? to, string? sortBy, int? page, string? productType, string? hangsx, string? ram, string? CPU, string? cardVGA)
        {

            var listProductDetail = await _repository.PGetProductDetail(getNumber, codeProductDetail, status, search, from, to, sortBy, page, productType, hangsx, ram, CPU, cardVGA);
            _reponse.Result = listProductDetail;
            return Ok(_reponse);
        }
        [HttpPost("CreateOrUpdateAsync")]
        public async Task<IActionResult> CreateOrUpdateAsync(CreateProductDetailDto objDto, IFormFile[] files)
        {
            var obj = _mapper.Map<ProductDetailEntity>(objDto);
            CreateOrUpdateProductDetailQuery query = new CreateOrUpdateProductDetailQuery();
            query.ProductDetailEntity = obj;
            return Ok(await _mediator.Send(query));
        }
        [HttpPost("Create")]
        public async Task<IActionResult> Create(ProductDetailCreateCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("CreateMany")]
        public async Task<IActionResult> CreateMany(List<ProductDetailEntity> list)
        {

            return BadRequest("Lỗi");
        }
        [HttpDelete("id")]
        public async Task<IActionResult> DeleteProductDetail(int id)
        {
            return Ok(await _repository.Delete(id));
        }
        [HttpPost]
        public async Task<ActionResult<CreateProductDetailDto>> PostProduct([FromForm] CreateProductDetailDto product, IFormFile[] files)
        {
            if (files != null && files.Length > 0)
            {
                await SaveFiles(product, files);
            }



            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }
        private async Task SaveFiles(CreateProductDetailDto product, IFormFile[] files)
        {
            string[] imageProperties = { "Image1", "Image2", "Image3", "Image4", "Image5", "Image6" };

            for (int i = 0; i < files.Length && i < imageProperties.Length; i++)
            {
                var file = files[i];
                if (file != null && file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(CreateProductDetailDto).GetProperty(imageProperties[i])?.SetValue(product, $"/uploads/{fileName}");
                }
            }
        }
        private async Task SaveFiles(CreateProductDetailDto product, IFormFile[] files)
        {
            string[] imageProperties = { "Image1", "Image2", "Image3", "Image4", "Image5", "Image6" };

            for (int i = 0; i < files.Length && i < imageProperties.Length; i++)
            {
                var file = files[i];
                if (file != null && file.Length > 0)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    typeof(CreateProductDetailDto).GetProperty(imageProperties[i])?.SetValue(product, $"/uploads/{fileName}");
                }
            }
        }
    }
}
