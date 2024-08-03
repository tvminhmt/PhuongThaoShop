//using MediatR;
//using PTS.Application.Dto;
//using PTS.Core.Services;
//using PTS.Domain.Entities;
//using PTS.Domain.Model.Base;

//namespace PTS.Application.Features.Bill.Commands
//{
//    public class CreateOrUpdateBillQuery : IRequest<ApiResult<BillEntity>>
//    {
//        public RequestBillDto? RequestBillDto { get; set; }
//    }

//    public class CreateOrUpdateBillHandler : IRequestHandler<CreateOrUpdateBillQuery, ApiResult<BillEntity>>
//    {
//        private readonly IBillService _service;

//        public CreateOrUpdateBillHandler(IBillService service)
//        {
//            _service = service;
//        }

//        public async Task<ApiResult<BillEntity>> Handle(CreateOrUpdateBillQuery request, CancellationToken cancellationToken)
//        {
//            return await _service.CreateBill(request.RequestBillDto);
//        }
//    }
//}
