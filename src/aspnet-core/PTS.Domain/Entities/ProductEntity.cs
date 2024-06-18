using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Product")]
    public class ProductEntity : BaseAuditableEntity
	{
        [Key]
		public int Id { get; set; }
		[Required]
        [MaxLength(150)]
        public string Name { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
		public int? ManufacturerEntityId { get; set; }
        public int? ProductTypeEntityId { get; set; }
       
        public virtual ManufacturerEntity? ManufacturerEntity { get; set; }
       
        public virtual ProductTypeEntity? ProductTypeEntity { get; set; }
       
        public virtual ICollection<ProductDetailEntity>? ProductDetailEntities { get; set; }
    }
}
