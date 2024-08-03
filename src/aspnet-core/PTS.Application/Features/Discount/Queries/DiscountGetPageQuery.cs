using AutoMapper;
using AutoMapper.QueryableExtensions;
using PTS.Application.DTOs;
using PTS.Application.Extensions;
using MediatR;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using PTS.Application.Features.Discount.DTOs;
using PTS.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace PTS.Application.Features.Discount.Queries
{
    public record DiscountGetPageQuery : BaseGetPageQuery, IRequest<PaginatedResult<DiscountDto>>
    {
     
    }
    internal class DiscountGetPagesQueryHandler : IRequestHandler<DiscountGetPageQuery, PaginatedResult<DiscountDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DiscountGetPagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<DiscountDto>> Handle(DiscountGetPageQuery queryInput, CancellationToken cancellationToken)
        {
            var query = from listObj in _unitOfWork.Repository<DiscountEntity>().Entities.AsNoTracking() select listObj;
            //if (!string.IsNullOrEmpty(queryInput.Keywords))
            //{
            //    query = query.Where(x => x.Ma.Contains(queryInput.Keywords) || x.ThongSo.Contains(queryInput.Keywords));
            //}
            //query = query.OrderBy(x => x.CrDateTime);
            var pQuery = query.ProjectTo<DiscountDto>(_mapper.ConfigurationProvider);
            var result = await pQuery.ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);
            return result;
        }
    }
}
