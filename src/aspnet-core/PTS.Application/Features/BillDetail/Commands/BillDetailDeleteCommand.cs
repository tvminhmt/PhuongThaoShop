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

namespace PTS.Application.Features.BillDetail.Commands
{
	public record BillDetailDeleteCommand : IRequest<Result<int>>, IMapFrom<BillDetailEntity>
	{
		public int Id { get; set; }
	}

	internal class BillDetailDeleteCommandHandler : IRequestHandler<BillDetailDeleteCommand, Result<int>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public BillDetailDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<int>> Handle(BillDetailDeleteCommand command, CancellationToken cancellationToken)
		{
			try
			{
				var entity = await _unitOfWork.Repository<BillDetailEntity>().GetByIdAsync(command.Id);
				if (entity == null)
				{
					return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại ");
				}
                await _unitOfWork.Repository<BillDetailEntity>().DeleteAsync(entity);

                var deleteResult = await _unitOfWork.Save(cancellationToken);
                if (deleteResult > 0)
                {
                    return await Result<int>.SuccessAsync($"Xóa dữ liệu thành công ");
                }

                return await Result<int>.FailureAsync($"Xóa dữ liệu không thành công ");
            }
			catch (Exception e)
			{
				throw;
			}
		}
	}
}
