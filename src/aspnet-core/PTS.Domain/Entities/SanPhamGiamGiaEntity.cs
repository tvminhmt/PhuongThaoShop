using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("SanPhamGiamGia")]
    public class SanPhamGiamGiaEntity : BaseEntity
    {
        [Key]
        public decimal DonGia { get; set; }
        public decimal SoTienConLai { get; set; }
        public int TrangThai { get; set; }
        public int ProductDetailEntityId { get; set; }
        public int GiamGiaEntityId { get; set; }
        public virtual GiamGiaEntity? GiamGia { get; set; }
        public virtual ProductDetailEntity? ProductDetail { get; set; }
    }
}
