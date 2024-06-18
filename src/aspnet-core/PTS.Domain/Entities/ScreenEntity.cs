using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Screen")]
    public class ScreenEntity : BaseAuditableEntity
	{
        [Key]
		public int Id { get; set; }
		public string Ma { get; set; }
        [MaxLength(50)]
        public string? KichCo { get; set; }
        [MaxLength(50)]
        public string? TanSo { get; set; }
        [MaxLength(50)]
        public string? ChatLieu { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
       
		public virtual ICollection<ProductDetailEntity>? ProductDetailEntities { get; set; }
    }
}
