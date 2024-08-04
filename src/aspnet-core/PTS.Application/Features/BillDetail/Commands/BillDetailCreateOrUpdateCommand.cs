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
using PTS.Domain.Common.Interfaces;

namespace PTS.Application.Features.BillDetail.Commands
{
    public record BillDetailCreateOrUpdateCommand : IRequest<Result<int>>, IMapFrom<BillDetailEntity>
    {
        public int Id { get; set; }
        public string CodeProductDetail { get; set; }
        public int Quantity { get; set; }
        public int BillEntityId { get; set; }
        public int? CrUserId { get; set; }
        public int Status { get; set; }
    }

    internal class BillDetailCreateOrUpdateCommandHandler : IRequestHandler<BillDetailCreateOrUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BillDetailCreateOrUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(BillDetailCreateOrUpdateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking()
                       .FirstOrDefaultAsync(x => x.Code == command.CodeProductDetail, cancellationToken);
                if (product == null) {
                    return await Result<int>.FailureAsync($"Mã sản phẩm chi tiết không tồn tại trong hệ thống");
                }
                var countProduct = GetCount(product.Id);
                if(countProduct < command.Quantity)
                {
                    return await Result<int>.FailureAsync($"Bạn chỉ có thể chọn tối đa {countProduct} sản phẩm");
                }
                if (command.Id > 0)
                {
                    // Update
                    var existingEntity = await _unitOfWork.Repository<BillDetailEntity>().Entities.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

                    if (existingEntity == null)
                    {
                        return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại");
                    }
                    //if (existingEntity.Status == (int)BillDetailStatusEnum.Completed)
                    //{
                    //    return await Result<int>.FailureAsync($"Hóa đơn đã hoàn thành không được sửa ");
                    //}
                    existingEntity = _mapper.Map(command, existingEntity);
                    existingEntity.CrDateTime = DateTime.Now;
                    await _unitOfWork.Repository<BillDetailEntity>().UpdateFieldsAsync(existingEntity,
                        x => x.CodeProductDetail,
                        x => x.Quantity,
                        x => x.Price,
                        x => x.Status);

                    var updateResult = await _unitOfWork.Save(cancellationToken);
                    return updateResult > 0
                        ? await Result<int>.SuccessAsync(existingEntity.Id, "Cập nhật dữ liệu thành công.")
                        : await Result<int>.FailureAsync("Cập nhật dữ liệu không thành công.");
                }
                else
                {
                    var existingEntity = await _unitOfWork.Repository<BillDetailEntity>().Entities.AsNoTracking()
                      .FirstOrDefaultAsync(x => x.CodeProductDetail == command.CodeProductDetail, cancellationToken);
                    if(existingEntity != null)
                    {
                        existingEntity.Quantity += command.Quantity;
                        existingEntity.UpdDateTime = DateTime.Now;
                        await _unitOfWork.Repository<BillDetailEntity>().AddAsync(existingEntity);
                        var result = await _unitOfWork.Save(cancellationToken);
                        return result > 0
                            ? await Result<int>.SuccessAsync(existingEntity.Id, "Thêm dữ liệu thành công.")
                            : await Result<int>.FailureAsync("Thêm dữ liệu không thành công.");
                    }
                    else
                    {
                      var entity = _mapper.Map<BillDetailEntity>(command);
                    entity.Code = StringUtility.RandomString(12);
                    entity.CrDateTime = DateTime.Now;
                    await _unitOfWork.Repository<BillDetailEntity>().AddAsync(entity);
                    var result = await _unitOfWork.Save(cancellationToken);
                    return result > 0
                        ? await Result<int>.SuccessAsync(entity.Id, "Thêm dữ liệu thành công.")
                        : await Result<int>.FailureAsync("Thêm dữ liệu không thành công.");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync($"Thêm hoặc cập nhật dữ liệu không thành công: {ex.Message}");
            }
        }
        private int GetCount(int id)
        {
            int getCount = _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking()
                .Where(x => x.Status > 0 && x.Id == id)
                .Join(_unitOfWork.Repository<SerialEntity>().Entities.AsNoTracking(),
                      a => a.Id,
                      b => b.ProductDetailEntityId,
                      (a, b) => new { a.Id })
                .Count();
            return getCount;
        }
    }
}
