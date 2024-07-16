using AutoMapper;
using AutoMapper.QueryableExtensions;
using MTS.Domain.Entities.Identity;
using IC.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace MTS.Application.Features.IdentityFeatures.Users.Queries
{
    public class UserGetAllQuery : IRequest<Result<List<UserGetAllDto>>>
    {
        public bool? IsEnabled { get; set; } = true;
	}
    internal class UserGetAllQueryHandler : IRequestHandler<UserGetAllQuery, Result<List<UserGetAllDto>>>
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UserGetAllQueryHandler(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }
        public async Task<Result<List<UserGetAllDto>>> Handle(UserGetAllQuery request, CancellationToken cancellationToken)
        {
            var query = _userManager.Users.AsNoTracking();

            if (request.IsEnabled.HasValue)
            {
                query = query.Where(x => x.IsEnabled == request.IsEnabled);
			}

			var result = await query
				.OrderByDescending(x => x.Id)
                .ProjectTo<UserGetAllDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return await Result<List<UserGetAllDto>>.SuccessAsync(result);
        }
    }
}
