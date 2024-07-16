using AutoMapper;
using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.Roles.Queries
{
	public record RoleGetByIdQuery : IRequest<Result<RoleGetByIdDto>>
	{
		public int Id { get; set; }
	}

	internal class RoleGetByIdQueryHandler : IRequestHandler<RoleGetByIdQuery, Result<RoleGetByIdDto>>
	{
		private readonly IMapper _mapper;
		private readonly RoleManager<Role> _roleManager;
		private readonly IIdentityUnitOfWork _unitOfWork;

		public RoleGetByIdQueryHandler(IMapper mapper, RoleManager<Role> roleManager, IIdentityUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_roleManager = roleManager;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<RoleGetByIdDto>> Handle(RoleGetByIdQuery query, CancellationToken cancellationToken)
		{
			var entity = await _roleManager.FindByIdAsync(query.Id.ToString());

			if (entity == null)
			{
				return await Result<RoleGetByIdDto>.FailureAsync($"Vai trò ${query.Id} không tồn tại.");
			}

			var roleGetByIdDto = _mapper.Map<RoleGetByIdDto>(entity);

			roleGetByIdDto.SysFunctionsByRoleList = await _unitOfWork.Repository<SysFunctionRole>().Entities
																.Where(x => x.RoleId == query.Id)
																.Select(x => x.SysFunctionId)
																.ToListAsync();

			return await Result<RoleGetByIdDto>.SuccessAsync(roleGetByIdDto);
		}
	}
}
