using MTS.Application.Common.Mappings;
using MTS.Application.DTOs;
using MTS.Application.Features.IdentityFeatures.Users.Queries;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using IC.Shared.Helpers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.Users.Commands
{
    public record UserEditCommand : BaseEditCommand, IRequest<Result<int>>, IMapFrom<UserGetByIdDto>
    {
        [DisplayName("Tên truy cập")]
        public string UserName { get; set; }

        public string Email { get; set; }

        [DisplayName("Số điện thoại")]
        public string PhoneNumber { get; set; }

        [DisplayName("Xác thực 2 bước")]
        public bool TwoFactorEnabled { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }

        [DisplayName("Họ và tên")]
        public string FullName { get; set; }

        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [DisplayName("Ngày sinh")]
        public string BirthDay { get; set; }

        public int? OrganId { get; set; }
        public int? DefaultActionId { get; set; }

        [DisplayName("Ghi chú")]
        public string Notes { get; set; }
        public string AvatarPath { get; set; }

        [DisplayName("Kích hoạt")]
        public bool IsEnabled { get; set; }

        [DisplayName("Gắn với tài khoản News")]
        public int? MapNewsUserId { get; set; }

        [DisplayName("Gắn với tài khoản PodCast")]
        public string MapPodCastUserId { get; set; }

        [DisplayName("Gắn với tài khoản VohData")]
        public string MapVohDataUserId { get; set; }
        public List<int> SelectedRoles { get; set; }
        public List<int> RolesByUserList { get; set; }

        [DisplayName("Thêm tiếp dữ liệu khác")]
        public bool AddMoreData { get; set; }
    }

    internal class UserEditCommandHandler : IRequestHandler<UserEditCommand, Result<int>>
    {
        private readonly UserManager<User> _userManager;
        private readonly IRoleRepo _roleRepo;

        public UserEditCommandHandler(IRoleRepo roleRepo, UserManager<User> userManager)
        {
            _userManager = userManager;
            _roleRepo = roleRepo;
        }

        public async Task<Result<int>> Handle(UserEditCommand command, CancellationToken cancellationToken)
        {
            var entity = await _userManager.FindByIdAsync(command.Id.ToString());

            if (entity == null)
            {
                return await Result<int>.FailureAsync($"Tài khoản Id <b>{command.Id}</b> không tồn tại.");
            }

            entity.FullName = command.FullName;
            entity.Email = command.Email;
            entity.PhoneNumber = command.PhoneNumber;
            entity.Address = command.Address;
            entity.Notes = command.Notes;
            entity.IsEnabled = command.IsEnabled;
            entity.TwoFactorEnabled = command.TwoFactorEnabled;
            entity.NormalizedEmail = entity.Email;
            entity.AvatarPath = command.AvatarPath;

            if (!string.IsNullOrWhiteSpace(command.BirthDay))
            {
                DateTime birthDayDb = command.BirthDay.ToDateTime();

                if (birthDayDb != DateTime.MinValue)
                {
                    entity.BirthDay = birthDayDb;
                }
            }

            IdentityResult identityResult = await _userManager.UpdateAsync(entity);

            if (!identityResult.Succeeded)
            {
                return await Result<int>.FailureAsync(string.Join(", ", identityResult.Errors.Select(x => x.Description)));
            }

            //Remove Roles
            var userRoles = await _userManager.GetRolesAsync(entity);

            await _userManager.RemoveFromRolesAsync(entity, userRoles);

            //Add Roles
            Role roleAdd = null;
            var rolesGetAllList = await _roleRepo.GetAllAsync();

            if (command.SelectedRoles != null)
            {
                foreach (var roleId in command.SelectedRoles)
                {
                    roleAdd = rolesGetAllList.FirstOrDefault(x => x.Id == roleId);

                    if (roleAdd != null)
                    {
                        await _userManager.AddToRoleAsync(entity, roleAdd.Name);
                    }
                }
            }

			return await Result<int>.SuccessAsync(entity.Id, $"Cập nhật tài khoản <b>{entity.UserName}</b> thành công.");
        }
    }
}
