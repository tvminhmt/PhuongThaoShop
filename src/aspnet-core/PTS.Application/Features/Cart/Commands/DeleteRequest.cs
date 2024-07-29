using MediatR;
using PTS.Application.Dto;
using PTS.Domain.Entities;
using PTS.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Features.Cart.Commands
{
    public class DeleteCartDetailQuery : IRequest<ServiceResponse>
    {
        public int Id { get; set; }
    }
    public class DeleteCartDetailHandler : IRequestHandler<DeleteCartDetailQuery, ServiceResponse>
    {
        private readonly ICartDetailRepository _repository;
        public DeleteCartDetailHandler(ICartDetailRepository repository)
        {
            _repository = repository;
        }
        public async Task<ServiceResponse> Handle(DeleteCartDetailQuery request, CancellationToken cancellationToken)
        {

            if (await _repository.Delete(request.Id))
                return new ServiceResponse(true, "Xóa thành công");
            return new ServiceResponse(false, "Xóa thất bại");

        }
    }
}
