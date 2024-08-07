using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.DTOs;
using PTS.Application.Extensions;
using PTS.Application.Features.ProductDetail.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.ProductDetail.Queries
{
    public record PProductDetailGetPageQuery : BaseGetPageQuery, IRequest<PaginatedResult<PProductDetailGetPageDto>>
    {
        public int? Manufacturer { get; set; }
        public string? ProductType { get; set; }
        public int? Ram { get; set; }
        public string? Cpu { get; set; }
        public string? CardVGA { get; set; }
        public string? HardDrive { get; set; }
        public string? Screen { get; set; }
        public string? Search { get; set; }
        public int? Price { get; set; }
        public int? SortBy { get; set; }
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
            try
            {
                var image = _unitOfWork.Repository<ImageEntity>().Entities.Where(x => x.Status != (int)StatusEnum.Delete).AsNoTracking();
                var query = (from a in _unitOfWork.Repository<ProductDetailEntity>().Entities.Where(x => x.Status == 1).AsNoTracking()
                             join pImage in _unitOfWork.Repository<ProductDetailImage>().Entities.Where(x => x.IsIndex).AsNoTracking()
                                  on a.Id equals pImage.ProductDetailId into pImageGroup
                             from pImage in pImageGroup.DefaultIfEmpty()
                             select new PProductDetailGetPageDto
                             {
                                 Id = a.Id,
                                 Code = a.Code,
                                 OldPrice = a.OldPrice,
                                 Price = a.Price,
                                 Status = a.Status,
                                 Upgrade = a.Upgrade,
                                 Description = a.Description,
                                 CrDateTime = a.CrDateTime,
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
                                 ManufacturerEntityId = a.ProductEntity.ManufacturerEntityId,
                                 NameManufacturer = a.ProductEntity.ManufacturerEntity.Name,
                                 NewPrice = a.Price - (a.Discount != null ? a.Discount.Percentage : 0),
                                 PhanTramGiamGia = a.Discount != null && a.Price != 0 ? Convert.ToInt32((a.Discount.Percentage / a.Price) * 100) : 0,
                                 IdImage = pImage != null ? pImage.ImageId : 0
                             });
                if (queryInput.Manufacturer > 0)
                {
                    query = query.Where(x => x.ManufacturerEntityId == queryInput.Manufacturer);
                }
                switch (queryInput.Price)
                {
                    case 1:
                        query = query.Where(x => x.Price >= 0 && x.Price <= 10000000);
                        break;
                    case 2:
                        query = query.Where(x => x.Price > 10000000 && x.Price <= 20000000);
                        break;
                    case 3:
                        query = query.Where(x => x.Price > 20000000 && x.Price <= 30000000);
                        break;
                    case 4:
                        query = query.Where(x => x.Price > 30000000 && x.Price <= 40000000);
                        break;
                    case 5:
                        query = query.Where(x => x.Price > 40000000 && x.Price <= 50000000);
                        break;
                    case 6:
                        query = query.Where(x => x.Price > 50000000);
                        break;
                    default:
                        break;
                }

                switch (queryInput.Ram)
                {
                    case 1:
                        query = query.Where(x => x.ThongSoRam.Contains("4GB"));
                        break;
                    case 2:
                        query = query.Where(x => x.ThongSoRam.Contains("8GB"));
                        break;
                    case 3:
                        query = query.Where(x => x.ThongSoRam.Contains("12GB"));
                        break;
                    case 4:
                        query = query.Where(x => x.ThongSoRam.Contains("16GB"));
                        break;
                    case 5:
                        query = query.Where(x => x.ThongSoRam.Contains("24GB"));
                        break;
                    case 6:
                        query = query.Where(x => x.ThongSoRam.Contains("32GB"));
                        break;
                    case 7:
                        query = query.Where(x => x.ThongSoRam.Contains("40GB"));
                        break;
                    case 8:
                        query = query.Where(x => x.ThongSoRam.Contains("64GB"));
                        break;
                    default:
                        break;
                }
                switch (queryInput.SortBy)
                {
                    case 1:
                        query = query.OrderBy(x => x.Price);
                        break;
                    case 2:
                        query = query.OrderByDescending(x => x.Price);
                        break;
                    case 3:
                        query = query.OrderBy(x => x.CrDateTime);
                        break;
                    case 4:
                        query = query.OrderBy(x => x.NameProduct);
                        break;
                    default:
                        break;
                }

                if (!string.IsNullOrEmpty(queryInput.Cpu))
                {
                    query = query.Where(a => a.MaCpu.Contains(queryInput.Cpu) || a.TenCpu.Contains(queryInput.Cpu));
                }
                if (!string.IsNullOrEmpty(queryInput.CardVGA))
                {
                    query = query.Where(a => a.MaCardVGA.Contains(queryInput.CardVGA) || a.MaCardVGA.Contains(queryInput.CardVGA));
                }
                var result = await query.ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);

                foreach (var productDetail in result.Data)
                {
                    productDetail.AvailableQuantity = GetCount(productDetail.Id);
                    if (productDetail.IdImage > 0)
                    {
                        productDetail.ImageMain = image.FirstOrDefault(x => x.Id == productDetail.IdImage).Url;
                    }
                    else
                    {
                        productDetail.ImageMain = "/uploads/noimage.jpg";
                    }
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private int GetCount(int id)
        {
            int getCount = _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking()
                .Where(x => x.Status > 0 && x.Id == id)
                .Join(_unitOfWork.Repository<SerialEntity>().Entities.AsNoTracking().Where(x => x.BillDetailEntityId == null),
                      a => a.Id,
                      b => b.ProductDetailEntityId,
                      (a, b) => new { a.Id })
                .Count();
            return getCount;
        }
    }
}
