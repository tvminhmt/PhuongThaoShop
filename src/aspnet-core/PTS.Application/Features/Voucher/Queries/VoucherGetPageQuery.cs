using AutoMapper;
using AutoMapper.QueryableExtensions;
using PTS.Application.DTOs;
using PTS.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.ProductDetail.Queries;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PTS.Application.Features.Voucher.DTOs;
using PTS.Core.Enums;

namespace PTS.Application.Features.Voucher.Queries
{
    public record VoucherGetPageQuery : BaseGetPageQuery, IRequest<PaginatedResult<VoucherDto>>
    {
     
    }
    internal class VoucherGetPagesQueryHandler : IRequestHandler<VoucherGetPageQuery, PaginatedResult<VoucherDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public VoucherGetPagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<VoucherDto>> Handle(VoucherGetPageQuery queryInput, CancellationToken cancellationToken)
        {
            var query = from listObj in _unitOfWork.Repository<VoucherEntity>().Entities.Where(x =>x.Status != (int)StatusEnum.Delete).AsNoTracking() select listObj;
            if (!string.IsNullOrEmpty(queryInput.Keywords))
            {
                query = query.Where(x => x.MaVoucher.Contains(queryInput.Keywords) || x.TenVoucher.Contains(queryInput.Keywords));
            }
            query = query.OrderBy(x => x.CrDateTime);
            var pQuery = query.ProjectTo<VoucherDto>(_mapper.ConfigurationProvider);
            var result = await pQuery.ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);
            return result;
        }
    }
}
