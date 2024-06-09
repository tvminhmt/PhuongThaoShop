using Abp.Application.Services.Dto;
using PTS.Base.Application.Dto;
using PTS.Core.Dto;
using PTS.Domain.Entities;

namespace PTS.Core.Repositories
{
    public interface IContactRepository
    {
        Task<ServiceResponse> Create( ContactEntity obj );
        Task<ServiceResponse> Update(ContactEntity obj);
        Task<ServiceResponse> Delete(int id);
        Task<IEnumerable<ContactEntity>> GetList();
        Task<PagedResultDto<ContactDto>> GetPagedAsync(PagedRequestDto request);
        Task<ContactEntity> GetById(int id);
    }
}
