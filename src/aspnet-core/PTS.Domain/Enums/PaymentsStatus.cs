using System.ComponentModel.DataAnnotations;

namespace PTS.Core.Enums
{
	public enum PaymentEnum
	{
        [Display(Name = "Thanh toán tiền mặt khi nhận hàng")]
        CashOnDelivery = 1,

        [Display(Name = "Chuyển khoản ngân hàng")]
        BankTransfer = 2,

        [Display(Name = "Thẻ tín dụng/thẻ ghi nợ")]
        CreditDebitCard = 3,

        [Display(Name = "Ví điện tử")]
        EWallet = 4,

        [Display(Name = "Thanh toán qua PayPal")]
        PayPal = 5,

        [Display(Name = "Trả góp")]
        Installment = 6,
    }
}
