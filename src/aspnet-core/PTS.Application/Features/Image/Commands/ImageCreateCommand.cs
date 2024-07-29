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

namespace PTS.Application.Features.Image.Commands
{
    public record ImageCreateCommand : IRequest<Result<int>>, IMapFrom<ImageEntity>
    {
		public string Url { get; set; }
		public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? CrDateTime { get; set; }
        public int Status { get; set; }
	}

    internal class ImageCreateCommandHandler : IRequestHandler<ImageCreateCommand, Result<int>>
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ImageCreateCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<int>> Handle(ImageCreateCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var existing = await _unitOfWork.Repository<ImageEntity>().Entities.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Url == command.Url, cancellationToken);
                if (existing != null)
                {
                    return await Result<int>.FailureAsync("Mã đã tồn tại. Vui lòng chọn tên khác.");
                }
                var entity = _mapper.Map<ImageEntity>(command);
                entity.CrDateTime = DateTime.Now;
                await _unitOfWork.Repository<ImageEntity>().AddAsync(entity); 
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
