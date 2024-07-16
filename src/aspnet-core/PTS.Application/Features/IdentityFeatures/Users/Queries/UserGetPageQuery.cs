using AutoMapper;
using AutoMapper.QueryableExtensions;
using MTS.Application.DTOs;
using MTS.Application.Extensions;
using MTS.Application.Features.IdentityFeatures.Roles.Queries;
using MTS.Application.Interfaces;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using MTS.Domain.Enums.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.Users.Queries
{
    public record UserGetPageQuery : BaseGetPageQuery, IRequest<PaginatedResult<UserGetPageDto>>
    {
        [DisplayName("Kích hoạt")]
        public byte IsEnabled { get; set; }

        [DisplayName("Xác thực 2 bước")]
        public byte TwoFactorEnabled { get; set; }
    }

    internal class UserGetPageQueryHandler : IRequestHandler<UserGetPageQuery, PaginatedResult<UserGetPageDto>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
		private readonly RoleManager<Role> _roleManager;
		private readonly IRoleRepo _roleRepo;
        private readonly IUserRoleRepo _userRoleRepo;
		private readonly ICurrentUserService _currentUserService;

		public UserGetPageQueryHandler(IMapper mapper, IRoleRepo roleRepo, 
            IUserRoleRepo userRoleRepo, UserManager<User> userManager,
			RoleManager<Role> roleManager,
			ICurrentUserService currentUserService)
        {
            _roleRepo = roleRepo;
            _userRoleRepo = userRoleRepo;
            _mapper = mapper;
            _userManager = userManager;
			_roleManager = roleManager;
			_currentUserService = currentUserService;
		}

        public async Task<PaginatedResult<UserGetPageDto>> Handle(UserGetPageQuery queryInput, CancellationToken cancellationToken)
        {
			var userRoles = await _currentUserService.GetRoles();

            bool isAdminRole = _currentUserService.IsAdminRole(userRoles),
                isModRole = _currentUserService.IsModRole(userRoles);

			var query = _userManager.Users;

            if(!isAdminRole && !isModRole )
            {
				query = from Users in query
						where (from UserRoles in _userRoleRepo.DbSet.AsNoTracking()
							   where
								(from Roles in _roleManager.Roles
								 where
								   userRoles.Any(x => x == Roles.Name)
								 select new
								 {
									 Roles.Id
								 }).Any(x => x.Id == UserRoles.RoleId)
							   select new
							   {
								   UserRoles.UserId
							   }).Any(x => x.UserId == Users.Id)
						select Users;
			}
            else
            {
				if(isModRole)
				{
					query = from Users in query
							where !(from UserRoles in _userRoleRepo.DbSet.AsNoTracking()
								   where
									   (from Roles in _roleManager.Roles
										where
											(Roles.Name == "Admin" || Roles.Name == "AdminVOH")
										select new
										{
											Roles.Id
										}).Any(x => x.Id == UserRoles.RoleId)
								   select new
								   {
									   UserRoles.UserId
								   }).Any(x => x.UserId == Users.Id)
							select Users;
				}
			}

            if (!string.IsNullOrEmpty(queryInput.Keywords))
            {
                query = query.Where(x => x.UserName.Contains(queryInput.Keywords) || x.FullName.Contains(queryInput.Keywords) || x.Email.Contains(queryInput.Keywords) || x.PhoneNumber.Contains(queryInput.Keywords));
            }

            if (queryInput.IsEnabled == (int)UserIsEnabledEnum.Active)
            {
                query = query.Where(x => x.IsEnabled == true);
            }
            else if (queryInput.IsEnabled == (int)UserIsEnabledEnum.Deactive)
            {
                query = query.Where(x => x.IsEnabled == false);
            }

            if (queryInput.TwoFactorEnabled == (int)UserTwoFactorEnabledEnum.Active)
            {
                query = query.Where(x => x.TwoFactorEnabled == true);
            }
            else if (queryInput.TwoFactorEnabled == (int)UserTwoFactorEnabledEnum.Deactive)
            {
                query = query.Where(x => x.TwoFactorEnabled == false);
            }

            var result = await query
                   .OrderByDescending(x => x.Id)
                   .ProjectTo<UserGetPageDto>(_mapper.ConfigurationProvider)
                   .ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);

			var userIdsList = result.Data.Select(x => x.Id).ToList();
            var rolesList = await _roleRepo.GetAllAsync();
            var userRolesList = await _userRoleRepo.GetByUserIdsListAsync(userIdsList);

            foreach (var item in result.Data)
            {
                var rolesByUser = (from r in rolesList
                                   join ur in userRolesList on r.Id equals ur.RoleId
                                   where ur.UserId == item.Id
                                   select r).ToList();

                item.Roles = _mapper.Map<List<RoleDto>>(rolesByUser);
            }

            return result;
        }
    }
}
