using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Mappings;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PTS.Application.Features.Serial.Commands
{
    public record SerialUpdateCommand : IRequest<Result<int>>, IMapFrom<SerialEntity>
    {
        public int[] Ids { get; set; }
        public int? CrUserId { get; set; }
        public int? ProductDetailEntityId { get; set; }
        public int? BillDetailEntityId { get; set; }
    }

    internal class SerialUpdateCommandHandler : IRequestHandler<SerialUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SerialUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(SerialUpdateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var id in command.Ids)
                {
                    var entity = await _unitOfWork.Repository<SerialEntity>().GetByIdAsync(id);
                    if (entity == null)
                    {
                        return await Result<int>.FailureAsync($"Id <b>{id}</b> không tồn tại ");
                    }

                    entity.BillDetailEntityId = command.BillDetailEntityId;
                    entity.CrUserId = command.CrUserId;

                    await _unitOfWork.Repository<SerialEntity>().UpdateFieldsAsync(entity,
                        x => x.BillDetailEntityId,
                        x => x.CrUserId
                    );
                }

                var result = await _unitOfWork.Save(cancellationToken);
                return result > 0
                    ? await Result<int>.SuccessAsync("Cập nhật dữ liệu thành công.")
                    : await Result<int>.FailureAsync("Cập nhật dữ liệu không thành công.");
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync($"Xử lý không thành công: {ex.Message}");
            }
        }
    }
}
