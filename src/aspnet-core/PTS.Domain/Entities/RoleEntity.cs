
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Role")]
    public class RoleEntity: BaseEntity
    {
        public string? RoleCode { get; set; }
        public string? RoleName { get; set; }
        public virtual ICollection<UserEntity>? UserEntities { get; set; }
    }
}
