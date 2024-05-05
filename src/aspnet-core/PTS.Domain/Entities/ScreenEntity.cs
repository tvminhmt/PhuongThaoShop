using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Screen")]
    public class ScreenEntity : BaseEntity
    {
        public string? Ma { get; set; }
        [MaxLength(50)]
        public string? KichCo { get; set; }
        [MaxLength(50)]
        public string? TanSo { get; set; }
        [MaxLength(50)]
        public string? ChatLieu { get; set; }
        public virtual ICollection<ProductDetailEntity>? ProductDetailEntities { get; set; }
    }
}
