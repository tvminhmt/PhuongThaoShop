using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("GiamGia")]
    public class GiamGiaEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Ma { get; set; }
        [MaxLength(150)]
        public string? Ten { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public decimal MucGiamGiaPhanTram { get; set; }
        public decimal MucGiamGiaTienMat { get; set; }
        public string? LoaiGiamGia { get; set; }
        public int TrangThai { get; set; }
        public virtual ICollection<SanPhamGiamGiaEntity>? SanPhamGiamGias { get; set; }
    }
}
