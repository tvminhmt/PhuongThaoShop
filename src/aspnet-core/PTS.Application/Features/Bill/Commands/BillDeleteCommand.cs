using AutoMapper;
using MediatR;
using PTS.Application.Common.Mappings;
using PTS.Core.Enums;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Features.Bill.Commands
{
	public record BillDeleteCommand : IRequest<Result<int>>, IMapFrom<BillEntity>
	{
		public int Id { get; set; }
	}

	internal class BillDeleteCommandHandler : IRequestHandler<BillDeleteCommand, Result<int>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public BillDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<int>> Handle(BillDeleteCommand command, CancellationToken cancellationToken)
		{
			try
			{
				var entity = await _unitOfWork.Repository<BillEntity>().GetByIdAsync(command.Id);
				if (entity == null)
				{
					return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại ");
				}
                if (entity.Status == (int)BillStatusEnum.Completed)
                {
                    return await Result<int>.FailureAsync($"Hóa đơn đã hoàn thành không được xóa ");
                }
                entity = _mapper.Map<BillEntity>(command);
				entity.Status = (int)StatusEnum.Delete;
				await _unitOfWork.Repository<BillEntity>().UpdateFieldsAsync(entity,
					x => x.Status);
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
