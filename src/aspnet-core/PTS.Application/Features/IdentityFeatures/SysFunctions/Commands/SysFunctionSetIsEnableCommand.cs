using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Commands
{
	public class SysFunctionSetIsEnableCommand : IRequest<Result<int>>
	{
		public int Id { get; set; }
		public bool IsEnable { get; set; }
	}

	internal class SysFunctionSetIsEnableCommandHandler : IRequestHandler<SysFunctionSetIsEnableCommand, Result<int>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;

		public SysFunctionSetIsEnableCommandHandler(IIdentityUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<int>> Handle(SysFunctionSetIsEnableCommand command, CancellationToken cancellationToken)
		{
			var entity = await _unitOfWork.Repository<SysFunction>().GetByIdAsync(command.Id);

			if (entity == null)
			{
				return await Result<int>.FailureAsync($"Chức năng Id <b>{command.Id}</b> không tồn tại.");
			}

			entity.IsEnable = command.IsEnable;

			await _unitOfWork.Repository<SysFunction>().UpdateAsync(entity.Id, entity);

			await _unitOfWork.Save(cancellationToken);

			return await Result<int>.SuccessAsync($"Cập nhật trạng thái chức năng <b>{entity.FunctionName}</b> thành công.");
		}
	}
}
