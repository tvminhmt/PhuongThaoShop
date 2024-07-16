using AutoMapper;
using MTS.Application.Common.Mappings;
using MTS.Application.Features.IdentityFeatures.SysFunctions.Queries;
using MTS.Application.Interfaces.Repositories;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Commands
{
    public class SysFunctionEditCommand : IRequest<Result<int>>, IMapFrom<SysFunction>, IMapFrom<SysFunctionGetByIdDto>
	{
		public int Id { get; set; }

		[DisplayName("Tên chức năng")]
		public string FunctionName { get; set; }

		[DisplayName("Mô tả")]
		public string FunctionDesc { get; set; }

		[DisplayName("Chức năng cha")]
		public int ParentItemId { get; set; }

		[DisplayName("Đường dẫn")]
		public string Url { get; set; }

		[DisplayName("Icon (sử dụng Font Awesome 5)")]
		public string IconPath { get; set; }
		public int DisplayOrder { get; set; }

		[DisplayName("Hiển thị")]
		public bool IsShow { get; set; }

		[DisplayName("Kích hoạt")]
		public bool IsEnable { get; set; }
		public int TreeOrder { get; set; }
		public byte TreeLevel { get; set; }
        public byte HasChild { get; set; }
        public int CurrentParentId { get; set; }
		public List<int> SelectedRoles { get; set; }
		public List<int> RolesByFunctionList { get; set; }
	}

	internal class SysFunctionEditCommandHandler : IRequestHandler<SysFunctionEditCommand, Result<int>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly ISysFunctionRepo _repo;
		private readonly IMapper _mapper;

		public SysFunctionEditCommandHandler(IIdentityUnitOfWork unitOfWork, ISysFunctionRepo repo, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(SysFunctionEditCommand command, CancellationToken cancellationToken)
		{
			var entity = await _unitOfWork.Repository<SysFunction>().Entities
									.FirstOrDefaultAsync(x => x.Id == command.Id);

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Chức năng Id <b>{command.Id}</b> không tồn tại.");
			}

			entity = _mapper.Map<SysFunction>(command);

			if (string.IsNullOrEmpty(entity.FunctionDesc))
			{
				entity.FunctionDesc = entity.FunctionName;
			}

			if (string.IsNullOrEmpty(entity.Url))
			{
				entity.Url = "#";
			}

			if (command.CurrentParentId != command.ParentItemId)
			{
				entity.DisplayOrder = (await _unitOfWork.Repository<SysFunction>().Entities.AsNoTracking()
                    .Where(x => x.ParentItemId == command.ParentItemId).MaxAsync(x => (int?)x.DisplayOrder) ?? 0) + 1;
			}

			await _unitOfWork.Repository<SysFunction>().UpdateAsync(entity.Id, entity);

			//Xóa roles cũ
			var sysFunctionRolesList = await _unitOfWork.Repository<SysFunctionRole>().Entities
										.Where(x => x.SysFunctionId == command.Id).ToListAsync();

			foreach (var sysFunctionRole in sysFunctionRolesList)
			{
				await _unitOfWork.Repository<SysFunctionRole>().DeleteAsync(sysFunctionRole);
			}

			//Add roles
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

			int sysFunctionEditResult = await _unitOfWork.Save(cancellationToken);

			if (sysFunctionEditResult > 0)
			{
				//Update TreeOrder
				if (command.CurrentParentId != command.ParentItemId)
				{
					await _repo.UpdateTreeOrder();
				}
			}

			return await Result<int>.SuccessAsync(entity.Id, $"Cập nhật chức năng <b>{entity.FunctionName}</b> thành công.");
		}
	}
}
