using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Voucher")]
    public class VoucherEntity : BaseEntity
    {
       
        [MaxLength(50)]
        public string? MaVoucher { get; set; }
        [MaxLength(150)]
        public string? TenVoucher { get; set; }
        public DateTime? StarDay { get; set; }
        public DateTime? EndDay { get; set; }
        public decimal GiaTri { get; set; }
        public int SoLuong { get; set; }
        public virtual ICollection<BillEntity>? BillEntities { get; set; }
    }
}
