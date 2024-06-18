using PTS.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PTS.Domain.Entities
{
    [Table("Bill")]
    public class BillEntity : BaseAuditableEntity
	{
        [Key]
		public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string? InvoiceCode { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        [MaxLength(150)]
        public string? FullName { get; set; }
        [MaxLength(150)]
        public string? Address { get; set; }
        public int Payment { get; set; }
        //1 thanh toán tại cửa hàng 2 thanh toán khi nhận hàng 3 thanh toán bằng chuyển khoản ngân hàng
        public bool IsPayment { get; set; }
        public decimal? Discount { get; set; }
        public int? VoucherEntityId { get; set; }
        public int? UserEntityId { get; set; }
		public int? CrUserId { get; set; }
		public DateTime? CrDateTime { get; set; }
		public int Status { get; set; }
       
		public virtual VoucherEntity? VoucherEntitity { get; set; }
       
        public virtual UserEntity? UserEntity { get; set; }
       
        public virtual ICollection<BillDetailEntity>? BillDetailEntities { get; set; }
    }
}
