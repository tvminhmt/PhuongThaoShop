using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Data;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Dto;

namespace PTS.Persistence.Repositories
{
    public class CartDetailRepository : ICartDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public CartDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartDetailEntity obj)
        {

            if (obj == null)
            {
                return false;
            }
            try
            {
                await _context.CartDetailEntity.AddAsync(obj);
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
            var cartDT = await _context.CartDetailEntity.FindAsync(id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                _context.CartDetailEntity.Remove(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartDetailEntity>> GetAll()
        {
            return await _context.CartDetailEntity.ToListAsync();
        }

        public async Task<CartDetailEntity> GetById(int id)
        {
            return await _context.CartDetailEntity.FindAsync(id);
        }

        public async Task<ServiceResponse> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return new ServiceResponse(false, "Cập nhật thất bại");
            }
            var sl = GetCount(cartDT.ProductDetailEntityId);
            if(obj.Quantity <= sl)
            {
                try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return new ServiceResponse(true, "Cập nhật hành công");
            }
            catch (Exception)
            {
                return new ServiceResponse(false, "Cập nhật thất bại");
            }
            }
            else
            {
                return new ServiceResponse(false, $"Cập nhật thất bại. Bạn chỉ có thể thêm tối đa {sl} sản phẩm");
            }
            
        }
        private int GetCount(int id)
        {
            int getCount = _context.ProductDetailEntity.ToList()
                .Where(x => x.Status > 0 && x.Id == id)
                .Join( _context.SerialEntity.Where(x => x.BillDetailEntityId == null),
                      a => a.Id,
                      b => b.ProductDetailEntityId,
                      (a, b) => new { a.Id })
                .Count();
            return getCount;
        }
    }
}
