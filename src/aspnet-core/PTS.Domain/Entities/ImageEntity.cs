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
		public string? Ma { get; set; }
        [MaxLength(150)]
        public string? LinkImage { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
		public int? ProductDetailEntityId { get; set; }
		[NotMapped]
        public virtual ProductDetailEntity? ProductDetailEntity { get; set; }
    }
}
