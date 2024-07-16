using MTS.Application.Common.Mappings;
using MTS.Domain.Entities.Identity;

namespace MTS.Application.Features.IdentityFeatures.Roles.Queries
{
    public class RoleGetByIdDto : RoleDto, IMapFrom<Role>
    {
		public List<int> SysFunctionsByRoleList { get; set; }
	}
}
