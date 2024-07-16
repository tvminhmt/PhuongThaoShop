using AutoMapper;
using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Queries
{
	public record SysFunctionGetByIdQuery : IRequest<Result<SysFunctionGetByIdDto>>
	{
		public int Id { get; set; }
	}

	internal class SysFunctionGetByIdQueryHandler : IRequestHandler<SysFunctionGetByIdQuery, Result<SysFunctionGetByIdDto>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public SysFunctionGetByIdQueryHandler(IIdentityUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<SysFunctionGetByIdDto>> Handle(SysFunctionGetByIdQuery query, CancellationToken cancellationToken)
		{
			var entity = await _unitOfWork.Repository<SysFunction>().GetByIdAsync(query.Id);

			if (entity == null)
			{
				return await Result<SysFunctionGetByIdDto>.FailureAsync($"Chức năng Id <b>{query.Id}</b> không tồn tại.");
			}

			var sysFunctionGetByIdDto = _mapper.Map<SysFunctionGetByIdDto>(entity);

			sysFunctionGetByIdDto.RolesByFunctionList = await _unitOfWork.Repository<SysFunctionRole>().Entities
																.Where(x => x.SysFunctionId == query.Id)
																.Select(x => x.RoleId)
																.ToListAsync();

			return await Result<SysFunctionGetByIdDto>.SuccessAsync(sysFunctionGetByIdDto);
		}
	}
}
