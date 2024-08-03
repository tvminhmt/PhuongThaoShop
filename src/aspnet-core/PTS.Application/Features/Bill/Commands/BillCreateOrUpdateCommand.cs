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

namespace PTS.Application.Features.Bill.Commands
{
    public record BillCreateOrUpdateCommand : IRequest<Result<int>>, IMapFrom<BillEntity>
    {
        public int Id { get; set; }
        public string? InvoiceCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public int Payment { get; set; }
        public int IsPayment { get; set; }
        public decimal? Discount { get; set; }
        public int? VoucherEntityId { get; set; }
        public int? UserEntityId { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
    }

    internal class BillCreateOrUpdateCommandHandler : IRequestHandler<BillCreateOrUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BillCreateOrUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(BillCreateOrUpdateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.Id > 0)
                {
                    // Update
                    var existingEntity = await _unitOfWork.Repository<BillEntity>().Entities.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                    if (existingEntity == null)
                    {
                        return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại");
                    }
                    if (existingEntity.Status == (int)BillStatusEnum.Completed)
                    {
                        return await Result<int>.FailureAsync($"Hóa đơn đã hoàn thành không được sửa ");
                    }
                    existingEntity = _mapper.Map(command, existingEntity);
                    existingEntity.CrDateTime = DateTime.Now; 
                    await _unitOfWork.Repository<BillEntity>().UpdateFieldsAsync(existingEntity,
                        x => x.FullName,
                        x => x.Address,
                        x => x.PhoneNumber,
                        x => x.Payment,
                        x => x.IsPayment,
                        x => x.Status);

                    var updateResult = await _unitOfWork.Save(cancellationToken);
                    return updateResult > 0
                        ? await Result<int>.SuccessAsync(existingEntity.Id, "Cập nhật dữ liệu thành công.")
                        : await Result<int>.FailureAsync("Cập nhật dữ liệu không thành công.");
                }
                else
                {
                    // Create
                    var entity = _mapper.Map<BillEntity>(command);
                    entity.InvoiceCode = StringUtility.RandomString(12);
                    entity.CrDateTime = DateTime.Now;
                    await _unitOfWork.Repository<BillEntity>().AddAsync(entity);
                    var result = await _unitOfWork.Save(cancellationToken);
                    return result > 0
                        ? await Result<int>.SuccessAsync(entity.Id, "Thêm dữ liệu thành công.")
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
