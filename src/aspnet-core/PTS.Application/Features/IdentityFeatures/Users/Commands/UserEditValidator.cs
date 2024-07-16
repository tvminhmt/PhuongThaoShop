using FluentValidation;
using MTS.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace MTS.Application.Features.IdentityFeatures.Users.Commands
{
	public class UserEditValidator : AbstractValidator<UserEditCommand>
	{
		private readonly UserManager<User> _userManager;
		public UserEditValidator(UserManager<User> userManager)
		{
			_userManager = userManager;

			RuleFor(x => x.FullName)
				.NotEmpty()
				.WithMessage("Họ tên không để trống.")
				.MaximumLength(200)
				.WithMessage("Họ tên không vượt quá 200 ký tự");

			RuleFor(x => x.Email)
			   .NotEmpty()
			   .WithMessage("Email không để trống.")
			   .Matches(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$")
			   .WithMessage("Email không hợp lệ.")
			   .MustAsync(IsUniqueEmail)
			   .WithMessage(x => $"Email {x.Email} đã được sử dụng.")
			   .MaximumLength(200)
			   .WithMessage("Email không vượt quá 200 ký tự");

			RuleFor(x => x.PhoneNumber)
			   .Matches(@"^(84|0)\d{9,10}$")
			   .WithMessage("Số điện thoại hợp lệ dạng 84xxxxxxxxx hoặc 0xxxxxxxxx")
			   .MustAsync(IsUniquePhoneNumber)
			   .WithMessage(x => $"Số điện thoại {x.PhoneNumber} đã được sử dụng.")
			   .MaximumLength(50)
			   .WithMessage("Số điện thoại không vượt quá 50 ký tự");

			RuleFor(c => c.BirthDay)
			   .Must(x => DateTime.TryParseExact(x, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
			   .When(x => !string.IsNullOrWhiteSpace(x.BirthDay))
			   .WithMessage("Chọn ngày sinh dạng ngày-tháng-năm.");
		}

		private async Task<bool> IsUniqueEmail(UserEditCommand instance, string email, CancellationToken cancellationToken)
		{
			if (!string.IsNullOrWhiteSpace(email))
			{
				var isExistingEmail = await _userManager.Users.AsNoTracking()
					.FirstOrDefaultAsync(x => x.Email.ToLower().Trim().Equals(email.ToLower().Trim()) && x.Id != instance.Id, cancellationToken);

				return isExistingEmail == null;
			}

			return true;
		}

		private async Task<bool> IsUniquePhoneNumber(UserEditCommand instance, string phoneNumber, CancellationToken cancellationToken)
		{
			if (!string.IsNullOrWhiteSpace(phoneNumber))
			{
				var isExistingPhoneNumber = await _userManager.Users.AsNoTracking()
					.FirstOrDefaultAsync(x => x.PhoneNumber.Equals(phoneNumber) && x.Id != instance.Id, cancellationToken);

				return isExistingPhoneNumber == null;
			}

			return true;
		}
	}
}
