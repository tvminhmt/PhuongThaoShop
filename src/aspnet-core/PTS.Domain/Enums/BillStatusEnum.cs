using System.ComponentModel.DataAnnotations;

namespace PTS.Core.Enums
{
	public enum BillStatusEnum
	{
		[Display(Name = "Đã xóa")]
		Delete = 0,
		[Display(Name = "Chờ xác nhận")]
		Active = 2,
        [Display(Name = "Chờ gửi hàng")]
        Pending = 3, 
        [Display(Name = "Đang giao hàng")]
        Shipping = 4,
        [Display(Name = "Giao hàng thành công")]
        Shipping1 = 5,
        [Display(Name = "Giao hàng thất bại")]
        Shipping2 = 6,
        [Display(Name = "Đã hủy")]
        Cancelled = 7,  
        [Display(Name = "Hoàn thành")]
        Completed = 8,    
    }
}
