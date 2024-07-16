using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.SysFunctionUsers.Commands
{
    public record SysFunctionUsersUpdateDisplayOrderCommand : IRequest<Result<int>>
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int DisplayOrder { get; set; }
    }

    internal class SysFunctionUsersUpdateDisplayOrderCommandHandler : IRequestHandler<SysFunctionUsersUpdateDisplayOrderCommand, Result<int>>
    {
        private readonly IIdentityUnitOfWork _unitOfWork;
        public SysFunctionUsersUpdateDisplayOrderCommandHandler(IIdentityUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(SysFunctionUsersUpdateDisplayOrderCommand command, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.Repository<SysFunctionUser>().Entities.FirstOrDefaultAsync(x => x.UserId == command.UserId && x.SysFunctionId == command.Id, cancellationToken);

            if (entity == null)
            {
                return await Result<int>.FailureAsync($"Chức năng ưa thích Id {command.Id} không tồn tại.");
            }

            entity.DisplayOrder = command.DisplayOrder;

            await _unitOfWork.Repository<SysFunctionUser>().UpdateFieldsAsync(entity, x => x.DisplayOrder);

            int updateResult = await _unitOfWork.Save(cancellationToken);

            if (updateResult > 0)
            {
                return await Result<int>.SuccessAsync(entity.SysFunctionId, "Cập nhật vị trí thành công.");
            }

            return await Result<int>.FailureAsync("Cập nhật vị trí không thành công.");
        }
    }
}
