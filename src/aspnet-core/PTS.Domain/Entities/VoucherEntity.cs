
using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Voucher")]
    public class VoucherEntity : BaseAuditableEntity
    {
		public int Id { get; set; }
		[MaxLength(50)]
        public string MaVoucher { get; set; }
        [MaxLength(200)]
        public string? TenVoucher { get; set; }
        public DateTime? StartDay { get; set; }
        public DateTime? EndDay { get; set; }
        public decimal GiaTri { get; set; }
        public int SoLuong { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
		[NotMapped]
        public virtual ICollection<BillEntity>? BillEntities { get; set; }
    }
}
