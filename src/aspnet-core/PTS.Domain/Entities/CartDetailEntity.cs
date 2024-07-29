using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("CartDetail")]
    public class CartDetailEntity : BaseAuditableEntity
	{
        [Key]
        public int Id { get; set; }
        public int CartEntityId { get; set; }
        public int ProductDetailEntityId { get; set; }
        public int Quantity { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
       
		public virtual CartEntity? CartEntity { get; set; }
       
        public virtual ProductDetailEntity? ProductDetailEntity { get; set; }


    }
}
