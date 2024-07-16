using AutoMapper;
using AutoMapper.QueryableExtensions;
using MTS.Application.Interfaces.Repositories;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Queries
{
	public record SysFunctionGetByParentQuery : IRequest<Result<List<SysFunctionGetByParentDto>>>
	{
        public int ParentItemId { get; set; }

		public bool HasChild { get; set; }
	}

	internal class SysFunctionGetByParentQueryHandler : IRequestHandler<SysFunctionGetByParentQuery, Result<List<SysFunctionGetByParentDto>>>
	{
		private readonly IIdentityUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public SysFunctionGetByParentQueryHandler(IIdentityUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Result<List<SysFunctionGetByParentDto>>> Handle(SysFunctionGetByParentQuery queryInput, CancellationToken cancellationToken)
		{
			var query = _unitOfWork.Repository<SysFunction>().Entities.AsNoTracking();

            if (queryInput.HasChild)
            {
                query = query.Where(x => x.ParentItemId == 0 || x.HasChild.HasValue && x.HasChild.Value);
            }

            if (queryInput.ParentItemId > 0)
            {
				query = query.Where(x => x.ParentItemId == queryInput.ParentItemId);
            }

            var sysParentFunctionsList = await query
                   .OrderBy(x => x.TreeOrder)
				   .ProjectTo<SysFunctionGetByParentDto>(_mapper.ConfigurationProvider)
				   .ToListAsync(cancellationToken);

			return await Result<List<SysFunctionGetByParentDto>>.SuccessAsync(sysParentFunctionsList);
		}
	}
}
