using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Cart")]
    public class CartEntity : BaseAuditableEntity
	{
        [Key]
        public int UserEntityId { get; set; }
        [MaxLength(200)]
        public string? Description { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
       
		public virtual UserEntity? UserEntity { get; set; }
       
        public virtual ICollection<CartDetailEntity>? CartDetailEntities { get; set; }
    }
}
