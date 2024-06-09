using Microsoft.EntityFrameworkCore;

using PTS.Core.Repositories;
using PTS.Core.Dto;
using PTS.Domain.Entities;
using System.Drawing.Printing;
using PTS.Data;
using Abp.Application.Services.Dto;
using PTS.Base.Application.Dto;

namespace PTS.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext)
        {
           _dbContext = dbContext;  
        }
        public async Task<PagedResultDto<ProductDto>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _dbContext.ProductEntity.Where(x => !x.IsDeleted);

            var totalCount = await query.CountAsync();

            var obj = await query.Skip(request.SkipCount)
                                    .Take(request.MaxResultCount)
                                    .ToListAsync();

            var objDto = obj.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ProductTypeEntityId = product.ProductTypeEntityId,
                ManufacturerEntityId = product.ManufacturerEntityId,
            }).ToList();

            return new PagedResultDto<ProductDto>(totalCount, objDto);
        }
        public async Task<bool> Create(ProductEntity obj)
        {
            var check = await _dbContext.ProductEntity.AnyAsync(p=>p.Name == obj.Name);
            if (check == true && obj == null)
            {
                return false;
            }
            try
            {
                await _dbContext.ProductEntity.AddAsync(obj);
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
            var obj = await _dbContext.ProductEntity.FindAsync(id);
            if (obj == null)
            {
                return false;
            }
            try
            {
                obj.IsDeleted = true;
                _dbContext.ProductEntity.Update(obj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<ProductEntity>> GetList()
        {
           return await _dbContext.ProductEntity.Where(a=>!a.IsDeleted).ToListAsync();
        }

        public async Task<ProductEntity> GetById(int id)
        {
          return await _dbContext.ProductEntity.FindAsync(id);
        }

        public async Task<bool> Update(ProductEntity obj)
        {
            var product = await _dbContext.ProductEntity.FindAsync(obj.Id);
            if (product == null)
            {
                return false;
            }
            try
            {
                product.Name = obj.Name;
                product.ManufacturerEntityId = obj.ManufacturerEntityId;
                product.ProductTypeEntityId = obj.ProductTypeEntityId;
                _dbContext.ProductEntity.Update(product);
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
