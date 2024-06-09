using PTS.Core.Dto;
using PTS.Domain.Entities;

namespace PTS.Core.Repositories
{
    public interface IGiamGiaRepository
    {
        Task<bool> Create(GiamGiaEntity obj);
        Task<bool> Update(GiamGiaEntity obj);
        Task<bool> Delete(int id);
        Task<List<GiamGiaEntity>> GetAllGiamGias();
        Task<GiamGiaEntity> GetGiamGiaByPromotionType(string promotionType);


    }
}
