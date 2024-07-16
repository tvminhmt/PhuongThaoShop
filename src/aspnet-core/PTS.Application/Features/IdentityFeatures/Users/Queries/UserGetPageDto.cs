using MTS.Application.Common.Mappings;
using MTS.Application.Features.IdentityFeatures.Roles.Queries;
using MTS.Domain.Entities.Identity;

namespace MTS.Application.Features.IdentityFeatures.Users.Queries
{
    public class UserGetPageDto : UserDto, IMapFrom<User>
    {
        public List<RoleDto> Roles { get; set; }

        public string GetRoleString()
        {
            string roles = "";
            if (Roles != null)
            {
                foreach (var item in Roles)
                {
                    if (roles != "") roles += " ";
                    roles += "<span class=\"badge bg-success\">" + item.Name + "</span>";
                }
            }
            return roles;
        }
    }
}
