using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Features.ProductDetail.DTOs;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Enums;
using PTS.Domain.Entities;
using PTS.Shared;

namespace PTS.Application.Features.ProductDetail.Queries
{
	public record ProductDetailGetCountByIdQuery : IRequest<int>
	{
		public int Id { get; set; }
	}

	internal class ProductDetailGetCountByIdQueryHandler : IRequestHandler<ProductDetailGetCountByIdQuery, int>
	{
		private readonly IUnitOfWork _unitOfWork;

		public ProductDetailGetCountByIdQueryHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<int> Handle(ProductDetailGetCountByIdQuery queryInput, CancellationToken cancellationToken)
		{
			int getCount = _unitOfWork.Repository<ProductDetailEntity>().Entities.AsNoTracking()
				.Where(x => x.Status > 0 && x.Id == queryInput.Id)
				.Join(_unitOfWork.Repository<SerialEntity>().Entities.AsNoTracking().Where(x => x.BillDetailEntityId == null),
					  a => a.Id,
					  b => b.ProductDetailEntityId,
					  (a, b) => new { a.Id })
				.Count();
			return getCount;
		}
    }
}
