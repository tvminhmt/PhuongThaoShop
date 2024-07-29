using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PTS.Application.Common.Mappings;
using PTS.Application.Interfaces.Repositories;
using PTS.Domain.Entities;
using PTS.Shared;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Formats.Asn1.AsnWriter;

namespace PTS.Application.Features.Ram.Commands
{
    public record RamCreateCommand : IRequest<Result<int>>, IMapFrom<RamEntity>
    {
        public string Ma { get; set; }
        [MaxLength(100)]
        public string ThongSo { get; set; }
        public int? CrUserId { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
    }

    internal class RamCreateCommandHandler : IRequestHandler<RamCreateCommand, Result<int>>
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public RamCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<int>> Handle(RamCreateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _unitOfWork.Repository<RamEntity>().Entities.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Ma == command.Ma, cancellationToken);
                if (existing != null)
                {
                    return await Result<int>.FailureAsync("Mã đã tồn tại. Vui lòng chọn tên khác.");
                }
                var entity = _mapper.Map<RamEntity>(command);
                entity.CrDateTime = DateTime.Now;
                await _unitOfWork.Repository<RamEntity>().AddAsync(entity); 
                var result = await _unitOfWork.Save(cancellationToken);
                return result > 0
                    ? await Result<int>.SuccessAsync(entity.Id, "Thêm dữ liệu thành công.")
                    : await Result<int>.FailureAsync("Thêm dữ liệu không thành công.");
            }
            catch (Exception ex)
            {
                return await Result<int>.FailureAsync($"Thêm dữ liệu không thành công: {ex.Message}");
            }
        }
    }
}
