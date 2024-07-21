﻿using MTS.Application.Common.Mappings;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using PTS.Shared;

namespace MTS.Application.Features.IdentityFeatures.Roles.Commands
{
	public record RoleCreateCommand : IRequest<Result<int>>, IMapFrom<Role>
	{
		[DisplayName("Tên vai trò")]
		public string Name { get; set; }

		[DisplayName("Mô tả")]
		public string Description { get; set; }

		[DisplayName("Thêm tiếp dữ liệu khác")]
		public bool AddMoreData { get; set; }
	}

	internal class RoleCreateCommandHandler : IRequestHandler<RoleCreateCommand, Result<int>>
	{
		private readonly RoleManager<Role> _roleManager;

		public RoleCreateCommandHandler(RoleManager<Role> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<Result<int>> Handle(RoleCreateCommand command, CancellationToken cancellationToken)
		{
			var entity = new Role
			{
				Name = command.Name,
				NormalizedName = command.Name,
				ConcurrencyStamp = command.Name,
				Description = command.Description
			};

			IdentityResult identityResult = await _roleManager.CreateAsync(entity);

			if (!identityResult.Succeeded)
			{
				return await Result<int>.FailureAsync(string.Join(", ", identityResult.Errors.Select(x => x.Description)));
			}

			return await Result<int>.SuccessAsync(entity.Id, $"Thêm vai trò <b>{entity.Name}</b> thành công.");
		}

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
