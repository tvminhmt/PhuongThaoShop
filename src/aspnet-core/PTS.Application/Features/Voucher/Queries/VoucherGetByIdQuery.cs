using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.Voucher.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.Voucher.Queries
{
    public record VoucherGetByIdQuery : IRequest<Result<VoucherDto>>
	{
		public int Id { get; set; }
	}
	internal class VoucherGetByIdQueryHandler : IRequestHandler<VoucherGetByIdQuery, Result<VoucherDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public VoucherGetByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<VoucherDto>> Handle(VoucherGetByIdQuery queryInput, CancellationToken cancellationToken)
		{
			var result = await _unitOfWork.Repository<VoucherEntity>().Entities
				  .AsNoTracking()
				.Where(x => x.Id == queryInput.Id && x.Status != (int)StatusEnum.Delete)
				.ProjectTo<VoucherDto>(_mapper.ConfigurationProvider)
				.FirstOrDefaultAsync(cancellationToken);
			return await Result<VoucherDto>.SuccessAsync(result);
		}
	}
}
