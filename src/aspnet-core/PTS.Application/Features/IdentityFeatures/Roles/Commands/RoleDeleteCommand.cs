using MTS.Application.Common.Mappings;
using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.Roles.Commands
{
	public record RoleDeleteCommand : IRequest<Result<int>>, IMapFrom<Role>
	{
		public int Id { get; set; }
	}

	internal class RoleDeleteCommandHandler : IRequestHandler<RoleDeleteCommand, Result<int>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly RoleManager<Role> _roleManager;

		public RoleDeleteCommandHandler(IIdentityUnitOfWork unitOfWork, RoleManager<Role> roleManager)
		{
			_unitOfWork = unitOfWork;
			_roleManager = roleManager;
		}

		public async Task<Result<int>> Handle(RoleDeleteCommand command, CancellationToken cancellationToken)
		{
			var entity = await _roleManager.FindByIdAsync(command.Id.ToString());

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Vai trò Id <b>{command.Id}</b> không tồn tại.");
			}

			IdentityResult identityResult = await _roleManager.DeleteAsync(entity);

			if (!identityResult.Succeeded)
			{
				return await Result<int>.FailureAsync(string.Join(", ", identityResult.Errors.Select(x => x.Description)));
			}

			var functionRolesDelete = await _unitOfWork.Repository<SysFunctionRole>().Entities.Where(x => x.SysFunctionId == command.Id).ToListAsync();

			if (functionRolesDelete != null)
			{
				await _unitOfWork.Repository<SysFunctionRole>().DeleteManyAsync(functionRolesDelete);

				await _unitOfWork.Save(cancellationToken);
			}

			return await Result<int>.SuccessAsync(entity.Id, $"Xóa vai trò <b>{entity.Name}</b> thành công.");
		}
	}
}
