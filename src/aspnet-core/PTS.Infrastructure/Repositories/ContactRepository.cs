using Microsoft.EntityFrameworkCore;

using PTS.Core.Repositories;
using PTS.Domain.Entities;
using PTS.Data;
using PTS.Core.Dto;
using Abp.Application.Services.Dto;
using PTS.Base.Application.Dto;

namespace PTS.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }
        public async Task<PagedResultDto<ContactDto>> GetPagedAsync(PagedRequestDto request)
        {
            var query = _dbContext.ContactEntity.Where(x => !x.IsDeleted);

            var totalCount = await query.CountAsync();

            var obj = await query.Skip(request.SkipCount)
                                    .Take(request.MaxResultCount)
                                    .ToListAsync();

            var objDto = obj.Select(contact => new ContactDto
            {
                Id = contact.Id,
               Email = contact.Email,
               Name = contact.Name,
               Message = contact.Message,
               CodeManagePost = contact.CodeManagePost,
              Website = contact.Website
        }).ToList();

            return new PagedResultDto<ContactDto>(totalCount, objDto);
        }
        public async Task<ContactEntity> GetById(int id)
        {
            return await _dbContext.ContactEntity.FindAsync(id);
        }
        public async Task<ServiceResponse> Create(ContactEntity contact)
        {
            var checkMa = await _dbContext.ContactEntity.AnyAsync(x => x.CodeManagePost == contact.CodeManagePost);
            if (contact == null || checkMa == true)
            {
                return new ServiceResponse(false, "Thêm thất bại");
            }
            try
            {
                await _dbContext.ContactEntity.AddAsync(contact);
                await _dbContext.SaveChangesAsync();
                return new ServiceResponse(true, "Thêm thành công");
            }
            catch (Exception)
            {
                return new ServiceResponse(false, "Thêm thất bại");
            }
        }

        public async Task<ServiceResponse> Delete(int Id)
        {

            var obj = await _dbContext.ContactEntity.FindAsync(Id);
            if (obj == null)
            {
                return new ServiceResponse(false, "Xóa thất bại");
            }
            try
            {
                obj.IsDeleted = true;
                _dbContext.ContactEntity.Update(obj);
                await _dbContext.SaveChangesAsync();
                return new ServiceResponse(true, "Xóa thành công");
            }
            catch (Exception)
            {
                return new ServiceResponse(false, "Xóa thất bại");
            }
        }

        public async Task<IEnumerable<ContactEntity>> GetList()
        {
            return await _dbContext.ContactEntity.Where(a=>!a.IsDeleted).ToListAsync();
        }

        public async Task<ServiceResponse> Update(ContactEntity contact)
        {
            var obj = await _dbContext.ContactEntity.FindAsync(contact.Id);
            if (obj == null)
            {
                return new ServiceResponse(false, "Cập nhật thất bại");
            }
            try
            {
                obj.Email = contact.Email;
                obj.Name = contact.Name;
                obj.Message = contact.Message;
                obj.CreationTime = contact.CreationTime;
                obj.CodeManagePost = contact.CodeManagePost;
                obj.Status = contact.Status;
                obj.Website = contact.Website;
                _dbContext.ContactEntity.Update(obj);
                await _dbContext.SaveChangesAsync();
                return new ServiceResponse(true, "Cập nhật thành công");
            }
            catch (Exception)
            {
                return new ServiceResponse(false, "Cập nhật thất bại");
            }
        }
    }
}
