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
    public record ImageEditCommand : IRequest<Result<int>>, IMapFrom<ImageEntity>
    {
		public int Id { get; set; }
		[MaxLength(50)]
		public string Url { get; set; }
		[MaxLength(200)]
		public string? TenImage { get; set; }
		public DateTime? StartDay { get; set; }
		public DateTime? EndDay { get; set; }
		public decimal GiaTri { get; set; }
		public int SoLuong { get; set; }
		public int Status { get; set; }
	}

    internal class ImageEditCommandHandler : IRequestHandler<ImageEditCommand, Result<int>>
    {
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public ImageEditCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Result<int>> Handle(ImageEditCommand command, CancellationToken cancellationToken)
        {
			try
			{
				var entity = await _unitOfWork.Repository<ImageEntity>().GetByIdAsync(command.Id);
                if (entity == null)
                {
                    return await Result<int>.FailureAsync($"Id <b>{command.Id}</b> không tồn tại ");
                }
                if (command.Url != entity.Url)
                {
                    var existing = await _unitOfWork.Repository<ImageEntity>().Entities.AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Url== command.Url, cancellationToken);
                    if (existing != null)
                    {
                        return await Result<int>.FailureAsync("Mã đã tồn tại. Vui lòng chọn tên khác.");
                    }
                }
               
				entity = _mapper.Map<ImageEntity>(command);
				await _unitOfWork.Repository<ImageEntity>().UpdateFieldsAsync(entity,
					x => x.Url,
					x => x.Status);
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
