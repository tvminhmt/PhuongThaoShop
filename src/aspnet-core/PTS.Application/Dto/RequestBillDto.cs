﻿namespace PTS.Application.Dto
{
    public class RequestBillDto
    {
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? UserName { get; set; }
        public string? CodeVoucher { get; set; }
        public int Payment { get; set; }
        public List<CartItemDto>? CartItem { get; set; }
    }
}
