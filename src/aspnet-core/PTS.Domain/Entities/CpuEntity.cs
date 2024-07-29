using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("CPU")]
    public class CpuEntity : BaseAuditableEntity
	{
		[Key]
		public int Id { get; set; }
		[MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(100)]
        public string? Ten { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
		[NotMapped]
		public virtual ICollection<ProductDetailEntity>? ProductDetailEntities { get; set; }
    }
}
