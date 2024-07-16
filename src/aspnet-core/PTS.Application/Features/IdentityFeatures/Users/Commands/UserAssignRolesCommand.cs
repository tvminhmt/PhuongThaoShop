using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MTS.Application.Features.IdentityFeatures.Users.Commands
{
    public record UserAssignRolesCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public List<int> SelectedRoles { get; set; }
    }

    internal class UserAssignRolesCommandHandler : IRequestHandler<UserAssignRolesCommand, Result<int>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IRoleRepo _roleRepo;

		public UserAssignRolesCommandHandler(UserManager<User> userManager, IRoleRepo roleRepo)
        {
            _userManager = userManager;
            _roleRepo = roleRepo;
        }

        public async Task<Result<int>> Handle(UserAssignRolesCommand request, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByIdAsync(request.Id.ToString());

            if (entity == null)
            {
                return await Result<int>.FailureAsync($"Tài khoản Id <b>${request.Id}</b> không tồn tại.");
            }

            //Xóa roles hiện tại
            var userRoles = await _userManager.GetRolesAsync(entity);

            IdentityResult removeUserRolesResult = await _userManager.RemoveFromRolesAsync(entity, userRoles);

            if (!removeUserRolesResult.Succeeded)
            {
                return await Result<int>.FailureAsync(string.Join(", ", removeUserRolesResult.Errors.Select(x => x.Description)));
            }

            //Add roles đã chọn
            var rolesGetListAll = await _roleRepo.GetAllAsync();

            if (request.SelectedRoles != null)
            {
                foreach (var roleId in request.SelectedRoles)
                {
                    await _userManager.AddToRoleAsync(entity, rolesGetListAll.Find(x => x.Id == roleId).Name);
                }
            }

            return await Result<int>.SuccessAsync(entity.Id, $"Gán vai trò cho tài khoản <b>{entity.UserName}</b> thành công");
        }
    }
}
