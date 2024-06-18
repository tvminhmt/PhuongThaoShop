using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Serial")]
    public class SerialEntity : BaseAuditableEntity
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string SerialNumber { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
		public int? ProductDetailEntityId {  get; set; }
        public int? BillDetailEntityId { get; set; }
       
        public virtual ProductDetailEntity? ProductDetailEntities { get; set; }
       
        public virtual BillDetailEntity? BillDetailEntities { get; set; }
    }
}
