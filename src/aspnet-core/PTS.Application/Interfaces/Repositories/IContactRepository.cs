using Abp.Application.Services.Dto;
using PTS.Shared.Dto;
using PTS.Application.Dto;
using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
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
