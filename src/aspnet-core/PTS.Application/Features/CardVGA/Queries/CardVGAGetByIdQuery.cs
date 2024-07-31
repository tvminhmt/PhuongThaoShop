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
    public record CardVGAGetByIdQuery : IRequest<Result<CardVGADto>>
	{
		public int Id { get; set; }
	}
	internal class CardVGAGetByIdQueryHandler : IRequestHandler<CardVGAGetByIdQuery, Result<CardVGADto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public CardVGAGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<CardVGADto>> Handle(CardVGAGetByIdQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<CardVGAEntity>().Entities
				  .AsNoTracking()
				.Where(x => x.Id == queryInput.Id && x.Status != (int)StatusEnum.Delete)
				.ProjectTo<CardVGADto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken);
			return await Result<CardVGADto>.SuccessAsync(result);
		}
	}
}
