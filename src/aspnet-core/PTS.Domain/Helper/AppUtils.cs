
using PTS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace PTS.Application.Helper
{
    public static class AppUtils
    {
        public static string GetPaymentEnumName(this PaymentEnum paymentEnum)
        {
            var type = paymentEnum.GetType();
            var memberInfo = type.GetMember(paymentEnum.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    return ((DisplayAttribute)attributes[0]).Name;
                }
            }
            return paymentEnum.ToString();
        }
        public static string GetPayment(int value)
        {
            if (Enum.IsDefined(typeof(PaymentEnum), value))
            {
                var paymentEnum = (PaymentEnum)value;
                return paymentEnum.GetPaymentEnumName();
            }
            return "Không xác định";
        }

    }
}
