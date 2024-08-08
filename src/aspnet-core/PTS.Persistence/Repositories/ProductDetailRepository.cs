using Microsoft.EntityFrameworkCore;
using PTS.Application.Interfaces.Repositories;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using System.Net.NetworkInformation;
using PTS.Data;
using System.Globalization;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace PTS.Persistence.Repositories
{
    public class ProductDetailRepository : IProductDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductDetailEntity>> GetListAsync()
        {
            return await _context.ProductDetailEntity.ToListAsync();
        }
        public async Task<PagedResultDto<ProductDetailDto>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _context.ProductDetailEntity;

            var totalCount = await query.CountAsync();

            var obj = await query.Skip(request.SkipCount)
                                    .Take(request.MaxResultCount)
                                    .ToListAsync();

            var objDto = obj.Select(p => new ProductDetailDto
            {
                Id = p.Id,
                Code = p.Code,
            }).ToList();

            return new PagedResultDto<ProductDetailDto>(totalCount, objDto);
        }
        public async Task<IEnumerable<ProductDetailDto>> PublicGetList(GetProductDetailRequest request)
        {
            var query = _context.ProductDetailEntity.AsNoTracking();
            if (!string.IsNullOrEmpty(request.ProductType))
            {
                query = query.Where(a => a.ProductEntity.ProductTypeEntity.Name == request.ProductType);
            }
            if (request.GetNumber > 0)
            {
                query = query.Take(Convert.ToInt32(request.GetNumber));
            }
            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(a => a.ProductEntity.Name.Contains(request.Search) || a.Code == request.Search);
            }
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                switch (request.SortBy)
                {
                    case "price_asc":
                        query = query.OrderBy(x => x.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(x => x.Price);
                        break;
                }
            }
            var result = await query
                             .Select(a => new ProductDetailDto
                             {
                                 Id = a.Id,
                                 Code = a.Code,
                                 Price = a.Price,
                                 Status = a.Status,
                                 Upgrade = a.Upgrade,
                                 Description = a.Description,
                                 AvailableQuantity = 1,
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
                             }).ToListAsync();

            foreach (var productDetail in result)
            {
                productDetail.AvailableQuantity = GetCountProductDetail(productDetail.Code);
            }

            return result;
        } 
        public async Task<ServiceResponse> Create(ProductDetailEntity obj)
        {
            var checkMa = await _context.ProductDetailEntity.AnyAsync(x => x.Code == obj.Code);
            if (obj == null || checkMa == true)
            {
                return new ServiceResponse(false, "Thêm thất bại");
            }
            try
            {

                await _context.ProductDetailEntity.AddAsync(obj);
                await _context.SaveChangesAsync();
                return new ServiceResponse(true, "Thêm thành công");
            }
            catch (Exception)
            {
                return new ServiceResponse(false, "Thêm thất bại");
            }
        }
        public async Task<ServiceResponse> CreateMany(List<ProductDetailEntity> list)
        {
            foreach (var i in list)
            {
                var checkMa = await _context.ProductDetailEntity.AnyAsync(x => x.Code == i.Code);
                if (checkMa == true)
                {
                    return new ServiceResponse(false, "Thêm thất bại");
                }
            }
            try
            {
                await _context.ProductDetailEntity.AddRangeAsync(list);
                await _context.SaveChangesAsync();
                return new ServiceResponse(true, "Thêm thành công");
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"Thêm thất bại: {e.Message}");
            }
        }
        public async Task<ServiceResponse> Delete(int id)
        {
            var productDetail = await _context.ProductDetailEntity.FindAsync(id);
            if (productDetail == null)
            {
                return new ServiceResponse(false, "Xóa thất bại");
            }
            try
            {
                productDetail.Status = 0;
                _context.ProductDetailEntity.Update(productDetail);
                await _context.SaveChangesAsync();
                return new ServiceResponse(true, "Xóa thành công");
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"Xóa thất bại: {e.Message}");
            }
        }
        private int GetCountProductDetail(string codeProductDetail)
        {
            int getCount = _context.ProductDetailEntity.Where(x => x.Status > 0 && x.Code == codeProductDetail)
           .Join(_context.SerialEntity, a => a.Id, b => b.ProductDetailEntityId, (a, b) => new ProductDetailEntity
           {
               Id = a.Id
           })
           .Count();
            return getCount;
        }
        public async Task<IEnumerable<ProductDetailDto>> PGetProductDetail(int? getNumber, string? codeProductDetail, int? status, string? search, decimal? from, decimal? to, string? sortBy, int? page,string? productType, string? namufacturer,string? ram, string? CPU, string? cardVGA)
        {
            var query = _context.ProductDetailEntity
                .AsNoTracking()
                .Where(a => (status == null || a.Status == status) && (codeProductDetail != null ? a.Code == codeProductDetail : true))
                .Select(a => new ProductDetailDto
                {
                    Id = a.Id,
                    Code = a.Code,
                    Price = a.Price,
                    Status = a.Status,
                    Upgrade = a.Upgrade,
                    Description = a.Description,
                    AvailableQuantity = 1,
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
                    NameManufacturer = a.ProductEntity.ManufacturerEntity.Name
                });

            if (getNumber > 0)
            {
                query = query.Take(Convert.ToInt32(getNumber));
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.NameProduct.Contains(search));
            }
            if (from.HasValue)
            {
                query = query.Where(x => x.Price >= from);
            }
            if (to.HasValue)
            {
                query = query.Where(x => x.Price <= to);
            }
            if (!string.IsNullOrEmpty(productType))
            {
                query = query.Where(x => x.NameProductType.Contains(productType));
            };

            if (!string.IsNullOrEmpty(namufacturer))
            {
                query = query.Where(x => x.NameManufacturer.Contains(namufacturer));
            };

            if (!string.IsNullOrEmpty(ram))
            {
                query = query.Where(x => x.ThongSoRam.Contains(ram));
            };

            if (!string.IsNullOrEmpty(CPU))
            {
                query = query.Where(x => x.TenCpu.Contains(CPU));
            };

            if (!string.IsNullOrEmpty(cardVGA))
            {
                query = query.Where(x => x.TenCardVGA.Contains(cardVGA));
            }; 
           


            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "nameproduct_desc":
                        query = query.OrderByDescending(x => x.NameProduct);
                        break;
                    case "price_asc":
                        query = query.OrderBy(x => x.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(x => x.Price);
                        break;
                }
            }

          //  var pageSize = Page_Size;
            var totalItems = await query.CountAsync();
          //  var totalPages = (int)Math.Ceiling(totalItems / (decimal)pageSize);
            //page = Math.Clamp((byte)page, 1, totalPages);
            //query = query.Skip((int)((page - 1) * pageSize)).Take(pageSize);

            var result = await query.ToListAsync();

            foreach (var productDetail in result)
            {
                productDetail.AvailableQuantity = GetCountProductDetail(productDetail.Code);
            }

            return result;
        }


        public async Task<ServiceResponse> Update(ProductDetailEntity obj)
        {
            var productDetail = await _context.ProductDetailEntity.FindAsync(obj.Id);
            if (productDetail == null)
            {
                return new ServiceResponse(false, "Cập nhật thất bại");
            }
            try
            {
                productDetail.Price = obj.Price;
                productDetail.Upgrade = obj.Upgrade;
                productDetail.Description = obj.Description;
                productDetail.Status = obj.Status;
                _context.ProductDetailEntity.Update(productDetail);
                await _context.SaveChangesAsync();
                return new ServiceResponse(true, "Cập nhật thành công");
            }
            catch (Exception e)
            {
                return new ServiceResponse(false, $"Cập nhật thất bại: {e.Message}");
            }
        }
        public async Task<bool> UpdateSoLuong(int id, int soLuong)
        // truyền vào 1 id + sl sản phẩm bán ra trong billdetail, giảm sl sp sau khi thanh toán
        {
            try
            {
                var productDetail = await _context.ProductDetailEntity.FindAsync(id);
                if (productDetail == null)
                {
                    return false;
                }
                //productDetail.AvailableQuantity -= soLuong;
                _context.ProductDetailEntity.Update(productDetail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<ProductDetailDto> GetById(int id)
        {
            var query = await _context.ProductDetailEntity
              .AsNoTracking()
            .Where(x=>x.Id == id)
            .Select(a => new ProductDetailDto
            {
                  Id = a.Id,
                  Code = a.Code,
                  Price = a.Price,
                  Status = a.Status,
                  Upgrade = a.Upgrade,
                  Description = a.Description,
                  AvailableQuantity = 1,
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
              }).FirstOrDefaultAsync();
            return query;
        }
        public async Task<Tuple<List<ProductDetailDto>, int>> GetPage(int pageIndex = 0, string orderBy = "", int pageSize = 7, DateTime? dateFr = null, DateTime? dateTo = null)
        {
            int rowCount = 0;
            List<ProductDetailDto> lLotteryKenos = new List<ProductDetailDto>();
            var conn = (SqlConnection)_context.Database.GetDbConnection();
            try
            {
                if (conn.State != ConnectionState.Open)
                {
                    await conn.OpenAsync();
                }

                DynamicParameters param = new DynamicParameters();

                if (dateFr != null)
                {
                    param.Add("@DateFr", dateFr, DbType.DateTime);
                }
                if (dateTo != null)
                {
                    param.Add("@DateTo", dateTo, DbType.DateTime);
                }
                if (!string.IsNullOrEmpty(orderBy))
                {
                    param.Add("@OrderBy", orderBy, DbType.String);
                }


                param.Add("@PageSize", pageSize, DbType.Int32);
                param.Add("@PageNumber", pageIndex, DbType.Int32);
                param.Add("@RowCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                lLotteryKenos = await conn
                    .QueryAsync<ProductDetailDto>("LotteryKenos_Search", param,
                        commandType: CommandType.StoredProcedure) as List<ProductDetailDto>;
                rowCount = param.Get<int>("RowCount");
            }
            finally
            {
                conn.Close();
            }

            return Tuple.Create(lLotteryKenos, rowCount);
        }
    }
}