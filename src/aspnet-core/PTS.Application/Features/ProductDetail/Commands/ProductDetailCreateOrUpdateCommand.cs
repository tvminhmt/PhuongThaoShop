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

namespace PTS.Application.Features.ProductDetail.Commands
{
    public record ProductDetailCreateOrUpdateCommand : IRequest<Result<int>>, IMapFrom<ProductDetailEntity>
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public Decimal Price { get; set; }
        public Decimal OldPrice { get; set; }
        public string? Upgrade { get; set; }
        public string? Description { get; set; }
        public int Product { get; set; }
        public int? Color { get; set; }
        public int? Ram { get; set; }
        public int? Cpu { get; set; }
        public int? HardDrive { get; set; }
        public int? Screen { get; set; }
        public int? CardVGA { get; set; }
        public int? CrUserId { get; set; }
        public int? DiscountId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int? UpdUserId { get; set; }
        public DateTime? UpdDateTime { get; set; }
        public int Status { get; set; }
    }
    internal class ProductDetailCreateOrUpdateCommandHandler : IRequestHandler<ProductDetailCreateOrUpdateCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductDetailCreateOrUpdateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<int>> Handle(ProductDetailCreateOrUpdateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                if (command.Id > 0)
                {
                    // Update logic
                    var entity = await _unitOfWork.Repository<ProductDetailEntity>().GetByIdAsync(command.Id);
                    if (entity == null)
                    {
                        return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại ");
                    }
                    if (command.Code != entity.Code)
                    {
                        var existing = await _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking()
                            .FirstOrDefaultAsync(x => x.Code == command.Code, cancellationToken);
                        if (existing != null)
                        {
                            return await Result<int>.FailureAsync("Mã đã tồn tại. Vui lòng chọn mã khác.");
                        }
                    }
                    entity = _mapper.Map(command, entity);
                    await _unitOfWork.Repository<ProductDetailEntity>().UpdateAsync(entity.Id, entity);
                }
                else
                {
                    // Create logic
                    var existing = await _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Code == command.Code, cancellationToken);
                    if (existing != null)
                    {
                        return await Result<int>.FailureAsync("Mã đã tồn tại. Vui lòng chọn mã khác.");
                    }
                    var entity = _mapper.Map<ProductDetailEntity>(command);
                    entity.CrDateTime = DateTime.Now;
                    await _unitOfWork.Repository<ProductDetailEntity>().AddAsync(entity);
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
