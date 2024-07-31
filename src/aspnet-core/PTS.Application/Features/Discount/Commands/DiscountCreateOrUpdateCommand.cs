using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Mappings;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace PTS.Application.Features.Discount.Commands
{
    public record DiscountCreateOrUpdateCommand : IRequest<Result<int>>, IMapFrom<DiscountEntity>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal Percentage { get; set; }
    }
    internal class DiscountCreateOrUpdateCommandHandler : IRequestHandler<DiscountCreateOrUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DiscountCreateOrUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(DiscountCreateOrUpdateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.Id > 0)
                {
                    // Update logic
                    var entity = await _unitOfWork.Repository<DiscountEntity>().GetByIdAsync(command.Id);
                    if (entity == null)
                    {
                        return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại ");
                    }
                    if (command.Code != entity.Code)
                    {
                        var existing = await _unitOfWork.Repository<DiscountEntity>().Entities.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Code == command.Code, cancellationToken);
                        if (existing != null)
                        {
                            return await Result<int>.FailureAsync("Mã đã tồn tại. Vui lòng chọn tên khác.");
                        }
                    }
                    entity = _mapper.Map(command, entity);
                    await _unitOfWork.Repository<DiscountEntity>().UpdateAsync(entity.Id, entity);
                }
                else
                {
                    // Create logic
                    var existing = await _unitOfWork.Repository<DiscountEntity>().Entities.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Code == command.Code, cancellationToken);
                    if (existing != null)
                    {
                        return await Result<int>.FailureAsync("Mã đã tồn tại. Vui lòng chọn mã khác.");
                    }
                    var entity = _mapper.Map<DiscountEntity>(command);
                   // entity.CrDateTime = DateTime.Now;
                    await _unitOfWork.Repository<DiscountEntity>().AddAsync(entity);
                }

                var result = await _unitOfWork.Save(cancellationToken);
                return result > 0
                    ? await Result<int>.SuccessAsync(command.Id > 0 ? command.Id : command.Id, command.Id > 0 ? "Cập nhật dữ liệu thành công." : "Thêm dữ liệu thành công.")
                    : await Result<int>.FailureAsync(command.Id > 0 ? "Cập nhật dữ liệu không thành công." : "Thêm dữ liệu không thành công.");
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync($"Xử lý không thành công: {ex.Message}");
            }
        }
    }
}
