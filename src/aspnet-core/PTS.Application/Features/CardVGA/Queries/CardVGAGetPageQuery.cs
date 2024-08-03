using AutoMapper;
using AutoMapper.QueryableExtensions;
using PTS.Application.DTOs;
using PTS.Application.Extensions;
using MediatR;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using PTS.Application.Features.CardVGA.DTOs;
using PTS.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace PTS.Application.Features.CardVGA.Queries
{
    public record CardVGAGetPageQuery : BaseGetPageQuery, IRequest<PaginatedResult<CardVGADto>>
    {
     
    }
    internal class CardVGAGetPagesQueryHandler : IRequestHandler<CardVGAGetPageQuery, PaginatedResult<CardVGADto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CardVGAGetPagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<CardVGADto>> Handle(CardVGAGetPageQuery queryInput, CancellationToken cancellationToken)
        {
            var query = from listObj in _unitOfWork.Repository<CardVGAEntity>().Entities.Where(x =>x.Status != (int)StatusEnum.Delete).AsNoTracking() select listObj;
            if (!string.IsNullOrEmpty(queryInput.Keywords))
            {
                query = query.Where(x => x.Ma.Contains(queryInput.Keywords) || x.ThongSo.Contains(queryInput.Keywords));
            }
            query = query.OrderBy(x => x.CrDateTime);
            var pQuery = query.ProjectTo<CardVGADto>(_mapper.ConfigurationProvider);
            var result = await pQuery.ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);
            return result;
        }
    }
}
