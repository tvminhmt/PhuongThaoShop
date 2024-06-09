//using Microsoft.EntityFrameworkCore;

//using PTS.Core.Repositories;
//using PTS.Core.Dto;
//using PTS.Domain.Entities;

//namespace PTS.Infrastructure.Repositories
//{
//    public class GiamGiaRepository : IGiamGiaRepository
//    {
//        private readonly ApplicationDbContext _context;
//        public GiamGiaRepository(ApplicationDbContext context)
//        {
//            _context = context;
//        }


//        public async Task<GiamGiaEntity> GetGiamGiaByPromotionType(string promotionType)
//        {
//            try
//            {
//                return await _context.GiamGias
//                    .Where(g => g.LoaiGiamGia == promotionType && g.TrangThai != 0)
//                    .FirstOrDefaultAsync();
//            }
//            catch (Exception)
//            {
//                return null;
//            }
//        }
//        public async Task<bool> Create(GiamGiaEntity obj)
//        {
//            var checkMa = await _context.GiamGias.AnyAsync(x => x.Ma == obj.Ma);// tìm mã, trả về true nếu đã có, false nếu chưa có
//            if (obj == null || checkMa == true)
//            {
//                return false;
//            }
//            try
//            {
//                await _context.GiamGiaEntity.AddAsync(obj);
//                await _context.SaveChangesAsync();
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }

//        public async Task<bool> Delete(int id)
//        {
//            var giamGia = await _context.GiamGiaEntity.FindAsync(id);
//            if (giamGia == null)
//            {
//                return false;
//            }
//            try
//            {
//                _context.GiamGiaEntity.Remove(giamGia);
//                await _context.SaveChangesAsync();
//                return true;

//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }

//        public async Task<List<GiamGiaEntity>> GetAllGiamGias()
//        {
//            var allList = await _context.GiamGiaEntity.ToListAsync();
//            var list = allList.Where(x => x.TrangThai != 0).ToList();
//            return list;
//        }

//        public async Task<bool> Update(GiamGiaEntity obj)
//        {
//            var giamGia = await _context.GiamGias.FindAsync(obj.Id);

//            if (giamGia == null)
//            {
//                return false;
//            }
//            try
//            {
//                giamGia.Ten = obj.Ten;
//                giamGia.NgayBatDau = obj.NgayBatDau;
//                giamGia.NgayKetThuc = obj.NgayKetThuc;
//                giamGia.MucGiamGiaPhanTram = obj.MucGiamGiaPhanTram;
//                giamGia.MucGiamGiaTienMat = obj.MucGiamGiaTienMat;
//                giamGia.LoaiGiamGia = obj.LoaiGiamGia;
//                giamGia.TrangThai = obj.TrangThai;
//                _context.GiamGias.Update(giamGia);
//                await _context.SaveChangesAsync();
//                return true;
//            }
//            catch (Exception)
//            {
//                return false;
//            }
//        }
//    }
//}
