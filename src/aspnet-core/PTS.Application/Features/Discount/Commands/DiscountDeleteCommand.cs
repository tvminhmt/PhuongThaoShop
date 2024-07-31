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

namespace PTS.Application.Features.Discount.Commands
{
	public record DiscountDeleteCommand : IRequest<Result<int>>, IMapFrom<DiscountEntity>
	{
		public int Id { get; set; }
	}

	internal class DiscountDeleteCommandHandler : IRequestHandler<DiscountDeleteCommand, Result<int>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public DiscountDeleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<int>> Handle(DiscountDeleteCommand command, CancellationToken cancellationToken)
		{
			try
			{
				var entity = await _unitOfWork.Repository<DiscountEntity>().GetByIdAsync(command.Id);
				if (entity == null)
				{
					return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại ");
				}
				entity = _mapper.Map<DiscountEntity>(command);
				//entity.Status =(int)StatusEnum.Delete;
				await _unitOfWork.Repository<DiscountEntity>().UpdateFieldsAsync(entity
					);
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
