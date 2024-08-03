using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.Discount.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.Discount.Queries
{
    public record DiscountGetByIdQuery : IRequest<Result<DiscountDto>>
	{
		public int Id { get; set; }
	}
	internal class DiscountGetByIdQueryHandler : IRequestHandler<DiscountGetByIdQuery, Result<DiscountDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public DiscountGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<DiscountDto>> Handle(DiscountGetByIdQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<DiscountEntity>().Entities
				  .AsNoTracking()
				.Where(x => x.Id == queryInput.Id )
				.ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken);
			return await Result<DiscountDto>.SuccessAsync(result);
		}
	}
}
