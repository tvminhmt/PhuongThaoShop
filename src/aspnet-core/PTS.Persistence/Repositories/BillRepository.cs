using Microsoft.EntityFrameworkCore;

using PTS.Application.Interfaces.Repositories;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Data;
using PTS.Shared.Dto;

namespace PTS.Persistence.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly ApplicationDbContext _context;
        public BillRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(BillEntity obj)
        {
            try
            {
                await _context.BillEntity.AddAsync(obj);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            // Check trạng thái hóa đơn hoàn thành là không được phép xóa
            var bill = await _context.BillEntity.FindAsync(id);
            if (bill == null || bill.Status == 5)
            {
                return false;
            }
            try
            {
                bill.Status = 0;
                _context.BillEntity.Update(bill);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<BillEntity>> GetAll()
        {
            return await _context.BillEntity.Where(a => a.Status > 0).AsNoTracking().ToListAsync();
        }
        public async Task<PagedResult<BillEntity>> GetPage(GetPageBillDto request)
        {
            var query = _context.BillEntity
                                .Where(a => a.Status > 0);

            if (!string.IsNullOrEmpty(request.Code))
            {
                query = query.Where(a => a.InvoiceCode.Contains(request.Code));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                              .AsNoTracking()
                              .Skip((request.PageNumber - 1) * request.PageSize)
                              .Take(request.PageSize)
                              .ToListAsync();

            return new PagedResult<BillEntity>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }

        public async Task<BillDto> GetBillByInvoiceCode(string invoiceCode)
        {
            var query = from bill in _context.BillEntity
                        where bill.InvoiceCode == invoiceCode
                        join v in _context.VoucherEntity on bill.VoucherEntityId equals v.Id into voucherGroup
                        from voucher in voucherGroup.DefaultIfEmpty()
                        select new BillDto
                        {
                            InvoiceCode = bill.InvoiceCode,
                            PhoneNumber = bill.PhoneNumber,
                            FullName = bill.FullName,
                            Address = bill.Address,
                            Status = bill.Status,
                            CreateDate = bill.CrDateTime,
                            GiamGia = voucher != null ? voucher.GiaTri : 0,
                            CodeVoucher = voucher != null ? voucher.MaVoucher : null,
                            UserId = bill.UserEntityId
                        };

            return await query.AsNoTracking().FirstOrDefaultAsync();

            // await _context.BillEntity.AsNoTracking().FirstOrDefaultAsync(x => x.InvoiceCode == invoiceCode);
        }

        public async Task<IEnumerable<BillDetailDto>> GetBillDetailByInvoiceCode(string invoiceCode)
        {
            try
            {
                var billDetails = await (
                    from x in _context.BillEntity.AsNoTracking().Where(a => a.InvoiceCode == invoiceCode)
                    join y in _context.BillDetailEntity.AsNoTracking() on x.Id equals y.BillEntityId
                    //join z in _context.Serials.AsNoTracking() on y.Id equals z.BillDetailId
                    //join o in _context.ProductDetailEntity.AsNoTracking().Where(a => a.Status > 0) on z.ProductDetailId equals o.Id
                    select new BillDetailDto
                    {
                        CodeProductDetail = y.CodeProductDetail,
                        Quantity = y.Quantity,
                        Price = y.Price
                    }).ToListAsync();
                return billDetails;
            }
            catch (Exception)
            {
                return null;
            }

        }


        public Task<IEnumerable<BillDetailDto>> GetBillDetail(string UserName)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Update(BillEntity obj)
        {
            var bill = await _context.BillEntity.FindAsync(obj.Id);
            if (bill == null)
            {
                return false;
            }
            try
            {
                bill.PhoneNumber = obj.PhoneNumber;
                bill.FullName = obj.FullName;
                bill.Address = obj.Address;
                bill.Status = obj.Status;
                _context.BillEntity.Update(bill);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
