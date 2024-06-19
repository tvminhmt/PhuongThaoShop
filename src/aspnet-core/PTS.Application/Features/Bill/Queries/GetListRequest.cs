using MediatR;
using PTS.Application.Dto;
using PTS.Application.Interfaces.Repositories;
using PTS.Core.Services;

namespace PTS.Application.Features.Bill.Queries
{
    public class GetListBillQuery : IRequest<ResponseDto>
    {
    }
    public class GetListBillHandler : IRequestHandler<GetListBillQuery, ResponseDto>
    {
        private readonly IBillRepository _repo;
        public GetListBillHandler(IBillRepository repo)
        {
            _repo = repo;
        }
        public async Task<ResponseDto> Handle(GetListBillQuery request, CancellationToken cancellationToken)
        {
            return new ResponseDto
            {
                Result = await _repo.GetAll(),
                IsSuccess = true,
                Code = 200,
                Message = ""
            };
        }
    }
}
