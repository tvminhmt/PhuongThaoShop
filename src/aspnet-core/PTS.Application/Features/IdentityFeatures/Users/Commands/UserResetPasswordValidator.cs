using FluentValidation;

namespace MTS.Application.Features.IdentityFeatures.Users.Commands
{
	public class UserResetPasswordValidator : AbstractValidator<UserResetPasswordCommand>
	{
		public UserResetPasswordValidator()
		{
			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Mật khẩu không để trống.");

			RuleFor(x => x.ConfirmPassword)
				.NotEmpty()
				.WithMessage("Mật khẩu xác nhận không để trống.");

			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password)
				.WithMessage("Mật khẩu xác nhận không chính xác.");
		}
	}
}
