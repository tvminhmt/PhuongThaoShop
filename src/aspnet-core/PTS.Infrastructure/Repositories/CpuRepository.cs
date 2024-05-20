using Microsoft.EntityFrameworkCore;
using PTS.Core.Repositories;
using PTS.Core.Dto;
using PTS.Domain.Entities;
using PTS.Data;
using Abp.Application.Services.Dto;
using PTS.Base.Application.Dto;

namespace PTS.Infrastructure.Repositories
{
    public class CpuRepository : ICpuRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CpuRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PagedResultDto<CpuDto>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _dbContext.CpuEntity.Where(x => !x.IsDeleted);

            var totalCount = await query.CountAsync();

            var obj = await query.Skip(request.SkipCount)
                                    .Take(request.MaxResultCount)
                                    .ToListAsync();

            var objDto = obj.Select(cpu => new CpuDto
            {
                Id = cpu.Id,
                Ma = cpu.Ma,
                Ten = cpu.Ten
            }).ToList();

            return new PagedResultDto<CpuDto>(totalCount, objDto);
        }
        public async Task<bool> Create(CpuEntity obj)
        {
            var checkMa = await _dbContext.CpuEntity.AnyAsync(x => x.Ma == obj.Ma);
            if (obj == null || checkMa == true)
            {
                return false;
            }
            try
            {
                await _dbContext.CpuEntity.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> Delete(int id)
        {
            var obj = await _dbContext.CpuEntity.FindAsync(id);
            if (obj == null)
            {
                return false;
            }
            try
            {
                obj.IsDeleted = true;
                _dbContext.CpuEntity.Update(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<CpuEntity>> GetList()
        {
            return await _dbContext.CpuEntity.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<CpuEntity> GetById(int id)
        {
          return await _dbContext.CpuEntity.FindAsync(id);
        }

        public async Task<bool> Update(CpuEntity obj)
        {
            var cpu = await _dbContext.CpuEntity.FindAsync(obj.Id);
            if (cpu == null)
            {
                return false;
            }
            try
            {
                cpu.Ma = obj.Ma;
                cpu.Ten = obj.Ten;
                _dbContext.CpuEntity.Update(cpu);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

