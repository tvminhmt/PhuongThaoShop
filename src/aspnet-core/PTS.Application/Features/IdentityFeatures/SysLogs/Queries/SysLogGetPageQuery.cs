using AutoMapper;
using AutoMapper.QueryableExtensions;
using MTS.Application.Extensions;
using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.SysLogs.Queries
{
	public record SysLogGetPageQuery : IRequest<PaginatedResult<SysLogGetPageDto>>
	{
		[DisplayName("Từ khóa")]
		public string Keywords { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }

		public SysLogGetPageQuery() { }

		public SysLogGetPageQuery(int page, int pageSize)
		{
			Page = page;
			PageSize = pageSize;
		}
	}

	internal class SysLogGetPageQueryHandler : IRequestHandler<SysLogGetPageQuery, PaginatedResult<SysLogGetPageDto>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public SysLogGetPageQueryHandler(IIdentityUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PaginatedResult<SysLogGetPageDto>> Handle(SysLogGetPageQuery queryInput, CancellationToken cancellationToken)
		{
			var query = _unitOfWork.Repository<SysLog>().Entities;

			if (!string.IsNullOrEmpty(queryInput.Keywords))
			{
				query = query.Where(x => x.Application.Contains(queryInput.Keywords) || x.Level.Contains(queryInput.Keywords) || x.Message.Contains(queryInput.Keywords));
			}

			var result = await query
				   .OrderByDescending(x => x.TimeStamp)
				   .ProjectTo<SysLogGetPageDto>(_mapper.ConfigurationProvider)
				   .ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);

			return result;
		}
	}
}
