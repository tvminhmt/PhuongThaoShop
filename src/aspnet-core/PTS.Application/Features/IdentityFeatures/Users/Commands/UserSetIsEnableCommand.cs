using MTS.Application.Common.Mappings;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MTS.Application.Features.IdentityFeatures.Users.Commands
{
	public class UserSetIsEnableCommand : IRequest<Result<int>>, IMapFrom<User>
	{
		public int Id { get; set; }
		public bool IsEnable { get; set; }
	}

	internal class UserSetIsEnableCommandHandler : IRequestHandler<UserSetIsEnableCommand, Result<int>>
	{
		private readonly UserManager<User> _userManager;

		public UserSetIsEnableCommandHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<Result<int>> Handle(UserSetIsEnableCommand command, CancellationToken cancellationToken)
		{
			var entity = await _userManager.FindByIdAsync(command.Id.ToString());

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Tài khoản Id <b>{command.Id}</b> không tồn tại.");
			}

			entity.IsEnabled = command.IsEnable;

			IdentityResult identityResult = await _userManager.UpdateAsync(entity);

			if (!identityResult.Succeeded)
			{
				return await Result<int>.FailureAsync(string.Join(", ", identityResult.Errors.Select(x => x.Description)));
			}

			return await Result<int>.SuccessAsync(entity.Id, $"Cập nhật trạng thái tài khoản <b>{entity.UserName}</b> thành công.");
		}
	}
}
