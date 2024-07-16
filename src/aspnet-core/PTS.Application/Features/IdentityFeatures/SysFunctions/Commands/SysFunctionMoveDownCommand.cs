using MTS.Application.Interfaces.Repositories;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Commands
{
    public class SysFunctionMoveDownCommand : IRequest<Result<int>>
	{
		public int Id { get; set; }
	}

	internal class SysFunctionMoveDownCommandHandler : IRequestHandler<SysFunctionMoveDownCommand, Result<int>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly ISysFunctionRepo _sysFunctionRepo;

		public SysFunctionMoveDownCommandHandler(IIdentityUnitOfWork unitOfWork, ISysFunctionRepo sysFunctionRepo)
		{
			_unitOfWork = unitOfWork;
			_sysFunctionRepo = sysFunctionRepo;
		}

		public async Task<Result<int>> Handle(SysFunctionMoveDownCommand command, CancellationToken cancellationToken)
		{
			var entity = await _unitOfWork.Repository<SysFunction>().Entities.FirstOrDefaultAsync(x => x.Id == command.Id);

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Chức năng Id <b>{command.Id}</b> không tồn tại.");
			}

			var entityBelow = await _unitOfWork.Repository<SysFunction>().Entities
								.Where(x => x.ParentItemId == entity.ParentItemId && x.DisplayOrder > entity.DisplayOrder)
								.OrderBy(o => o.DisplayOrder)
								.FirstOrDefaultAsync();

			if (entityBelow == null)
			{
				return await Result<int>.SuccessAsync($"Di chuyển vị trí chức năng <b>{entity.FunctionName}</b> thành công.");
			}

			int displayOrder = entity.DisplayOrder;
			entity.DisplayOrder = entityBelow.DisplayOrder;
			entityBelow.DisplayOrder = displayOrder;

			await _unitOfWork.Repository<SysFunction>().UpdateAsync(entity.Id, entity);

			await _unitOfWork.Repository<SysFunction>().UpdateAsync(entityBelow.Id, entityBelow);

			await _unitOfWork.Save(cancellationToken);

			await _sysFunctionRepo.UpdateTreeOrder();

			return await Result<int>.SuccessAsync($"Di chuyển vị trí chức năng <b>{entity.FunctionName}</b> thành công.");
		}
	}
}
