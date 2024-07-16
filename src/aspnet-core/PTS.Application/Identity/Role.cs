using Microsoft.AspNetCore.Identity;

namespace MTS.Domain.Entities.Identity
{
    public class Role : IdentityRole<int>
    {
        public override int Id { get; set; }
        public string Description { get; set; }
    }
}
