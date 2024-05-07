using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("HardDrive")]
    public class HardDriveEntity : BaseEntity
    {
        [MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(100)]
        public string? ThongSo { get; set; }
        public virtual ICollection<ProductDetailEntity>? ProductDetailEntities { get; set; }
    }
}
