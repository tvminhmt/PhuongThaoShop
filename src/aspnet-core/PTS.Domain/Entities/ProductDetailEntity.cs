using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("ProductDetail")]
    public class ProductDetailEntity : BaseEntity
    {
      
        [Required]
        [MaxLength(50)]
        public string? Code { get; set; }
        public Decimal Price { get; set; }
        public Decimal OldPrice { get; set; }
        public string? Upgrade { get; set; }
        public string? Description { get; set; }
        public int ProductEntityId { get; set; }
        public int? ColorEntityId { get; set; }
        public int? RamEntityId { get; set; }
        public int? CpuEntityId { get; set; }
        public int? HardDriveEntityId { get; set; }
        public int? ScreenEntityId { get; set; }
        public int? CardVGAEntityId { get; set; }
        public virtual ColorEntity? ColorEntity { get; set; }
        public virtual RamEntity? RamEntity { get; set; }
        public virtual CpuEntity? CpuEntity { get; set; }
        public virtual ScreenEntity? ScreenEntity { get; set; }
        public virtual CardVGAEntity? CardVGAEntity { get; set; }
        public virtual HardDriveEntity? HardDriveEntity { get; set; }
        public virtual ProductEntity? ProductEntity { get; set; }
        public ICollection<ImageEntity>? ImageEntities { get; set; }
        public ICollection<CartDetailEntity>? CartDetailEntities { get; set; }
        public ICollection<SerialEntity>? SerialEntities { get; set; }
    }
}
