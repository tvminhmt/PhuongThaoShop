using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Features.Bill.DTOs
{
    public class PBillGetByCodeQueryDto
    {
        public string InvoiceCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? CodeVoucher { get; set; }
        public decimal? GiamGia { get; set; }
        public int Payment { get; set; }
        public string PaymentStatus { get; set; }
        public bool IsPayment { get; set; }
        public string? StringPayment { get; set; }
        public int? UserId { get; set; }
        public int? Status { get; set; }
        public object? BillDetail { get; set; }
        public int Count { get; set; } = 0;
        public decimal? Total { get; set; }
        public decimal? IntoMoney { get; set; } 
    }
}
