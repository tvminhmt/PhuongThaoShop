using PTS.Core.Dto;
using PTS.Domain.Entities;

namespace PTS.Core.Repositories
{
    public interface ICartRepository
    {
        Task<bool> Create(CartEntity obj);
        Task<bool> Update(CartEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<CartEntity>> GetAll();
        Task<CartEntity> GetCartById(int id);
        Task<IEnumerable<CartItemDto>> GetCartItem(string username);
        //  Task<IEnumerable<CartItemDto>> GetAllCarts();
    }
}
