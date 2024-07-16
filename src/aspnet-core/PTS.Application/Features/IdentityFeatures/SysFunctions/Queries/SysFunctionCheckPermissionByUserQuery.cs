using MTS.Application.Interfaces.Repositories.Identity;
using IC.Shared;
using MediatR;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Queries
{
    public record SysFunctionCheckPermissionByUserQuery : IRequest<Result<bool>>
	{
		public int UserId { get; set; } = 0;
		public string UserName { get; set; } = "";
		public string Url { get; set; }

		public bool AllowByParent { get; set; }

		public SysFunctionCheckPermissionByUserQuery(int userId, string url, bool allowByParent = true) { UserId = userId; Url = url; AllowByParent = allowByParent; }
		public SysFunctionCheckPermissionByUserQuery(string userName, string url, bool allowByParent = true) { UserName = userName; Url = url; AllowByParent = allowByParent; }
	}

	internal class SysFunctionCheckPermissionByUserQueryHandler : IRequestHandler<SysFunctionCheckPermissionByUserQuery, Result<bool>>
	{
		private readonly ISysFunctionRepo _sysFunctionRepo;

		public SysFunctionCheckPermissionByUserQueryHandler(ISysFunctionRepo sysFunctionRepo)
		{
			_sysFunctionRepo = sysFunctionRepo;
		}

		public async Task<Result<bool>> Handle(SysFunctionCheckPermissionByUserQuery query, CancellationToken cancellationToken)
		{
			bool result = false;

			if (query.UserId > 0)
			{
				result = await _sysFunctionRepo.UserHasPermissionOnUrl(query.UserId, query.Url, query.AllowByParent);
			}
			else if (!string.IsNullOrWhiteSpace(query.UserName))
			{
				result = await _sysFunctionRepo.UserHasPermissionOnUrl(query.UserName, query.Url, query.AllowByParent);
			}

			return await Result<bool>.SuccessAsync(result);
		}
	}
}
