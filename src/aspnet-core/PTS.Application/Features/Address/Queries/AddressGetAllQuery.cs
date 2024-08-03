using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.Address.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.Address.Queries
{
    public record AddressGetAllQuery : IRequest<Result<List<AddressDto>>>
	{
    }
	internal class AddressGetAllQueryHandler : IRequestHandler<AddressGetAllQuery, Result<List<AddressDto>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public AddressGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
        }
		public async Task<Result<List<AddressDto>>> Handle(AddressGetAllQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<AddressEntity>().Entities
				.AsNoTracking()
				.Where(x => x.Status != (int)StatusEnum.Delete)
				.ProjectTo<AddressDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
			return await Result<List<AddressDto>>.SuccessAsync(result);
		}
	}
}
