using MTS.Application.Interfaces.Repositories;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Commands
{
    public record SysFunctionDeleteCommand : IRequest<Result<int>>
	{
		public int Id { get; set; }
	}

	internal class SysFunctionDeleteCommandHandler : IRequestHandler<SysFunctionDeleteCommand, Result<int>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly ISysFunctionRepo _sysFunctionRepo;

		public SysFunctionDeleteCommandHandler(IIdentityUnitOfWork unitOfWork, ISysFunctionRepo sysFunctionRepo)
		{
			_unitOfWork = unitOfWork;
			_sysFunctionRepo = sysFunctionRepo;
		}

		public async Task<Result<int>> Handle(SysFunctionDeleteCommand command, CancellationToken cancellationToken)
		{
			var entity = await _unitOfWork.Repository<SysFunction>().GetByIdAsync(command.Id);

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Chức năng Id <b>{command.Id}</b> không tồn tại.");
			}

			await _unitOfWork.Repository<SysFunction>().DeleteAsync(entity);


			var sysFunctionRolesDelete = await _unitOfWork.Repository<SysFunctionRole>().Entities
													.Where(x => x.SysFunctionId == command.Id).ToListAsync();

			if (sysFunctionRolesDelete != null)
			{
				await _unitOfWork.Repository<SysFunctionRole>().DeleteManyAsync(sysFunctionRolesDelete);
			}

			int sysFunctionDeleteResult = await _unitOfWork.Save(cancellationToken);

			if (sysFunctionDeleteResult > 0)
			{
				//Update TreeOrder
				await _sysFunctionRepo.UpdateTreeOrder();
			}

			return await Result<int>.SuccessAsync(entity.Id, $"Xóa chức năng <b>{entity.FunctionName}</b> thành công.");
		}
	}
}
