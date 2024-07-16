using AutoMapper;
using MTS.Application.Common.Mappings;
using MTS.Application.Interfaces.Repositories;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Commands
{
    public record SysFunctionCreateCommand : IRequest<Result<int>>, IMapFrom<SysFunction>
	{
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

		public List<int> SelectedRoles { get; set; }

		[DisplayName("Thêm tiếp dữ liệu khác")]
		public bool AddMoreData { get; set; }
	}

	internal class SysFunctionCreateCommandHandler : IRequestHandler<SysFunctionCreateCommand, Result<int>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly ISysFunctionRepo _repo;
		private readonly IMapper _mapper;

		public SysFunctionCreateCommandHandler(IIdentityUnitOfWork unitOfWork, ISysFunctionRepo repo, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_repo = repo;
			_mapper = mapper;
		}

		public async Task<Result<int>> Handle(SysFunctionCreateCommand command, CancellationToken cancellationToken)
		{
			var entity = _mapper.Map<SysFunction>(command);

			if (string.IsNullOrEmpty(entity.FunctionDesc))
			{
				entity.FunctionDesc = entity.FunctionName;
			}

			if (string.IsNullOrEmpty(entity.Url))
			{
				entity.Url = "#";
			}

			entity.DisplayOrder = (await _unitOfWork.Repository<SysFunction>().Entities.AsNoTracking()
                                    .Where(x => x.ParentItemId == command.ParentItemId)
										.MaxAsync(x => (int?)x.DisplayOrder) ?? 0) + 1;

			await _unitOfWork.Repository<SysFunction>().AddAsync(entity);

			int sysFunctionCreateResult = await _unitOfWork.Save(cancellationToken);

			if (sysFunctionCreateResult > 0)
			{
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

					await _unitOfWork.Save(cancellationToken);
				}

				//Update TreeOrder
				await _repo.UpdateTreeOrder();
			}

			return await Result<int>.SuccessAsync(entity.Id, $"Thêm chức năng <b>{entity.FunctionName}</b> thành công.");
		}
	}
}
