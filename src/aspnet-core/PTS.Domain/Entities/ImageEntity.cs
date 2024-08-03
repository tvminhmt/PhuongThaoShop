using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Image")]
    public class ImageEntity : BaseAuditableEntity
	{
		[Key]
		public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
        public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
        public virtual ICollection<ProductDetailImage> ProductDetailImages { get; set; }
    }
}
