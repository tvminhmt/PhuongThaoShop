using Microsoft.EntityFrameworkCore;

using PTS.Core.Repositories;
using PTS.Core.Dto;
using PTS.Domain.Entities;
using PTS.Data;

namespace PTS.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
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
     public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(CartEntity obj)
        {
            var checkId = await _context.CartEntity.AnyAsync(x => x.UserEntityId == obj.UserEntityId);
            if (obj == null || checkId == true)
            {
                return false;
            }
            try
            {
                await _context.CartEntity.AddAsync(obj);
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
            var cart = await _context.CartEntity.FindAsync(id);
            if (cart == null)
            {
                return false;
            }
            try
            {
                _context.CartEntity.Remove(cart);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<CartEntity>> GetAll()
        {
            return await _context.CartEntity.ToListAsync();
        }

        public async Task<CartEntity> GetCartById(int id)
        {
            try
            {

                var cart = await _context.CartEntity.FirstOrDefaultAsync(x => x.UserEntityId == id);
                return cart;
            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<IEnumerable<CartItemDto>> GetCartItem(string username)
        {
            try
            {
                var user = await _context.UserEntity.FirstOrDefaultAsync(x => x.Username == username);// lấy danh sách người dùng trong database
                // Chú ý lấy trước rồi mới tìm để phân biệt được chữ hoa, chữ thường
                // Nếu tìm trực tiếp sẽ không phân biệt được chữ hoa, chữ thường
                // Lấy ra ìd người dùng//x => x.UserName == username
                // Dùng CartItemDto để hiển thị kết quả
                List<CartItemDto> cartItem = new List<CartItemDto>();
                cartItem = (
                           from x in await _context.CartEntity.Where(x=>x.UserEntityId== user.Id).ToListAsync()
                           join y in await _context.CartDetailEntity.ToListAsync() on x.UserEntityId equals y.CartEntityId
                           join productDetail in await _context.ProductDetailEntity.ToListAsync() on y.ProductDetailEntityId equals productDetail.Id
                           join product in await _context.ProductEntity.AsNoTracking().ToListAsync() on productDetail.ProductEntityId equals product.Id
                           join productType in await _context.ProductTypeEntity.AsNoTracking().ToListAsync() on product.ProductTypeEntityId equals productType.Id
                           join manufacturer in await _context.ManufacturerEntity.AsNoTracking().ToListAsync() on product.ManufacturerEntityId equals manufacturer.Id
                           join ram in await _context.RamEntity.Where(x => x.Id != null).ToListAsync() on productDetail.RamEntityId equals ram.Id into ramGroup
                           from ram in ramGroup.DefaultIfEmpty()
                           join cpu in await _context.CpuEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CpuEntityId equals cpu.Id into cpuGroup
                           from cpu in cpuGroup.DefaultIfEmpty()
                           join color in await _context.ColorEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ColorEntityId equals color.Id into colorGroup
                           from color in colorGroup.DefaultIfEmpty()
                           join hardDrive in await _context.HardDriveEntity.Where(x => x.Id != null).ToListAsync() on productDetail.HardDriveEntityId equals hardDrive.Id into hardDriveGroup
                           from hardDrive in hardDriveGroup.DefaultIfEmpty()
                           join cardVGA in await _context.CardVGAEntity.Where(x => x.Id != null).ToListAsync() on productDetail.CardVGAEntityId equals cardVGA.Id into cardVGAGroup
                           from cardVGA in cardVGAGroup.DefaultIfEmpty()
                           join screen in await _context.ScreenEntity.Where(x => x.Id != null).ToListAsync() on productDetail.ScreenEntityId equals screen.Id into screenGroup
                           from screen in screenGroup.DefaultIfEmpty()
                           select new CartItemDto// Dùng kiểu đối tượng ẩn danh (anonymous type)
                           {
                               Id = y.Id,
                               UserId = x.UserEntityId,
                               Quantity = y.Quantity,
                               Status = y.Status,
                               IdProductDetails = productDetail.Id,
                               MaProductDetail = productDetail.Code,
                               Price = productDetail.Price,
                               Description = productDetail.Description,
                               ThongSoRam = ram != null ? ram.ThongSo : string.Empty,
                               MaRam = ram != null ? ram.Ma : string.Empty,
                               TenCpu = cpu != null ? cpu.Ten : string.Empty,
                               MaCpu = cpu != null ? cpu.Ma : string.Empty,
                               ThongSoHardDrive = hardDrive != null ? hardDrive.ThongSo : string.Empty,
                               MaHardDrive = hardDrive != null ? hardDrive.Ma : string.Empty,
                               NameColor = color != null ? color.Name : string.Empty,
                               MaColor = color != null ? color.Ma : string.Empty,
                               MaCardVGA = cardVGA != null ? cardVGA.Ma : string.Empty,
                               TenCardVGA = cardVGA != null ? cardVGA.Ten : string.Empty,
                               ThongSoCardVGA = cardVGA != null ? cardVGA.ThongSo : string.Empty,
                               MaManHinh = screen != null ? screen.Ma : string.Empty,
                               KichCoManHinh = screen != null ? screen.KichCo : string.Empty,
                               TanSoManHinh = screen != null ? screen.TanSo : string.Empty,
                               ChatLieuManHinh = screen != null ? screen.ChatLieu : string.Empty,
                               NameProduct = product.Name,
                               NameManufacturer = manufacturer.Name
                               //  LinkImage = (a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1") != null) ? a.ImageEntities.FirstOrDefault(image => image.Ma == "Anh1").LinkImage : null,
                               // OtherImages = (a.ImageEntities.Where(image => image.Ma != "Anh1").Select(image => image.LinkImage).ToList()),
                           }
                    ).ToList();
                return cartItem;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<bool> Update(CartEntity obj)
        {
            var cart = await _context.CartEntity.FindAsync(obj.UserEntityId);
            if (cart == null)
            {
                return false;
            }
            try
            {
                cart.Description = obj.Description;
                _context.CartEntity.Update(cart);
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
