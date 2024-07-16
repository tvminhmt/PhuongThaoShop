using AutoMapper;
using MTS.Application.Common.Mappings;
using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Commands
{
	public class SysFunctionAssignUserCommand : IRequest<Result<int>>, IMapFrom<SysFunctionUser>
	{
		public int SysFunctionId { get; set; }
		public int UserId { get; set; }
	}

	internal class SysFunctionAssignUserCommandHandler : IRequestHandler<SysFunctionAssignUserCommand, Result<int>>
	{
		private readonly IMapper _mapper;
		private readonly IIdentityUnitOfWork _unitOfWork;
		public SysFunctionAssignUserCommandHandler(IMapper mapper, IIdentityUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<int>> Handle(SysFunctionAssignUserCommand command, CancellationToken cancellationToken)
		{
			bool addFunctionFavorite = false;

			var entity = await _unitOfWork.Repository<SysFunctionUser>().Entities
									.FirstOrDefaultAsync(x => x.SysFunctionId == command.SysFunctionId && x.UserId == command.UserId);

			if (entity == null)
			{
				addFunctionFavorite = true;

				entity = _mapper.Map<SysFunctionUser>(command);

				entity.DisplayOrder = (await _unitOfWork.Repository<SysFunctionUser>().Entities.AsNoTracking()
									.Where(x => x.UserId == command.UserId)
										.MaxAsync(x => (int?)x.DisplayOrder) ?? 0) + 1;

				await _unitOfWork.Repository<SysFunctionUser>().AddAsync(entity);
			}
			else
			{
				await _unitOfWork.Repository<SysFunctionUser>().DeleteAsync(entity);
			}

			await _unitOfWork.Save(cancellationToken);

			return await Result<int>.SuccessAsync($"{(addFunctionFavorite ? "Thêm" : "Hủy")} chức năng ưa thích thành công.");
		}
	}
}
