using AutoMapper;
using MTS.Application.Common.Mappings;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MTS.Application.Features.IdentityFeatures.Users.Queries
{
    public record UserGetByIdQuery : IRequest<Result<UserGetByIdDto>>, IMapFrom<UserGetByIdDto>
	{
		public int Id { get; set; }
	}

	internal class UserGetByIdQueryHandler : IRequestHandler<UserGetByIdQuery, Result<UserGetByIdDto>>
	{
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly IUserRoleRepo _userRoleRepo;
		public UserGetByIdQueryHandler(IMapper mapper, IUserRoleRepo userRoleRepo, UserManager<User> userManager)
		{
			_userRoleRepo = userRoleRepo;
			_mapper = mapper;
			_userManager = userManager;
		}

		public async Task<Result<UserGetByIdDto>> Handle(UserGetByIdQuery query, CancellationToken cancellationToken)
		{
			var userById = await _userManager.FindByIdAsync(query.Id.ToString());

			var userGetByIdDto = _mapper.Map<UserGetByIdDto>(userById);

			if (userById != null && userById.Id > 0)
			{
				var userRoles = await _userRoleRepo.GetByUserIdAsync(query.Id);

				userGetByIdDto.RolesByUserList = userRoles.Select(x => x.RoleId).ToList();
			}

			return await Result<UserGetByIdDto>.SuccessAsync(userGetByIdDto);
		}
	}
}
