using Microsoft.EntityFrameworkCore;

using PTS.Core.Repositories;
using PTS.Core.Dto;
using PTS.Domain.Entities;
using PTS.Data;
using Abp.Application.Services.Dto;
using PTS.Base.Application.Dto;

namespace PTS.Infrastructure.Repositories
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductTypeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PagedResultDto<ProductTypeDto>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _dbContext.ProductTypeEntity.Where(x => !x.IsDeleted);

            var totalCount = await query.CountAsync();

            var obj = await query.Skip(request.SkipCount)
                                    .Take(request.MaxResultCount)
                                    .ToListAsync();

            var objDto = obj.Select(productType => new ProductTypeDto
            {
                Id = productType.Id,
                Name = productType.Name
            }).ToList();
            return new PagedResultDto<ProductTypeDto>(totalCount, objDto);
        }
        public async Task<bool> Create(ProductTypeEntity obj)
        {
            var check = await _dbContext.ProductTypeEntity.AnyAsync(x => x.Name == obj.Name);
            if (check || obj == null)
            {
                return false;
            }
            try
            {
                await _dbContext.ProductTypeEntity.AddAsync(obj);
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
            var productType = await _dbContext.ProductTypeEntity.FindAsync(id);
            if (productType == null)
            {
                return false;
            }
            try
            {
                productType.IsDeleted = true;
                _dbContext.ProductTypeEntity.Update(productType);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<ProductTypeEntity>> GetList()
        {
        return await _dbContext.ProductTypeEntity.Where(a=>!a.IsDeleted).ToListAsync();
        }

        public async Task<ProductTypeEntity> GetById(int id)
        {
            return await _dbContext.ProductTypeEntity.FindAsync(id);
        }

        public async Task<bool> Update(ProductTypeEntity obj)
        {
            var productType = await _dbContext.ProductTypeEntity.FindAsync(obj.Id);
            if (obj == null || productType == null)
            {
                return false;
            }
            try
            {
                productType.Name = obj.Name;
                _dbContext.ProductTypeEntity.Update(productType);
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
