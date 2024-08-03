using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.BillDetail.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.BillDetail.Queries
{
    public record BillDetailGetByBillIdQuery : IRequest<Result<List<BillDetailDto>>>
    {
        public int BillId { get; set; } 
    }
    internal class BillDetailGetByBillIdQueryHandler : IRequestHandler<BillDetailGetByBillIdQuery, Result<List<BillDetailDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BillDetailGetByBillIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<List<BillDetailDto>>> Handle(BillDetailGetByBillIdQuery queryInput, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository<BillDetailEntity>().Entities
                .AsNoTracking()
                .Where(x =>  x.BillEntityId == queryInput.BillId)
                .ProjectTo<BillDetailDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            return await Result<List<BillDetailDto>>.SuccessAsync(result);
        }
    }
}
