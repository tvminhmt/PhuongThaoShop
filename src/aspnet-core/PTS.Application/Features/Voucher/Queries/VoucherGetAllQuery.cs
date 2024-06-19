using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.Voucher.Queries
{
	public record VoucherGetAllQuery : IRequest<Result<List<VoucherDto>>>
	{
    }
	internal class VoucherGetAllQueryHandler : IRequestHandler<VoucherGetAllQuery, Result<List<VoucherDto>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        public VoucherGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
        }
		public async Task<Result<List<VoucherDto>>> Handle(VoucherGetAllQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<VoucherEntity>().Entities
				.AsNoTracking()
				.Where(x => x.Status != (int)StatusEnum.Delete)
				.ProjectTo<VoucherDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
			return await Result<List<VoucherDto>>.SuccessAsync(result);
		}
	}
}
