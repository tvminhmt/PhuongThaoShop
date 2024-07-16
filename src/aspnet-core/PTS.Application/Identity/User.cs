using MTS.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace MTS.Domain.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDay { get; set; }
        public int? DefaultActionId { get; set; }
        public string Notes { get; set; }
        public string AvatarPath { get; set; }
        public bool? IsEnabled { get; set; }
        public DateTime? LastTimeChangePass { get; set; }
        public DateTime? LastTimeLogin { get; set; }
        public DateTime? CrDateTime { get; set; }
    }
}
