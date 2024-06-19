using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.DTOs;
using PTS.Application.Extensions;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.ProductDetail.Queries
{
	public record PProductDetailGetPageQuery : BaseGetPageQuery, IRequest<PaginatedResult<PProductDetailGetPageDto>>
	{
		public string? Manufacturer { get; set; }
		public string? ProductType { get; set; }
		public string? Ram {  get; set; }
		public string? CPU { get; set; }
		public string? CardVGA { get; set; }
		public string? HardDrive { get; set; }
		public string? Screen { get; set; }
		public string? Search { get; set; }
		public string? SortBy { get; set; }
	}

	internal class PProductDetailGetPagesQueryHandler : IRequestHandler<PProductDetailGetPageQuery, PaginatedResult<PProductDetailGetPageDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ISender _sender;

		public PProductDetailGetPagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISender sender)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_sender = sender;
		}

		public async Task<PaginatedResult<PProductDetailGetPageDto>> Handle(PProductDetailGetPageQuery queryInput, CancellationToken cancellationToken)
		{
			var query = _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking();

			if (!string.IsNullOrEmpty(queryInput.ProductType))
			{
				query = query.Where(a => a.ProductEntity.ProductTypeEntity.Name == queryInput.ProductType);
			}

			if (!string.IsNullOrEmpty(queryInput.Keywords))
			{
				query = query.Where(a => a.ProductEntity.Name.Contains(queryInput.Keywords) || a.Code.Contains(queryInput.Keywords));
			}

			if (!string.IsNullOrEmpty(queryInput.SortBy))
			{
				switch (queryInput.SortBy)
				{
					case "price_asc":
						query = query.OrderBy(x => x.Price);
						break;
					case "price_desc":
						query = query.OrderByDescending(x => x.Price);
						break;
				}
			}

			var pQuery = query.Select(a => new PProductDetailGetPageDto
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
				PhanTramGiamGia = Convert.ToInt32((((a.OldPrice - a.Price) / a.OldPrice) * 100))
			});

			var result = await pQuery.ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);

			foreach (var productDetail in result.Data)
			{
				productDetail.AvailableQuantity = GetCountProductDetail(productDetail.Code);
			}

			return result;
		}

		private int GetCountProductDetail(string codeProductDetail)
		{
			int getCount = _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking()
				.Where(x => x.Status > 0 && x.Code == codeProductDetail)
				.Join(_unitOfWork.Repository<SerialEntity>().Entities.AsNoTracking(),
					  a => a.Id,
					  b => b.ProductDetailEntityId,
					  (a, b) => new { a.Id })
				.Count();
			return getCount;
		}
	}
}
