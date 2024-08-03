using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Mappings;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using PTS.Core.Enums;
using PTS.Shared.Utilities;

namespace PTS.Application.Features.Address.Commands
{
    public record AddressCreateOrUpdateCommand : IRequest<Result<int>>, IMapFrom<AddressEntity>
    {
        public int Id { get; set; }
        public string AddressName { get; set; }
        public int UserEntityId { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
    }

    internal class AddressCreateOrUpdateCommandHandler : IRequestHandler<AddressCreateOrUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddressCreateOrUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(AddressCreateOrUpdateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.Id > 0)
                {
                    // Update
                    var existingEntity = await _unitOfWork.Repository<AddressEntity>().Entities.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.AddressId == command.Id, cancellationToken);

                    if (existingEntity == null)
                    {
                        return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại");
                    }
                    //if (existingEntity.Status == (int)AddressStatusEnum.Completed)
                    //{
                    //    return await Result<int>.FailureAsync($"Hóa đơn đã hoàn thành không được sửa ");
                    //}
                    existingEntity = _mapper.Map(command, existingEntity);
                    existingEntity.CrDateTime = DateTime.Now;
                    await _unitOfWork.Repository<AddressEntity>().UpdateFieldsAsync(existingEntity,
                        x => x.AddressName,
                        x => x.Status);

                    var updateResult = await _unitOfWork.Save(cancellationToken);
                    return updateResult > 0
                        ? await Result<int>.SuccessAsync(existingEntity.AddressId, "Cập nhật dữ liệu thành công.")
                        : await Result<int>.FailureAsync("Cập nhật dữ liệu không thành công.");
                }
                else
                {
                    // Create
                    var entity = _mapper.Map<AddressEntity>(command);
                    entity.CrDateTime = DateTime.Now;
                    await _unitOfWork.Repository<AddressEntity>().AddAsync(entity);
                    var result = await _unitOfWork.Save(cancellationToken);
                    return result > 0
                        ? await Result<int>.SuccessAsync(entity.AddressId, "Thêm dữ liệu thành công.")
                        : await Result<int>.FailureAsync("Thêm dữ liệu không thành công.");
                }
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync($"Thêm hoặc cập nhật dữ liệu không thành công: {ex.Message}");
            }
        }
    }
}
