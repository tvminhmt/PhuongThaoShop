using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Color")]
    public class ColorEntity : BaseAuditableEntity
	{
		[Key]
		public int Id { get; set; }
		public string Ma { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
		public virtual ICollection<ProductDetailEntity>? ProductDetailEntities { get; set; }
    }
}
