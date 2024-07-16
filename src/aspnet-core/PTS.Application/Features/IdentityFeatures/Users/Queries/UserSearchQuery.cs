using MTS.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.Application.Features.IdentityFeatures.Users.Queries
{
    public class UserSearchQuery  : IRequest<List<UserSearchDto>>
    {
        public string KeyWords { get; set; }
    }
    internal class UserSearchQueryHandler : IRequestHandler<UserSearchQuery, List<UserSearchDto>>
    {
        private readonly UserManager<User> _userManager;
        public UserSearchQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<List<UserSearchDto>> Handle(UserSearchQuery queryInput, CancellationToken cancellationToken)
        {
            List<UserSearchDto> result = new List<UserSearchDto>();
            var query = _userManager.Users;
            if (!string.IsNullOrEmpty(queryInput.KeyWords))
            {
                query = query.Where(x => x.UserName.Contains(queryInput.KeyWords) || x.FullName.Contains(queryInput.KeyWords));
            }
            var data = await query.ToListAsync(cancellationToken);
            if (data != null && data.Any())
            {
                foreach (var item in data)
                {
                    result.Add(new UserSearchDto
                    {
                        Id = item.Id,
                        UserName = item.UserName
                    });
                }
            }
            return result;
        }
    }
}
