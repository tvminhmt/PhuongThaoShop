using AutoMapper;
using MTS.Application.Interfaces;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.Roles.Queries
{
	public record RoleGetAllQuery : IRequest<Result<List<RoleGetAllDto>>>
	{
		[DisplayName("Từ khóa")]
		public string Keywords { get; set; }
	}

	internal class RoleGetAllQueryHandler : IRequestHandler<RoleGetAllQuery, Result<List<RoleGetAllDto>>>
	{
		private readonly IMapper _mapper;
		private readonly RoleManager<Role> _roleManager;
		private readonly ICurrentUserService _currentUserService;

		public RoleGetAllQueryHandler(IMapper mapper, RoleManager<Role> roleManager, ICurrentUserService currentUserService)
		{
			_mapper = mapper;
			_roleManager = roleManager;
			_currentUserService = currentUserService;
		}

		public async Task<Result<List<RoleGetAllDto>>> Handle(RoleGetAllQuery queryInput, CancellationToken cancellationToken)
		{
			var roles = await _currentUserService.GetRoles();

			bool isSuperAdminRole = _currentUserService.IsSuperAdminRole(roles);

			var query = _roleManager.Roles.AsNoTracking();

			if (!string.IsNullOrWhiteSpace(queryInput.Keywords))
			{
				query = query.Where(x => x.Name.Contains(queryInput.Keywords) || x.Description.Contains(queryInput.Keywords));
			}
			
			if(!isSuperAdminRole)
			{
				query = query.Where(x => x.Name != "Admin");
			}

			List<Role> rolesList = await query.ToListAsync(cancellationToken);

			List<RoleGetAllDto> rolesDtoList = _mapper.Map<List<RoleGetAllDto>>(rolesList);

			return await Result<List<RoleGetAllDto>>.SuccessAsync(rolesDtoList);
		}
	}
}
