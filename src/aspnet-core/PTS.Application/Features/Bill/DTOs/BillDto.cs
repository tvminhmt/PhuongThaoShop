using PTS.Application.Common.Mappings;
using PTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTS.Application.Features.Bill.DTOs
{
    public class BillDto : IMapFrom<BillEntity>
    {
        public int Id { get; set; }
        public string? InvoiceCode { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
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
    }
}
