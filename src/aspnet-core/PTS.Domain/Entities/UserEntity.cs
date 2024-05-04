using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace PTS.Domain.Entities
{
    [Table("User")]
    public class UserEntity : BaseEntity
    {
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public virtual CartEntity? Cart { get; set; }
        public int? RoleEntityId{ get; set; }
        public virtual RoleEntity? RoleEntities { get; set; }
    }
}
