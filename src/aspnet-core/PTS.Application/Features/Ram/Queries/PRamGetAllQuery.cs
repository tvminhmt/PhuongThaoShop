using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.Ram.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;
namespace PTS.Application.Features.Ram.Queries
{
    public record PRamGetAllQuery : IRequest<Result<List<RamDto>>>
	{
		
	}
	internal class PRamGetAllQueryHandler : IRequestHandler<PRamGetAllQuery, Result<List<RamDto>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public PRamGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<List<RamDto>>> Handle(PRamGetAllQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<RamEntity>().Entities
				.AsNoTracking()
				.Where(x => x.Status == (int)StatusEnum.Active )
				.ProjectTo<RamDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
			return await Result<List<RamDto>>.SuccessAsync(result);
		}
	}
}
