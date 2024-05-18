using PTS.Core.Repositories;
using PTS.Domain.Entities;
using PTS.Data;
using Microsoft.EntityFrameworkCore;

namespace PTS.Infrastructure.Repositories
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    /*
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
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

        public async Task<bool> UpdateQuantity(CartDetailEntity obj)
        {
            var cartDT = await _context.CartDetailEntity.FindAsync(obj.Id);
            if (cartDT == null)
            {
                return false;
            }
            try
            {
                cartDT.Quantity = obj.Quantity;
                _context.CartDetailEntity.Update(cartDT);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
     */
}
