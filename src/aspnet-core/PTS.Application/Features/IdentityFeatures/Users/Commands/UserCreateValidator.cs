using FluentValidation;
using System.Globalization;

namespace MTS.Application.Features.IdentityFeatures.Users.Commands
{
	public class UserCreateValidator : AbstractValidator<UserCreateCommand>
	{
		public UserCreateValidator()
		{
			RuleFor(x => x.UserName)
				.NotEmpty()
				.WithMessage("Tên truy cập không để trống.")
				.MaximumLength(200)
				.WithMessage("Tên truy cập không vượt quá 200 ký tự");

			RuleFor(x => x.FullName)
				.NotEmpty()
				.WithMessage("Họ tên không để trống.")
				.MaximumLength(200)
				.WithMessage("Họ tên không vượt quá 200 ký tự");

			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("Mật khẩu không để trống.");

			RuleFor(x => x.ConfirmPassword)
				.NotEmpty()
				.WithMessage("Mật khẩu xác nhận không để trống.");

			RuleFor(x => x.ConfirmPassword)
				.Equal(x => x.Password)
				.WithMessage("Mật khẩu xác nhận không chính xác.");

			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("Email không để trống.")
				.Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
				.WithMessage("Email không hợp lệ.")
				.MaximumLength(200)
				.WithMessage("Email không vượt quá 200 ký tự");

			RuleFor(x => x.PhoneNumber)
			   .Matches(@"^(84|0)\d{9,10}$")
			   .WithMessage("Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")
			   .MaximumLength(50)
			   .WithMessage("Số điện thoại không vượt quá 50 ký tự");

			RuleFor(c => c.BirthDay)
			   .Must(x => DateTime.TryParseExact(x, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
			   .When(x => !string.IsNullOrWhiteSpace(x.BirthDay))
			   .WithMessage("Chọn ngày sinh dạng ngày-tháng-năm.");
		}
	}
}
