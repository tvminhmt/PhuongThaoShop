using AutoMapper;
using MTS.Application.Interfaces.Repositories;
using MTS.Application.Interfaces.Repositories.Identity;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace MTS.Application.Features.IdentityFeatures.SysFunctions.Queries
{
    public record SysFunctionGetMenuByUserQuery : IRequest<Result<List<SysFunctionGetMenuByUserDto>>>
	{
        public int UserId { get; set; } = 0;
        public string UserName { get; set; } = string.Empty;
        public bool GetFavorites { get; set; } = false;
		public SysFunctionGetMenuByUserQuery(int userId) { UserId = userId; }
		public SysFunctionGetMenuByUserQuery(int userId, bool getFavorites) 
        {
            UserId = userId;
            GetFavorites = getFavorites;
		}
		public SysFunctionGetMenuByUserQuery(string userName) { UserName = userName; }
    }

    internal class SysFunctionGetMenuByUserQueryHandler : IRequestHandler<SysFunctionGetMenuByUserQuery, Result<List<SysFunctionGetMenuByUserDto>>>
    {
        private readonly IMapper _mapper;
		private readonly ISysFunctionRepo _repo;
		private readonly IIdentityUnitOfWork _unitOfWork;

		public SysFunctionGetMenuByUserQueryHandler(IIdentityUnitOfWork unitOfWork, ISysFunctionRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
			_unitOfWork = unitOfWork;
		}

        public async Task<Result<List<SysFunctionGetMenuByUserDto>>> Handle(SysFunctionGetMenuByUserQuery query, CancellationToken cancellationToken)
        {
            List<SysFunction> entities = new();
            if (query.UserId > 0) entities = await _repo.GetMenuByUserId(query.UserId);
            else if (!string.IsNullOrEmpty(query.UserName)) entities = await _repo.GetMenuByUserName(query.UserName);

            List<SysFunctionGetMenuByUserDto> sysFunctionGetMenuByUserDtoList = _mapper.Map<List<SysFunctionGetMenuByUserDto>>(entities);

			if (sysFunctionGetMenuByUserDtoList.Count > 0)
            {
				if (query.GetFavorites)
				{
					var sysFunctionUsersByUserList = await _unitOfWork.Repository<SysFunctionUser>().Entities.AsNoTracking()
							.Where(x => x.UserId == query.UserId)
                            .Select(x => new SysFunctionUser
                            {
                                SysFunctionId = x.SysFunctionId,
                                DisplayOrder = x.DisplayOrder
                            })
                            .ToListAsync();

                    SysFunctionUser sysFunctionUser = null;

					if (sysFunctionUsersByUserList != null && sysFunctionUsersByUserList.Any()) 
                    {
						foreach (var item in sysFunctionGetMenuByUserDtoList)
						{
                            sysFunctionUser = sysFunctionUsersByUserList.FirstOrDefault(x => x.SysFunctionId == item.Id);
							if (sysFunctionUser != null)
                            {
                                item.IsFavorite = true;
                                if(sysFunctionUser.DisplayOrder.HasValue)
                                {
									item.DisplayOrder = sysFunctionUser.DisplayOrder.Value;
								}
							}    
					    }
					}
				}
			}
            
            return await Result<List<SysFunctionGetMenuByUserDto>>.SuccessAsync(sysFunctionGetMenuByUserDtoList);
        }
    }
}
