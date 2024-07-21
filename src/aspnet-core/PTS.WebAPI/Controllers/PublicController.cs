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
    }
}
