using Microsoft.EntityFrameworkCore;

using PTS.Core.Repositories;
using PTS.Core.Dto;
using PTS.Domain.Entities;
using Abp.Application.Services.Dto;
using PTS.Base.Application.Dto;
using PTS.Data;
using PTS.Domain.Repositories;

namespace PTS.Infrastructure.Repositories
{
    public class ManagePostRepository : IManagePostRepository
    {
        private readonly ApplicationDbContext _context;

        public ManagePostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResultDto<ManagePostDto>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _context.ManagePostEntity.Where(x => !x.IsDeleted);

            var totalCount = await query.CountAsync();

            var obj = await query.Skip(request.SkipCount)
                                    .Take(request.MaxResultCount)
                                    .ToListAsync();

            var objDto = obj.Select(managePost => new ManagePostDto
            {
                Id = managePost.Id,
                Code = managePost.Code,
               Description = managePost.Description,
               LinkImage = managePost.LinkImage
            }).ToList();

            return new PagedResultDto<ManagePostDto>(totalCount, objDto);
        }
        public async Task<bool> Create(ManagePostEntity obj)
        {
            var checkMa = await _context.ManagePostEntity.AnyAsync(x => x.Code == obj.Code);
            if (obj == null || checkMa)
            {
                return false;
            }
            try
            {
                await _context.ManagePostEntity.AddAsync(obj);
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
            var ManagePost = await _context.ManagePostEntity.FindAsync(id);
            if (ManagePost == null)
            {
                return false;
            }
            try
            {
                ManagePost.IsDeleted = true;
                _context.ManagePostEntity.Update(ManagePost);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IEnumerable<ManagePostEntity>> GetList()
        {
            return await _context.ManagePostEntity.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<ManagePostEntity> GetById(int id)
        {
            return await _context.ManagePostEntity.FindAsync(id);
        }

        public async Task<bool> Update(ManagePostEntity obj)
        {
            var managePost = await _context.ManagePostEntity.FindAsync(obj.Id);
            if (managePost == null)
            {
                return false;
            }
            try
            {
                managePost.Code = obj.Code;
                managePost.Description = obj.Description;
                managePost.LinkImage = obj.LinkImage;
                managePost.Status = obj.Status;
                _context.ManagePostEntity.Update(managePost);
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
