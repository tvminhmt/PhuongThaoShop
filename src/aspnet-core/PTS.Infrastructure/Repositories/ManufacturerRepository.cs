using Microsoft.EntityFrameworkCore;

using PTS.Core.Repositories;
using PTS.Core.Dto;
using PTS.Domain.Entities;
using PTS.Data;
using Abp.Application.Services.Dto;
using PTS.Base.Application.Dto;

namespace PTS.Infrastructure.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ManufacturerRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PagedResultDto<ManufacturerDto>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _dbContext.ManufacturerEntity.Where(x => !x.IsDeleted);

            var totalCount = await query.CountAsync();

            var obj = await query.Skip(request.SkipCount)
                                    .Take(request.MaxResultCount)
                                    .ToListAsync();

            var objDto = obj.Select(manufacturer => new ManufacturerDto
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name
            }).ToList();

            return new PagedResultDto<ManufacturerDto>(totalCount, objDto);
        }

        public async Task<bool> Create(ManufacturerEntity obj)
        {
            var check = await _dbContext.ManufacturerEntity.AnyAsync(p => p.Name == obj.Name);
            if (obj == null || check == true)
            {
                return false;
            }
            try
            {
                await _dbContext.ManufacturerEntity.AddAsync(obj);
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
            var obj = await _dbContext.ManufacturerEntity.FindAsync(id);
            if (obj == null)
            {
                return false;
            }
            try
            {
                obj.IsDeleted = true;
                _dbContext.ManufacturerEntity.Update(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ManufacturerEntity> GetById(int id)
        {
           return await _dbContext.ManufacturerEntity.FindAsync(id);
        }

        public async Task<IEnumerable<ManufacturerEntity>> GetList()
        {
           return await _dbContext.ManufacturerEntity.Where(a=>!a.IsDeleted).ToListAsync();
        }

        public async Task<bool> Update(ManufacturerEntity obj)
        {
            var manufacturer = await _dbContext.ManufacturerEntity.FindAsync(obj.Id);
            if (manufacturer == null)
            {
                return false;
            }
            try
            {
                manufacturer.Name = obj.Name;
                _dbContext.ManufacturerEntity.Update(manufacturer);
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
