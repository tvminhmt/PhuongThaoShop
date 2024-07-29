using AutoMapper;
using AutoMapper.QueryableExtensions;
using PTS.Application.DTOs;
using PTS.Application.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using PTS.Application.Features.Bill.DTOs;
using PTS.Core.Enums;

namespace PTS.Application.Features.Bill.Queries
{
    public record BillGetPageQuery : BaseGetPageQuery, IRequest<PaginatedResult<BillGetPageDto>>
    {
     
    }
    internal class BillGetPagesQueryHandler : IRequestHandler<BillGetPageQuery, PaginatedResult<BillGetPageDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BillGetPagesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginatedResult<BillGetPageDto>> Handle(BillGetPageQuery queryInput, CancellationToken cancellationToken)
        {
            var query = from listObj in _unitOfWork.Repository<BillEntity>().Entities.Where(x =>x.Status != (int)StatusEnum.Delete).AsNoTracking() select listObj;
            if (!string.IsNullOrEmpty(queryInput.Keywords))
            {
               // query = query.Where(x => x.Ma.Contains(queryInput.Keywords) || x.ThongSo.Contains(queryInput.Keywords));
            }
            query = query.OrderBy(x => x.CrDateTime);
            var pQuery = query.ProjectTo<BillGetPageDto>(_mapper.ConfigurationProvider);
            var resultVar = await pQuery.ToPaginatedListAsync(queryInput.Page, queryInput.PageSize, cancellationToken);
            if (resultVar.Data != null && resultVar.Data.Any())
            {
                int index = (queryInput.Page - 1) * queryInput.PageSize + 1;
                foreach (var item in resultVar.Data)
                {
                    item.StrPayment = GetStringPayment(item.Payment);
                    item.Stt = index++;
                    item.StrStatus = GetStringStatus(item.Status);
                    item.StrIsPayment = item.IsPayment ? "Đã thanh toán" : "Chưa thanh toán";
                }
            }
            return resultVar;
        }
        private string GetStringPayment(int payment)
        {
            return payment switch
            {
                1 => "Thanh toán tại cửa hàng",
                2 => "Thanh toán khi nhận hàng (COD)",
                3 => "Thanh toán bằng chuyển khoản ngân hàng",
                4 => "Thanh toán qua VNPAY",
                _ => "Không xác định"
            };
        }
        private string GetStringStatus(int status)
        {
            return status switch
            {
                0 => "Đã xóa",
                2 => "Chờ xác nhận",
                3 => "Chờ gửi hàng",
                4 => "Đang giao hàng",
                5 => "Giao hàng thành công",
                6 => "Giao hàng thất bại",
                7 => "Đã hủy",
                8 => "Hoàn thành",
                _ => "Không xác định"
            };
        }
    }
}
