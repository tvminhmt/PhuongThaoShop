using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.Image.Queries
{
	public record ImageGetByIdQuery : IRequest<Result<ImageDto>>
	{
		public int Id { get; set; }
	}
	internal class ImageGetByIdQueryHandler : IRequestHandler<ImageGetByIdQuery, Result<ImageDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ImageGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<ImageDto>> Handle(ImageGetByIdQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<ImageEntity>().Entities
				  .AsNoTracking()
				.Where(x => x.Id == queryInput.Id && x.Status != (int)StatusEnum.Delete)
				.ProjectTo<ImageDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken);
			return await Result<ImageDto>.SuccessAsync(result);
		}
	}
}
