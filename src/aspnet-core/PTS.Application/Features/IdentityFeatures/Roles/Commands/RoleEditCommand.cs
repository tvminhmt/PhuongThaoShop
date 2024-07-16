using MTS.Application.Common.Mappings;
using MTS.Application.Features.IdentityFeatures.Roles.Queries;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.Roles.Commands
{
	public class RoleEditCommand : IRequest<Result<int>>, IMapFrom<Role>, IMapFrom<RoleGetByIdDto>
    {
		public int Id { get; set; }

		[DisplayName("Tên vai trò")]
		public string Name { get; set; }

		[DisplayName("Mô tả")]
		public string Description { get; set; }
		public string ConcurrencyStamp { get; set; }
	}

	internal class RoleEditCommandHandler : IRequestHandler<RoleEditCommand, Result<int>>
	{
		private readonly RoleManager<Role> _roleManager;

		public RoleEditCommandHandler(RoleManager<Role> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<Result<int>> Handle(RoleEditCommand command, CancellationToken cancellationToken)
		{
			var entity = await _roleManager.FindByIdAsync(command.Id.ToString());

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Vai trò Id <b>{command.Id}</b> không tồn tại.");
			}

			entity.Name = command.Name;
			entity.Description = command.Description;
			entity.ConcurrencyStamp = command.ConcurrencyStamp;
			entity.NormalizedName = command.Name;

			IdentityResult identityResult = await _roleManager.UpdateAsync(entity);

			if (!identityResult.Succeeded)
			{
				return await Result<int>.FailureAsync(string.Join(", ", identityResult.Errors.Select(x => x.Description)));
			}

			return await Result<int>.SuccessAsync(entity.Id, $"Cập nhật vai trò <b>{entity.Name}</b> thành công.");
		}
	}
}
