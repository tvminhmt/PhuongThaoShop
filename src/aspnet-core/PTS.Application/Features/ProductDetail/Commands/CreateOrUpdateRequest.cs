using MediatR;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;

namespace PTS.Application.Features.ProductDetail.Commands
{
    public class CreateOrUpdateProductDetailQuery : IRequest<ServiceResponse>
    {
        public ProductDetailEntity? ProductDetailEntity { get; set; }
    }
    public class CreateOrUpdateProductDetailHandler : IRequestHandler<CreateOrUpdateProductDetailQuery, ServiceResponse>
    {
        private readonly IProductDetailRepository _repository;
        public CreateOrUpdateProductDetailHandler(IProductDetailRepository repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse> Handle(CreateOrUpdateProductDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.ProductDetailEntity.Id > 0)
            {
                return await _repository.Update(request.ProductDetailEntity);
            }
            else
            {
                return await _repository.Create(request.ProductDetailEntity);
            }
        }
    }
}
