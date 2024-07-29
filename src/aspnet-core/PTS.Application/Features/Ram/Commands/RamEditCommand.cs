using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Mappings;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using System.ComponentModel.DataAnnotations;

namespace PTS.Application.Features.Ram.Commands
{
    public record RamEditCommand : IRequest<Result<int>>, IMapFrom<RamEntity>
    {
        public int Id { get; set; }
        public string Ma { get; set; }
        [MaxLength(100)]
        public string ThongSo { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
    }

    internal class RamEditCommandHandler : IRequestHandler<RamEditCommand, Result<int>>
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public RamEditCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<int>> Handle(RamEditCommand command, CancellationToken cancellationToken)
        {
			try
			{
				var entity = await _unitOfWork.Repository<RamEntity>().GetByIdAsync(command.Id);
                if (entity == null)
                {
                    return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại ");
                }
                if (command.Ma != entity.Ma)
                {
                    var existing = await _unitOfWork.Repository<RamEntity>().Entities.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Ma == command.Ma, cancellationToken);
                    if (existing != null)
                    {
                        return await Result<int>.FailureAsync("Mã đã tồn tại. Vui lòng chọn tên khác.");
                    }
                }
               
				entity = _mapper.Map<RamEntity>(command);
				await _unitOfWork.Repository<RamEntity>().UpdateAsync(entity.Id, entity);
				var result = await _unitOfWork.Save(cancellationToken);
				return result > 0
						? await Result<int>.SuccessAsync(entity.Id, "Cập nhật dữ liệu thành công.")
						: await Result<int>.FailureAsync("Cập nhật dữ liệu không thành công.");
			}
			catch (Exception e)
			{

				throw;
			}
		}
    }
}
