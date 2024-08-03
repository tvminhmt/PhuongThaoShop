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

namespace PTS.Application.Features.Image.Queries
{
    public record ImageGetPageQuery : BaseGetPageQuery, IRequest<PaginatedResult<ImageDto>>
    {
     
    }
    internal class ImageGetPagesQueryHandler : IRequestHandler<ImageGetPageQuery, PaginatedResult<ImageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ImageGetPagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<ImageDto>> Handle(ImageGetPageQuery queryInput, CancellationToken cancellationToken)
        {
            var query = from listObj in _unitOfWork.Repository<ImageEntity>().Entities.AsNoTracking() select listObj;
            if (!string.IsNullOrEmpty(queryInput.Keywords))
            {
                query = query.Where(x => x.Name.Contains(queryInput.Keywords));
            }
            query = query.OrderBy(x => x.CrDateTime);
            var pQuery = query.ProjectTo<ImageDto>(_mapper.ConfigurationProvider);
            var result = await pQuery.ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);
            return result;
        }
    }
}
