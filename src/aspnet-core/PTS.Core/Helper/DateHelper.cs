using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.Shared.Helper
{
    public static class DateHelper
    {
        public static DateTime SetZeroHour(this DateTime d)
        {
            return new DateTime(d.Year, d.Month, d.Day, 0, 0, 0);
        }

        public static DateTime SetTimeFromStringHHmm(this DateTime d, string timeHHmm)
        {
            if (string.IsNullOrEmpty(timeHHmm)) return d;
            var splGio = timeHHmm.Split(':');
            if (splGio.Length == 2)
            {
                return new DateTime(
                    d.Year,
                    d.Month,
                    d.Day,
                    Convert.ToInt32(splGio[0]),
                    Convert.ToInt32(splGio[1]),
                    0);
            }
            return d;
        }
        public static DateTime? SetTimeFromStringHHmm(this DateTime? d, string timeHHmm)
        {
            if (!d.HasValue)
            {
                return null;
            }
            if (string.IsNullOrEmpty(timeHHmm))
            {
                return d;
            }

            var splGio = timeHHmm.Split(':');
            if (splGio.Length == 2)
            {
                return new DateTime(
                    d.Value.Year,
                    d.Value.Month,
                    d.Value.Day,
                    Convert.ToInt32(splGio[0]),
                    Convert.ToInt32(splGio[1]),
                    0);
            }
            return d;

        }

        public static string DocNgayGioThangNam(this DateTime d)
        {
            var ret = new StringBuilder();
            ret.Append($@"{d:HH} giờ ");
            ret.Append($@"{d:mm} phút, ");
            ret.Append($@"ngày {d:dd} ");
            ret.Append($@"tháng {d:MM} ");
            ret.Append($@"năm {d:yyyy} ");
            return ret.ToString();
        }
        /// <summary>
        /// Convert Date to string dd/MM/yyyy
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string FormatIn(this DateTime d)
        {
            return d.ToString("dd/MM/yyyy");
        }
        /// <summary>
        /// Convert Date to string dd/MM/yyyy
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string FormatIn(this DateTime? d)
        {
            return d.HasValue ? d.Value.FormatIn() : String.Empty;
        }
        /// <summary>
        /// Convert date to định dạng yyyy-MM-dd cho query sql
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToStringSql(this DateTime? d)
        {
            return d.HasValue ? d.Value.ToStringSql() : string.Empty;
        }
        public static string ToStringSql(this DateTime d)
        {
            return d.ToString("yyyy-MM-dd");
        }
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            {
                yield return day.SetZeroHour().AddHours(12);
            }
        }

        public static string QueryTuNgay(this DateTime? d ,string field)
        {
            return !d.HasValue ? string.Empty : $@" and  DATE({field}) >= '{d.ToStringSql()}' ";
        }
        public static string QueryDenNgay(this DateTime? d, string field)
        {
            return !d.HasValue ? string.Empty : $@" and  DATE({field}) <= '{d.ToStringSql()}' ";
        }

        public static string ToStringLienThongBaoHiem(this DateTime? d)
        {
            return d.HasValue ? d.Value.ToStringLienThongBaoHiem() : string.Empty;
        }
        public static string ToStringLienThongBaoHiem(this DateTime d)
        {
            return d.ToString("yyyyMMdd");
        }
    }
}
