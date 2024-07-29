using PTS.Application.Dto;
using PTS.Domain.Entities;

namespace PTS.Application.Interfaces.Repositories
{
    public interface ICartRepository
    {
        Task<bool> Create(CartEntity obj);
        Task<bool> Update(CartEntity obj);
        Task<bool> Delete(int id);
        Task<IEnumerable<CartEntity>> GetAll();
        Task<CartEntity> GetCartById(int id);
        Task<IEnumerable<CartItemDto>> GetCartItem(string UserName);
        //  Task<IEnumerable<CartItemDto>> GetAllCarts();
    }
}
