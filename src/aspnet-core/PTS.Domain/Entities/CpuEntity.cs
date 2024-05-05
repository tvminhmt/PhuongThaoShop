using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("CPU")]
    public class CpuEntity : BaseEntity
    {
        [MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(100)]
        public string? Ten { get; set; }
        public virtual ICollection<ProductDetailEntity>? ProductDetailEntities { get; set; }
    }
}
