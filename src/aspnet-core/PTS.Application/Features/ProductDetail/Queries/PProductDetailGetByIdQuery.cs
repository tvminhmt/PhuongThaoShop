using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.ProductDetail.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.ProductDetail.Queries
{
	public record PProductDetailGetByIdQuery : IRequest<Result<PProductDetailGetByIdDto>>
	{
		public int Id { get; set; }
	}

	internal class PProductDetailGetByIdQueryHandler : IRequestHandler<PProductDetailGetByIdQuery, Result<PProductDetailGetByIdDto>>
	{
		private readonly IUnitOfWork _unitOfWork;

		public PProductDetailGetByIdQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<PProductDetailGetByIdDto>> Handle(PProductDetailGetByIdQuery queryInput, CancellationToken cancellationToken)
		{
			try
			{
				var imageQuery = _unitOfWork.Repository<ImageEntity>().Entities.AsNoTracking();
				var productQuery = _unitOfWork.Repository<ProductDetailEntity>().Entities
					.Where(x => x.Id == queryInput.Id)
					.AsNoTracking();

				var query = from a in productQuery
							join pImage in _unitOfWork.Repository<ProductDetailImage>().Entities.Where(x => x.IsIndex).AsNoTracking()
								 on a.Id equals pImage.ProductDetailId into pImageGroup
							from pImage in pImageGroup.DefaultIfEmpty()
							select new PProductDetailGetByIdDto
							{
								Id = a.Id,
								Code = a.Code,
								OldPrice = a.OldPrice,
								Price = a.Price,
								Status = a.Status,
								Upgrade = a.Upgrade,
								Description = a.Description,
								AvailableQuantity = 0,
								ThongSoRam = a.RamEntity.ThongSo,
								MaRam = a.RamEntity.Ma,
								TenCpu = a.CpuEntity.Ten,
								MaCpu = a.CpuEntity.Ma,
								ThongSoHardDrive = a.HardDriveEntity.ThongSo,
								MaHardDrive = a.HardDriveEntity.Ma,
								NameColor = a.ColorEntity.Name,
								MaColor = a.ColorEntity.Ma,
								MaCardVGA = a.CardVGAEntity.Ma,
								TenCardVGA = a.CardVGAEntity.Ten,
								ThongSoCardVGA = a.CardVGAEntity.ThongSo,
								MaManHinh = a.ScreenEntity.Ma,
								KichCoManHinh = a.ScreenEntity.KichCo,
								TanSoManHinh = a.ScreenEntity.TanSo,
								ChatLieuManHinh = a.ScreenEntity.ChatLieu,
								NameProduct = a.ProductEntity.Name,
								NameProductType = a.ProductEntity.ProductTypeEntity.Name,
								NameManufacturer = a.ProductEntity.ManufacturerEntity.Name,
								PhanTramGiamGia = Convert.ToInt32((((a.OldPrice - a.Price) / a.OldPrice) * 100)),
								IdImage = pImage != null ? pImage.ImageId : 0,
								ListImage = new List<string>()
							};

				var result = await query.SingleOrDefaultAsync(cancellationToken);

				if (result == null)
				{
					return await Result<PProductDetailGetByIdDto>.FailureAsync("Product not found.");
				}

				result.AvailableQuantity = GetCount(result.Id);

				if (result.IdImage > 0)
				{
					var list = await imageQuery.Where(x => x.Id == result.IdImage).ToListAsync(cancellationToken);
					if (list != null)
					{
						foreach (var item in list)
						{
							result.ListImage.Add(item.Url);
						}
					}

				}
				else
				{
					result.ImageMain = "/uploads/noimage.jpg";
				}

				return await Result<PProductDetailGetByIdDto>.SuccessAsync(result);
			}
			catch (Exception ex)
			{
				// Handle exception (log it, rethrow it, etc.)
				return await Result<PProductDetailGetByIdDto>.FailureAsync(ex.Message);
			}
		}

		private int GetCount(int id)
		{
			int getCount = _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking()
				.Where(x => x.Status > 0 && x.Id == id)
				.Join(_unitOfWork.Repository<SerialEntity>().Entities.AsNoTracking(),
					  a => a.Id,
					  b => b.ProductDetailEntityId,
					  (a, b) => new { a.Id })
				.Count();
			return getCount;
		}
	}
}
