using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Commands
{
	public record SysFunctionAssignRolesCommand : IRequest<Result<int>>
	{
		public int Id { get; set; }
		public List<int> SelectedRoles { get; set; }
	}

	internal class SysFunctionAssignRolesCommandHandler : IRequestHandler<SysFunctionAssignRolesCommand, Result<int>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		public SysFunctionAssignRolesCommandHandler(IIdentityUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<int>> Handle(SysFunctionAssignRolesCommand command, CancellationToken cancellationToken)
		{
			var entity = await _unitOfWork.Repository<SysFunction>().Entities
									.FirstOrDefaultAsync(x => x.Id == command.Id);

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Chức năng Id <b>${command.Id}</b> không tồn tại.");
			}

			//Xóa roles hiện tại
			var sysFunctionRolesList = await _unitOfWork.Repository<SysFunctionRole>().Entities
										.Where(x => x.SysFunctionId == command.Id).ToListAsync();

			foreach (var sysFunctionRole in sysFunctionRolesList)
			{
				await _unitOfWork.Repository<SysFunctionRole>().DeleteAsync(sysFunctionRole);
			}

			//Add roles đã chọn
			if (command.SelectedRoles != null)
			{
				foreach (var roleId in command.SelectedRoles)
				{
					await _unitOfWork.Repository<SysFunctionRole>()
							.AddAsync(new SysFunctionRole
							{
								SysFunctionId = entity.Id,
								RoleId = roleId
							});
				}
			}

			await _unitOfWork.Save(cancellationToken);

			return await Result<int>.SuccessAsync(entity.Id, $"Gán vai trò cho chức năng <b>{entity.FunctionName}</b> thành công.");
		}
	}
}
