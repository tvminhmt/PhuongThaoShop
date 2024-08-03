using Microsoft.EntityFrameworkCore;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Data;
using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using PTS.Application.Dto;

namespace PTS.Persistence.Repositories
{
    public class ScreenRepository : IScreenRepository
    {
        private readonly ApplicationDbContext _context;
        public ScreenRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResultDto<ScreenDto>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _context.ScreenEntity.Where(x => x.Status > 0);

            var totalCount = await query.CountAsync();

            var obj = await query.Skip(request.SkipCount)
                                    .Take(request.MaxResultCount)
                                    .ToListAsync();

            var objDto = obj.Select(screen => new ScreenDto
            {
                Id = screen.Id,
               Ma = screen.Ma,
               KichCo = screen.KichCo,
               TanSo = screen.TanSo,
               ChatLieu = screen.ChatLieu
            }).ToList();
            return new PagedResultDto<ScreenDto>(totalCount, objDto);
        }
        public async Task<bool> Create(ScreenEntity obj)
        {
            var check = await _context.ScreenEntity.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || check == true)
            {
                return false;
            }
            try
            {
                await _context.ScreenEntity.AddAsync(obj);
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
            var screen = await _context.ScreenEntity.FindAsync(id);
            if (screen == null)
            {
                return false;
            }
            try
            {
                screen.Status = 0;
                _context.ScreenEntity.Update(screen);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<ScreenEntity>> GetList()
        {
         return await _context.ScreenEntity.Where(x => x.Status > 0).ToListAsync();
        }

        public async Task<ScreenEntity> GetById(int id)
        {
            return  await _context.ScreenEntity.FindAsync(id);
        }
        public async Task<bool> Update(ScreenEntity obj)
        {
            var screen = await _context.ScreenEntity.FindAsync(obj.Id);
            if (screen == null)
            {
                return false;
            }
            try
            {
                screen.Ma = obj.Ma;
                screen.KichCo = obj.KichCo;
                screen.TanSo = obj.TanSo;
                screen.ChatLieu = obj.ChatLieu;
                _context.ScreenEntity.Update(screen);
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
