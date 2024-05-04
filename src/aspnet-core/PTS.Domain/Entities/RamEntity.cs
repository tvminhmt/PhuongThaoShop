using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Ram")]
    public class RamEntity : BaseEntity
    {
        public string? Ma { get; set; }
        [MaxLength(100)]
        public string? ThongSo { get; set; }
        public virtual ICollection<ProductDetailEntity>? ProductDetailEntities { get; set; }
    }
}
