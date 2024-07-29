using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Mappings;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Formats.Asn1.AsnWriter;

namespace PTS.Application.Features.Bill.Commands
{
    public record BillEditByCodeCommand : IRequest<Result<int>>, IMapFrom<BillEntity>
    {
	  public string Code { get; set; }	
	}

    internal class BillEditByCodeCommandHandler : IRequestHandler<BillEditByCodeCommand, Result<int>>
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public BillEditByCodeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<int>> Handle(BillEditByCodeCommand command, CancellationToken cancellationToken)
        {
			try
			{
				var entity = await _unitOfWork.Repository<BillEntity>().Entities.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.InvoiceCode == command.Code, cancellationToken);
                if (entity == null)
                {
                    return await Result<int>.FailureAsync($"Id <b>{command.Code}</b> không tồn tại ");
                }
                if (entity.Status == (int)BillStatusEnum.Completed)
                {
                    return await Result<int>.FailureAsync($"Hóa đơn đã hoàn thành không được sửa ");
                }
                entity.IsPayment = true;
				await _unitOfWork.Repository<BillEntity>().UpdateFieldsAsync(entity,
					x => x.IsPayment);
				var result = await _unitOfWork.Save(cancellationToken);
				return result > 0
						? await Result<int>.SuccessAsync(entity.Id, "Cập nhật dữ liệu thành công.")
						: await Result<int>.FailureAsync("Cập nhật dữ liệu không thành công.");
			}
			catch (Exception e)
			{

				throw;
			}
		}
    }
}
