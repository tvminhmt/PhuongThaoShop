 using Microsoft.EntityFrameworkCore;
using PTS.Application.Interfaces.Repositories;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Data;
using Abp.Application.Services.Dto;
using PTS.Shared.Dto;

namespace PTS.Persistence.Repositories
{
    public class HardDriveRepository : IHardDriveRepository
    {
        private readonly ApplicationDbContext _context;

        public HardDriveRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResultDto<HardDriveDto>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _context.HardDriveEntity.Where(x => x.Status > 0);

            var totalCount = await query.CountAsync();

            var obj = await query.Skip(request.SkipCount)
                                    .Take(request.MaxResultCount)
                                    .ToListAsync();

            var objDto = obj.Select(hardDrive => new HardDriveDto
            {
                Id = hardDrive.Id,
                Ma = hardDrive.Ma,
                ThongSo = hardDrive.ThongSo
            }).ToList();

            return new PagedResultDto<HardDriveDto>(totalCount, objDto);
        }
        public async Task<bool> Create(HardDriveEntity obj)
        {
            var checkMa = await _context.HardDriveEntity.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa)
            {
                return false;
            }
            try
            {
                await _context.HardDriveEntity.AddAsync(obj);
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
            var hardDrive = await _context.HardDriveEntity.FindAsync(id);
            if (hardDrive == null)
            {
                return false;
            }
            try
            {
                hardDrive.Status = 0;
                _context.HardDriveEntity.Update(hardDrive);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<HardDriveEntity>> GetList()
        {
             return await _context.HardDriveEntity.Where(x => x.Status > 0).ToListAsync();
        }

        public async Task<HardDriveEntity> GetById(int id)
        {
           return await _context.HardDriveEntity.FindAsync(id);
        }

        public async Task<bool> Update(HardDriveEntity obj)
        {
            var hardDrive = await _context.HardDriveEntity.FindAsync(obj.Id);
            if (hardDrive == null)
            {
                return false;
            }
            try
            {
                hardDrive.Ma = obj.Ma;
                hardDrive.ThongSo = obj.ThongSo;
                _context.HardDriveEntity.Update(hardDrive);
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
