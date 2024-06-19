using Microsoft.EntityFrameworkCore;

using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Data;

namespace PTS.Persistence.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly ApplicationDbContext _context;
        public ImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Create(ImageEntity obj)
        {
            var checkMa = await _context.ImageEntity.AnyAsync(x => x.Ma == obj.Ma);// tìm mã, trả về true nếu đã có, false nếu chưa có
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _context.ImageEntity.AddAsync(obj);
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
            var img = await _context.ImageEntity.FindAsync(id);
            if (img == null)
            {
                return false;
            }
            try
            {
                img.Status = 0;
                _context.ImageEntity.Update(img);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ImageEntity>> GetAllImage()
        {
            var list = await _context.ImageEntity.AsQueryable().Where(x=>x.Status!=0).ToListAsync();
            return list;
        }

        public async Task<bool> Update(ImageEntity obj)
        {
            var img = await _context.ImageEntity.FindAsync(obj.Id);
            if (img == null)
            {
                return false;
            }
            try
            {

              
                img.Status = obj.Status;
                img.LinkImage = obj.LinkImage;
                _context.ImageEntity.Update(img);
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
