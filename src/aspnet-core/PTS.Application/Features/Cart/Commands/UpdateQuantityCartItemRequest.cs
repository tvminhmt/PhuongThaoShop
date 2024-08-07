using MediatR;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Services;

namespace PTS.Application.Features.Cart.Commands
{
    public class UpdateQuantityCartItemQuery : IRequest<ServiceResponse>
    {
        public int Quantity { get; set; }
        public int IdCartDetail { get; set; }
    }
    public class UpdateQuantityCartItemHandler : IRequestHandler<UpdateQuantityCartItemQuery, ServiceResponse>
    {
        private readonly ICartDetailRepository _repo;
        public UpdateQuantityCartItemHandler(ICartDetailRepository repo)
        {
            _repo = repo;
        }
        public async Task<ServiceResponse> Handle(UpdateQuantityCartItemQuery request, CancellationToken cancellationToken)
        {
            CartDetailEntity cartDetail = new CartDetailEntity();
            cartDetail.Id = request.IdCartDetail;
            cartDetail.Quantity = request.Quantity;
            return await _repo.UpdateQuantity(cartDetail);
        }
    }
}
