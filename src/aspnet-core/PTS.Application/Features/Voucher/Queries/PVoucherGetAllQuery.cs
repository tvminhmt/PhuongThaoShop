using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Features.Voucher.Queries
{
	public record PVoucherGetAllQuery : IRequest<Result<List<VoucherDto>>>
	{
		
	}
	internal class PVoucherGetAllQueryHandler : IRequestHandler<PVoucherGetAllQuery, Result<List<VoucherDto>>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public PVoucherGetAllQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<List<VoucherDto>>> Handle(PVoucherGetAllQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<VoucherEntity>().Entities
				.AsNoTracking()
				.Where(x => x.Status == (int)StatusEnum.Active && x.StartDay <= DateTime.Now && DateTime.Now <= x.EndDay)
				.ProjectTo<VoucherDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
			return await Result<List<VoucherDto>>.SuccessAsync(result);
		}
	}
}
