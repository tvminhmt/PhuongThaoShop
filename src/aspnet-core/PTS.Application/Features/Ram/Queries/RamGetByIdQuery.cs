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
    public record RamGetByIdQuery : IRequest<Result<RamDto>>
	{
		public int Id { get; set; }
	}
	internal class RamGetByIdQueryHandler : IRequestHandler<RamGetByIdQuery, Result<RamDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public RamGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<RamDto>> Handle(RamGetByIdQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<RamEntity>().Entities
				  .AsNoTracking()
				.Where(x => x.Id == queryInput.Id && x.Status != (int)StatusEnum.Delete)
				.ProjectTo<RamDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken);
			return await Result<RamDto>.SuccessAsync(result);
		}
	}
}
