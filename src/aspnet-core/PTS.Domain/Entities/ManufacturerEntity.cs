using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Manufacturer")]
    public class ManufacturerEntity : BaseAuditableEntity
	{
		[Key]
		public int Id { get; set; }
		[Required]
        [MaxLength(150)]
        public string? Name { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
		[NotMapped]
		public virtual ICollection<ProductEntity>? ProductEntities { get; set; }
    }
}
