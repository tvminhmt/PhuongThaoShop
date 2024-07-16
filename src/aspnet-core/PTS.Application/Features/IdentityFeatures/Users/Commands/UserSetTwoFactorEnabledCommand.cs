using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MTS.Application.Features.IdentityFeatures.Users.Commands
{
	public record UserSetTwoFactorEnabledCommand : IRequest<Result<int>>
	{
		public int Id { get; set; }
		public bool TwoFactorEnabled { get; set; }
	}

	internal class UserSetTwoFactorEnabledCommandHandler : IRequestHandler<UserSetTwoFactorEnabledCommand, Result<int>>
	{
		private readonly UserManager<User> _userManager;

		public UserSetTwoFactorEnabledCommandHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<Result<int>> Handle(UserSetTwoFactorEnabledCommand command, CancellationToken cancellationToken)
		{
			var entity = await _userManager.FindByIdAsync(command.Id.ToString());

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Tài khoản Id <b>{command.Id}</b> không tồn tại.");
			}

			entity.TwoFactorEnabled = command.TwoFactorEnabled;

			IdentityResult identityResult = await _userManager.UpdateAsync(entity);

			if (!identityResult.Succeeded)
			{
				return await Result<int>.FailureAsync(string.Join(", ", identityResult.Errors.Select(x => x.Description)));
			}

			return await Result<int>.SuccessAsync(entity.Id, $"Cập nhật trạng thái xác thực 2 bước tài khoản <b>{entity.UserName}</b> thành công.");
		}
	}
}
