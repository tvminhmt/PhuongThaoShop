using MTS.Application.Common.Mappings;
using MTS.Domain.Entities.Identity;

namespace MTS.Application.Features.IdentityFeatures.Users.Queries
{
    public class UserGetByIdDto : UserDto, IMapFrom<User>
    {
		public List<int> RolesByUserList { get; set; }
	}
}
