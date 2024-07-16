using AutoMapper;
using AutoMapper.QueryableExtensions;
using MTS.Application.Extensions;
using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.UserLogs.Queries
{
	public record UserLogGetPageQuery : IRequest<PaginatedResult<UserLogGetPageDto>>
	{
		[DisplayName("Từ khóa")]
		public string Keywords { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }

		public UserLogGetPageQuery() { }

		public UserLogGetPageQuery(int page, int pageSize)
		{
			Page = page;
			PageSize = pageSize;
		}
	}

	internal class UserLogGetPageQueryHandler : IRequestHandler<UserLogGetPageQuery, PaginatedResult<UserLogGetPageDto>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UserLogGetPageQueryHandler(IIdentityUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<PaginatedResult<UserLogGetPageDto>> Handle(UserLogGetPageQuery queryInput, CancellationToken cancellationToken)
		{
			var query = _unitOfWork.Repository<UserLog>().Entities;
			if (!string.IsNullOrEmpty(queryInput.Keywords)) query = query.Where(x => x.UserName.Contains(queryInput.Keywords) || x.FromIP == queryInput.Keywords);

			return await query
				   .OrderByDescending(x => x.Id)
				   .ProjectTo<UserLogGetPageDto>(_mapper.ConfigurationProvider)
				   .ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);
		}
	}
}
