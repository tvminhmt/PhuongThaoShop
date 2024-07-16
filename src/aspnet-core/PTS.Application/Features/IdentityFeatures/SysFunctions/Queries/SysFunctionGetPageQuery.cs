using AutoMapper;
using AutoMapper.QueryableExtensions;
using MTS.Application.Extensions;
using MTS.Application.Features.IdentityFeatures.Roles.Queries;
using MTS.Application.Features.IdentityFeatures.SysFunctions.Queries;
using MTS.Application.Interfaces.Repositories;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.UserLogs.Queries
{
    public record SysFunctionGetPageQuery : IRequest<PaginatedResult<SysFunctionGetPageDto>>
	{
		[DisplayName("Từ khóa")]
		public string Keywords { get; set; }

		[DisplayName("Chức năng cha")]
		public int ParentItemId { get; set; }

		[DisplayName("Hiện chức năng cấp 1")]
		public bool ShowFirstLevelOnly { get; set; }
		public int Page { get; set; }
		public int PageSize { get; set; }

		public byte IsShow { get; set; }

		public byte IsEnable { get; set; }

		public SysFunctionGetPageQuery() { }

		public SysFunctionGetPageQuery(int page, int pageSize)
		{
			Page = page;
			PageSize = pageSize;
		}
	}

	internal class SysFunctionGetPageQueryHandler : IRequestHandler<SysFunctionGetPageQuery, PaginatedResult<SysFunctionGetPageDto>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly IRoleRepo _roleRepo;
		private readonly IMapper _mapper;

		public SysFunctionGetPageQueryHandler(IIdentityUnitOfWork unitOfWork, IRoleRepo roleRepo, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_roleRepo = roleRepo;
			_mapper = mapper;
		}

		public async Task<PaginatedResult<SysFunctionGetPageDto>> Handle(SysFunctionGetPageQuery queryInput, CancellationToken cancellationToken)
		{
			var query = _unitOfWork.Repository<SysFunction>().Entities.AsNoTracking();

			if (!string.IsNullOrWhiteSpace(queryInput.Keywords))
			{
				query = query.Where(x => x.FunctionName.Contains(queryInput.Keywords));
			}

			if (queryInput.ShowFirstLevelOnly)
			{
				query = query.Where(x => x.ParentItemId == 0);
			}
				
			if (queryInput.ParentItemId > 0)
			{
				query = query.Where(x => x.Id == queryInput.ParentItemId || x.ParentItemId == queryInput.ParentItemId);
			}
				
			var result = await query
				   .OrderBy(x => x.TreeOrder)
				   .ProjectTo<SysFunctionGetPageDto>(_mapper.ConfigurationProvider)
				   .ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);

			var roles = await _roleRepo.GetAllAsync();
			var itemRoles = await _unitOfWork.Repository<SysFunctionRole>().GetAllAsync();

			foreach (var item in result.Data)
			{
				var menuItemRole = from r in roles
								   join ur in itemRoles on r.Id equals ur.RoleId
								   where ur.SysFunctionId == item.Id
								   select r;

				item.Roles = _mapper.Map<List<RoleDto>>(menuItemRole.ToList());
			}

			return result;
		}
	}
}
