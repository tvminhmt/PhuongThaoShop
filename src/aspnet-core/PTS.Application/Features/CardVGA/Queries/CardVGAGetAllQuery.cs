using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.CardVGA.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.CardVGA.Queries
{
    public record CardVGAGetAllQuery : IRequest<Result<List<CardVGADto>>>
	{
    }
	internal class CardVGAGetAllQueryHandler : IRequestHandler<CardVGAGetAllQuery, Result<List<CardVGADto>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public CardVGAGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
        }
		public async Task<Result<List<CardVGADto>>> Handle(CardVGAGetAllQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<CardVGAEntity>().Entities
				.AsNoTracking()
				.Where(x => x.Status != (int)StatusEnum.Delete)
				.ProjectTo<CardVGADto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
			return await Result<List<CardVGADto>>.SuccessAsync(result);
		}
	}
}
