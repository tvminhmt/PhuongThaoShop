using MediatR;
using PTS.Application.Dto;
using PTS.Core.Services;

namespace PTS.Application.Features.Cart.Queries
{
    public class GetCartByUserQuery : IRequest<ResponseDto>
    {
        public string? UserName { get; set; }
    }
    public class GetCartByUserHandler : IRequestHandler<GetCartByUserQuery, ResponseDto>
    {
        private readonly ICartService _cartService;
        public GetCartByUserHandler(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<ResponseDto> Handle(GetCartByUserQuery request, CancellationToken cancellationToken)
        {
            return await _cartService.GetCartByUser(request.UserName);
        }
    }
}
