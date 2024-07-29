using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Mappings;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PTS.Application.Features.Voucher.Commands
{
    public record ProductDetailCreateCommand : IRequest<Result<int>>, IMapFrom<ProductDetailEntity>
    {
		[Required]
		[MaxLength(50)]
		public string? Code { get; set; }
		public Decimal Price { get; set; }
		public Decimal OldPrice { get; set; }
		public IFormFile[] Files { get; set; }
		public string? Image1 { get; set; }
		public string? Image2 { get; set; }
		public string? Image3 { get; set; }
		public string? Image4 { get; set; }
		public string? Image5 { get; set; }
		public string? Image6 { get; set; }
		public string? Upgrade { get; set; }
		public string? Description { get; set; }
		public int ProductEntityId { get; set; }
		public int? ColorEntityId { get; set; }
		public int? RamEntityId { get; set; }
		public int? CpuEntityId { get; set; }
		public int? HardDriveEntityId { get; set; }
		public int? ScreenEntityId { get; set; }
		public int? CardVGAEntityId { get; set; }
		public DateTime CrDateTime { get; set; }
		public int Status { get; set; }
	}

    internal class ProductDetailCreateCommandHandler : IRequestHandler<ProductDetailCreateCommand, Result<int>>
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IWebHostEnvironment _environment;
		public ProductDetailCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_environment = environment;
		}
		public async Task<Result<int>> Handle(ProductDetailCreateCommand command, CancellationToken cancellationToken)
        {
            try
            {
				bool isAny =await _unitOfWork.Repository<ProductDetailEntity>().Entities.AnyAsync(a=> a.Code == command.Code);
				if(!isAny)
				{
                var entity = _mapper.Map<ProductDetailEntity>(command);
                entity.CrDateTime = DateTime.Now;
					await SaveFiles(entity, command.Files);
				
					await _unitOfWork.Repository<ProductDetailEntity>().AddAsync(entity); 
                var result = await _unitOfWork.Save(cancellationToken);
                return result > 0
                    ? await Result<int>.SuccessAsync(entity.Id, "Thêm dữ liệu thành công.")
                    : await Result<int>.FailureAsync("Thêm dữ liệu không thành công.");
				}
				else
				{
					return await Result<int>.FailureAsync($"Thêm dữ liệu không thành công: Mã đã tồn tại");
				}
			
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync($"Thêm dữ liệu không thành công: {ex.Message}");
            }
        }
		private async Task SaveFiles(ProductDetailEntity entity, IFormFile[] files)
		{
			string[] imageProperties = { "Image1", "Image2", "Image3", "Image4", "Image5", "Image6" };

			for (int i = 0; i < files.Length && i < imageProperties.Length; i++)
			{
				var file = files[i];
				if (file != null && file.Length > 0)
				{
					var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
					var filePath = Path.Combine(_environment.WebRootPath, "Images/ImagesProduct", fileName);

					using (var stream = new FileStream(filePath, FileMode.Create))
					{
						await file.CopyToAsync(stream);
					}

					// Set image path to entity property
					typeof(ProductDetailEntity).GetProperty(imageProperties[i])?.SetValue(entity, $"/Images/ImagesProduct/{fileName}");
				}
			}
		}
	}
}
