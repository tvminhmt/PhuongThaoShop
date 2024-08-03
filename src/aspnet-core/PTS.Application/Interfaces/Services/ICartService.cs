using PTS.Application.Dto;

namespace PTS.Core.Services
{
    public interface ICartService
    {
        Task<ServiceResponse> AddCart(string UserName, int  idProductDetail, int quantity);// Phương thức để người dùng tạo giỏ hàng
        Task<ResponseDto> GetListCarts();// Cho admin quản lý
        Task<ResponseDto> GetCartById(int id);// Cho admin quản lý
        Task<ResponseDto> GetCartByUser(string UserName);// Hiển thị giỏ hàng cho người dùng
    }
}
