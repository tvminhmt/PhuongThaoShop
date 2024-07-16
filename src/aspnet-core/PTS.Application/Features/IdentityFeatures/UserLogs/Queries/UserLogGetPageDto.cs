using MTS.Application.Common.Mappings;
using MTS.Domain.Entities.Identity;

namespace MTS.Application.Features.IdentityFeatures.UserLogs.Queries
{
    public class UserLogGetPageDto : IMapFrom<UserLog>
    {
        public string UserName { get; set; }
        public string FromIP { get; set; }
        public string UserAction { get; set; }
        public string ActionStatus { get; set; }
        public DateTime CrDateTime { get; set; }
    }
}
