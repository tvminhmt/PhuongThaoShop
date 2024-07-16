using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.Users.Queries
{
	public record UserStatisticQuery : IRequest<Result<int>>
	{
	}

	public class UserStatisticQueryHandler : IRequestHandler<UserStatisticQuery, Result<int>>
	{
		private readonly UserManager<Domain.Entities.Identity.User> _userManager;

		public UserStatisticQueryHandler(UserManager<Domain.Entities.Identity.User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<Result<int>> Handle(UserStatisticQuery request, CancellationToken cancellationToken)
		{
			var result = await _userManager.Users.AsNoTracking()
							.Where(x => x.IsEnabled.HasValue && x.IsEnabled.Value == true)
							.CountAsync(cancellationToken);

			return await Result<int>.SuccessAsync(result);
		}
	}
}
