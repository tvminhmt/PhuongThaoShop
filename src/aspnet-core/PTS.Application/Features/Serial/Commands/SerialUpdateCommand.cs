using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Mappings;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;

namespace PTS.Application.Features.Serial.Commands
{
    public record SerialUpdateCommand : IRequest<Result<int>>, IMapFrom<SerialEntity>
    {
        public int Id { get; set; }
       // public string SerialNumber { get; set; }
        public int? CrUserId { get; set; }
    //    public int Status { get; set; }
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
                    // Update logic
                    var entity = await _unitOfWork.Repository<SerialEntity>().GetByIdAsync(command.Id);
                    if (entity == null)
                    {
                        return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại ");
                    }
                    entity = _mapper.Map(command, entity);
                    await _unitOfWork.Repository<SerialEntity>().UpdateFieldsAsync(entity,
                        x => x.BillDetailEntityId
                        );
                var result = await _unitOfWork.Save(cancellationToken);
                return result > 0
                    ? await Result<int>.SuccessAsync( "Cập nhật dữ liệu thành công.")
                    : await Result<int>.FailureAsync("Cập nhật dữ liệu không thành công.");
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync($"Xử lý không thành công: {ex.Message}");
            }
        }
    }
}
