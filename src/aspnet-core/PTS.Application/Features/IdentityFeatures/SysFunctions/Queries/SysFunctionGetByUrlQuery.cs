using AutoMapper;
using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Queries
{
	public record SysFunctionGetByUrlQuery : IRequest<SysFunctionGetByUrlDto>
	{
		public int UserId { get; set; }
		public string Url { get; set; }
	}

	internal class SysFunctionGetByUrlQueryHandler : IRequestHandler<SysFunctionGetByUrlQuery, SysFunctionGetByUrlDto>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public SysFunctionGetByUrlQueryHandler(IIdentityUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<SysFunctionGetByUrlDto> Handle(SysFunctionGetByUrlQuery query, CancellationToken cancellationToken)
		{
			SysFunctionGetByUrlDto sysFunctionGetByUrlDto = null;

			if (!string.IsNullOrWhiteSpace(query.Url))
			{
				var entity = await _unitOfWork.Repository<SysFunction>().Entities.AsNoTracking().FirstOrDefaultAsync( x => x.Url.ToLower().Trim().Equals(query.Url.ToLower().Trim()));

				if (entity != null)
				{
					sysFunctionGetByUrlDto = _mapper.Map<SysFunctionGetByUrlDto>(entity);

					sysFunctionGetByUrlDto.FunctionByUser = await _unitOfWork.Repository<SysFunctionUser>().Entities
																.Where(x => x.UserId == query.UserId && x.SysFunctionId == entity.Id)
																.Select(x => x.SysFunctionId)
																.FirstOrDefaultAsync();
				}
			}

			return sysFunctionGetByUrlDto;
		}
	}
}
