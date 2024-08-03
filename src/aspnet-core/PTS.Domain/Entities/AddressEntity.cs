
using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Address")]
    public class AddressEntity : BaseAuditableEntity
    {
        [Key]
		public int AddressId { get; set; }
        public string? AddressName { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
        public int? UserEntityId { get; set; }
        public virtual UserEntity? UserEntity { get; set; }
    }
}
