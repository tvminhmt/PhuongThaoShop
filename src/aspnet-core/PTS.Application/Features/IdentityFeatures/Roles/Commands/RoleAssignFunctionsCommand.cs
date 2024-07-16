﻿using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.Roles.Commands
{
	public record RoleAssignFunctionsCommand : IRequest<Result<int>>
	{
		public int Id { get; set; }
		public List<int> SelectedFunctions { get; set; }
	}

	internal class RoleAssignFunctionsCommandHandler : IRequestHandler<RoleAssignFunctionsCommand, Result<int>>
	{
		private readonly RoleManager<Role> _roleManager;
		private readonly IIdentityUnitOfWork _unitOfWork;
		public RoleAssignFunctionsCommandHandler(RoleManager<Role> roleManager, IIdentityUnitOfWork unitOfWork)
		{
			_roleManager = roleManager;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<int>> Handle(RoleAssignFunctionsCommand command, CancellationToken cancellationToken)
		{
			var entity = await _roleManager.FindByIdAsync(command.Id.ToString());

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Vai trò Id <b>{command.Id}</b> không tồn tại.");
			}

			//Xóa roles cũ
			var sysFunctionRolesList = await _unitOfWork.Repository<SysFunctionRole>().Entities
										.Where(x => x.RoleId == command.Id).ToListAsync();

			foreach (var sysFunctionRole in sysFunctionRolesList)
			{
				await _unitOfWork.Repository<SysFunctionRole>().DeleteAsync(sysFunctionRole);
			}

			//Add roles
			if (command.SelectedFunctions != null)
			{
				foreach (var functionId in command.SelectedFunctions)
				{
					await _unitOfWork.Repository<SysFunctionRole>()
							.AddAsync(new SysFunctionRole
							{
								SysFunctionId = functionId,
								RoleId = entity.Id
							});
				}
			}

			await _unitOfWork.Save(cancellationToken);

			return await Result<int>.SuccessAsync(entity.Id, $"Gán chức năng cho vai trò <b>{entity.Name}</b> thành công.");
		}
	}
}