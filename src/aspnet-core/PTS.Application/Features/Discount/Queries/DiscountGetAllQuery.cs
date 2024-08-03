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
    public record DiscountGetAllQuery : IRequest<Result<List<DiscountDto>>>
	{
    }
	internal class DiscountGetAllQueryHandler : IRequestHandler<DiscountGetAllQuery, Result<List<DiscountDto>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public DiscountGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
        }
		public async Task<Result<List<DiscountDto>>> Handle(DiscountGetAllQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<DiscountEntity>().Entities
				.AsNoTracking()
				
				.ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
			return await Result<List<DiscountDto>>.SuccessAsync(result);
		}
	}
}
