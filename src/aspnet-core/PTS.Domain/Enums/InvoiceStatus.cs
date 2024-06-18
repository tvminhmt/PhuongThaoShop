using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Core.Enums
{
    public enum InvoiceStatus
    { 
        Pending =100,       // Đang chờ xử lý
        Unpaid,        // Chưa thanh toán
        Paid,          // Đã thanh toán
        Shipping,       // Đang giao hàng
        Cancelled,     // Bị hủy
        Overdue,       // Quá hạn
        Completed,     // Hoàn thành
        
    }
}
