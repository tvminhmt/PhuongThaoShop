using PTS.Domain.Entities;

namespace PTS.Core.Repositories
{
    public interface IImageRepository
    {
        Task<bool> Create(ImageEntity obj);
        Task<bool> Update(ImageEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<ImageEntity>> GetAllImage();
    }
}
