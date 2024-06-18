using System.ComponentModel.DataAnnotations;

namespace PTS.Core.Enums
{
	public enum BillStatusEnum
	{
		[Display(Name = "Đã xóa")]
		Delete = 1,

		[Display(Name = "Hoạt động")]
		Active = 2,

        [Display(Name = "Đang chờ xử lý")]
        Pending = 3,  
        
        [Display(Name = "Chưa thanh toán")]
        Unpaid = 4,     
        
        [Display(Name = "Đã thanh toán")]
        Paid = 5,   

        [Display(Name = "Đang giao hàng")]
        Shipping = 6,    
        
        [Display(Name = "Bị hủy")]
        Cancelled = 7,  
        
        [Display(Name = "Quá hạn")]
        Overdue = 8,   
        
        [Display(Name = "Hoàn thành")]
        Completed = 9,    
    }
}
