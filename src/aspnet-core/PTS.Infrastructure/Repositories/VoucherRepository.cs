using Microsoft.EntityFrameworkCore;

using PTS.Core.Repositories;
using PTS.Core.Dto;
using PTS.Domain.Entities;
using PTS.Data;

namespace PTS.Infrastructure.Repositories
{
    public class VoucherRepository : IVoucherRepository
    {
        private readonly ApplicationDbContext _context;
        public VoucherRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(VoucherEntity obj)
        {
            var checkMa = await _context.VoucherEntity.AnyAsync(x => x.MaVoucher == obj.MaVoucher);
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.VoucherEntity.AddAsync(obj);
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
            var voucher = await _context.VoucherEntity.FindAsync(id);
            if (voucher == null)
            {
                return false;
            }
            try
            {
                voucher.IsDeleted = true;
                _context.VoucherEntity.Update(voucher);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<VoucherEntity>> GetAll()
        {
            return await _context.VoucherEntity.Where(v => !v.IsDeleted).ToListAsync();
        }

        public async Task<bool> Update(VoucherEntity obj)
        {
            var vou = await _context.VoucherEntity.FindAsync(obj.Id);
            if (vou == null)
            {
                return false;
            }
            if (obj.EndDay <= obj.StarDay)
            {
                return false;
            }
            try
            {
                vou.TenVoucher = obj.TenVoucher;
                vou.StarDay = obj.StarDay;
                vou.SoLuong = obj.SoLuong;
                vou.EndDay = obj.EndDay;
                vou.GiaTri = obj.GiaTri;
                _context.VoucherEntity.Update(vou);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UpdateSL(string codeVoucher)
        {
            var vou = await _context.VoucherEntity.FirstOrDefaultAsync(x => x.MaVoucher == codeVoucher);
            if (vou == null)
            {
                return false;
            }
            try
            {
                vou.SoLuong--;

                _context.VoucherEntity.Update(vou);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<VoucherEntity> GetByCode(string codeVoucher)
        {
            var result = await _context.VoucherEntity.FirstOrDefaultAsync(x => x.MaVoucher == codeVoucher);
            return result;
        }
        public async Task<bool> Duyet(int Id)
        {
            var vou = await _context.VoucherEntity.FindAsync(Id);

            if (vou == null)
            {
                return false;
            }
            else
            {
                try
                {
               //     vou.Status = 1;
                    _context.VoucherEntity.Update(vou);
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        public async Task<bool> HuyDuyet(int Id)
        {
            var vou = await _context.VoucherEntity.FindAsync(Id);
            if (vou == null)
            {
                return false;
            }
            else
            {
                try
                {
              //      vou.Status = 0;
                    _context.VoucherEntity.Update(vou);
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
}

