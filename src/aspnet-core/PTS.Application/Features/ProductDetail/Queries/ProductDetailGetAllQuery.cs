using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.ProductDetail.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.ProductDetail.Queries
{
    public record ProductDetailGetAllQuery : IRequest<Result<List<ProductDetailDto>>>
    {
    }
    internal class ProductDetailGetAllQueryHandler : IRequestHandler<ProductDetailGetAllQuery, Result<List<ProductDetailDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductDetailGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<ProductDetailDto>>> Handle(ProductDetailGetAllQuery queryInput, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository<ProductDetailEntity>().Entities
                .AsNoTracking()
                .Where(x => x.Status != (int)StatusEnum.Delete)
                .ProjectTo<ProductDetailDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return await Result<List<ProductDetailDto>>.SuccessAsync(result);
        }
    }
}
