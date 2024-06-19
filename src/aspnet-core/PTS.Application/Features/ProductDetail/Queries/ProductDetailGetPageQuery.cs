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

namespace PTS.Application.Features.PhapDienDocs.Fields.Queries
{
    public record ProductDetailGetPageQuery : BaseGetPageQuery, IRequest<PaginatedResult<ProductDetailDto>>
    {
     
    }
    internal class ProductDetailGetPagesQueryHandler : IRequestHandler<ProductDetailGetPageQuery, PaginatedResult<ProductDetailDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        public ProductDetailGetPagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper,ISender sender)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _sender = sender;
        }
        public async Task<PaginatedResult<ProductDetailDto>> Handle(ProductDetailGetPageQuery queryInput, CancellationToken cancellationToken)
        {
            var query = from fields in _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking() select fields;
            //if (!string.IsNullOrWhiteSpace(queryInput.Keywords))
            //{
            //    query = query.Where(field => field.FieldName.Contains(queryInput.Keywords));
            //}
            //if(queryInput.ParentFieldId > 0)
            //{
            //    query = query.Where(a=>a.ParentFieldId == queryInput.ParentFieldId);
            //}
            //if (queryInput.ReviewStatusId > 0)
            //{
            //    query = query.Where(x => x.ReviewStatusId == queryInput.ReviewStatusId);
            //}
            //// Kiểm tra quyền với site
         
            //query = query.OrderBy(x => x.DisplayOrder);
            var pQuery = query.ProjectTo<ProductDetailDto>(_mapper.ConfigurationProvider);
            var result = await pQuery.ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);
            return result;
        }
    }
}
