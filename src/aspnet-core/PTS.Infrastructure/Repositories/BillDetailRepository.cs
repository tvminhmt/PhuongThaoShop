using Microsoft.EntityFrameworkCore;
using PTS.Data;
using PTS.Core.Repositories;
using PTS.Domain.Entities;

namespace PTS.Infrastructure.Repositories
{
    public class BillDetailRepository : IBillDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public BillDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateBillDetail(BillDetailEntity obj)
        {
            try
            {
                if (obj == null) return false;
                else
                {
                    await _context.BillDetailEntity.AddAsync(obj);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> DeleteBillDetail(int id)
        {
            try
            {
                var x = await _context.BillDetailEntity.FindAsync(id);
                if (x == null) return false;
                else
                {
                    _context.BillDetailEntity.Remove(x);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<List<BillDetailEntity>> GetAllBillDetails()
        {
            return await _context.BillDetailEntity.ToListAsync();
        }

        public async Task<BillDetailEntity> GetBillDetailById(int id)
        {
            return await _context.BillDetailEntity.FindAsync(id);
        }
        public async Task<bool> UpdateBillDetail(BillDetailEntity obj)
        {
            try
            {
                var x = await _context.BillDetailEntity.FindAsync(obj.Id);
                if (x == null) return false;
                else
                {
                    //     x.Quantity = obj.Quantity;
                    x.Price = obj.Price;
                    _context.BillDetailEntity.Update(x);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
