using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace MTS.Application.Features.IdentityFeatures.Users.Commands
{
	public record UserDeleteCommand : IRequest<Result<int>>
	{
		public int Id { get; set; }
	}

	internal class UserDeleteCommandHandler : IRequestHandler<UserDeleteCommand, Result<int>>
	{
		private readonly UserManager<User> _userManager;

		public UserDeleteCommandHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<Result<int>> Handle(UserDeleteCommand command, CancellationToken cancellationToken)
		{
			var entity = await _userManager.FindByIdAsync(command.Id.ToString());

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Tài khoản Id {command.Id} không tồn tại.");
			}

			IdentityResult identityResult = await _userManager.DeleteAsync(entity);

			if (!identityResult.Succeeded)
			{
				return await Result<int>.FailureAsync(string.Join(", ", identityResult.Errors.Select(x => x.Description)));
			}

			return await Result<int>.SuccessAsync(entity.Id, $"Xóa tài khoản <b>{entity.UserName}</b> thành công.");
		}
	}
}
