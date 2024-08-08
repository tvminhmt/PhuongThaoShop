using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("BillDetail")]
    public class BillDetailEntity : BaseAuditableEntity
    {
        [Key]
        public int Id { get; set; } 
        public string Code { get; set; }
        public string? CodeProductDetail { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BillEntityId { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
        public int? UpdUserId { get; set; }
        public DateTime? UpdDateTime { get; set; }
        public int Status { get; set; }
		public virtual BillEntity? BillEntity { get; set; }
        public ICollection<SerialEntity>? SerialEntities { get; set; }
    }
}
