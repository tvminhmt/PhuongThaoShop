using System;
using System.Globalization;
using System.Text;

namespace PTS.Shared.Helper
{
    public static class MoneyExtensions
    {
        // Hàm đọc số thành chữ tiếng việt
        private static string ReadVi(decimal number, string currencyName)
        {
            if (number == 0)
                return "không " + currencyName;

            var lsoChu = "";
            var tachMod = "";
            var tachConlai = "";
            var num = Math.Floor(number);
            var gN = Convert.ToString(num, CultureInfo.InvariantCulture);
            var m = Convert.ToInt32(gN.Length / 3);
            var mod = gN.Length - m * 3;
            var dau = "[+]";

            // Dau [+ , - ]
            if (number < 0)
                dau = "[-]";
            dau = "";

            // Tach hang lon nhat
            if (mod.Equals(1))
                tachMod = "00" + Convert.ToString(num.ToString().Trim().Substring(0, 1)).Trim();
            if (mod.Equals(2))
                tachMod = "0" + Convert.ToString(num.ToString().Trim().Substring(0, 2)).Trim();
            if (mod.Equals(0))
                tachMod = "000";
            // Tach hang con lai sau mod :
            if (num.ToString().Length > 2)
                tachConlai = Convert.ToString(num.ToString().Trim().Substring(mod, num.ToString().Length - mod)).Trim();

            //don vi hang mod
            var im = m + 1;
            if (mod > 0)
                lsoChu = Tach(tachMod).Trim() + " " + Donvi(im.ToString().Trim());
            // Tach 3 trong tach_conlai

            var i = m;
            var _m = m;
            var j = 1;
            var tach3 = "";
            var tach3_ = "";

            while (i > 0)
            {
                tach3 = tachConlai.Trim().Substring(0, 3).Trim();
                tach3_ = tach3;
                lsoChu = lsoChu.Trim() + " " + Tach(tach3.Trim()).Trim();
                m = _m + 1 - j;
                if (!tach3_.Equals("000"))
                    lsoChu = lsoChu.Trim() + " " + Donvi(m.ToString().Trim()).Trim();
                tachConlai = tachConlai.Trim().Substring(3, tachConlai.Trim().Length - 3);

                i = i - 1;
                j = j + 1;
            }

            if (lsoChu.Trim().Substring(0, 1).Equals("k"))
                lsoChu = lsoChu.Trim().Substring(10, lsoChu.Trim().Length - 10).Trim();
            if (lsoChu.Trim().Substring(0, 1).Equals("l"))
                lsoChu = lsoChu.Trim().Substring(4, lsoChu.Trim().Length - 4).Trim();
            if (lsoChu.Trim().Length > 0)
                lsoChu = dau.Trim() + " " + lsoChu.Trim().Substring(0, 1).Trim().ToUpper() +
                         lsoChu.Trim().Substring(1, lsoChu.Trim().Length - 1).Trim();

            return lsoChu.Trim() + " " + currencyName;
        }

        private static string Chu(string gNumber)
        {
            var result = "";
            switch (gNumber)
            {
                case "0":
                    result = "không";
                    break;
                case "1":
                    result = "một";
                    break;
                case "2":
                    result = "hai";
                    break;
                case "3":
                    result = "ba";
                    break;
                case "4":
                    result = "bốn";
                    break;
                case "5":
                    result = "năm";
                    break;
                case "6":
                    result = "sáu";
                    break;
                case "7":
                    result = "bảy";
                    break;
                case "8":
                    result = "tám";
                    break;
                case "9":
                    result = "chín";
                    break;
            }

            return result;
        }

        private static string Donvi(string so)
        {
            var Kdonvi = "";

            if (so.Equals("1"))
                Kdonvi = "";
            if (so.Equals("2"))
                Kdonvi = "nghìn";
            if (so.Equals("3"))
                Kdonvi = "triệu";
            if (so.Equals("4"))
                Kdonvi = "tỷ";
            if (so.Equals("5"))
                Kdonvi = "nghìn tỷ";
            if (so.Equals("6"))
                Kdonvi = "triệu tỷ";
            if (so.Equals("7"))
                Kdonvi = "tỷ tỷ";

            return Kdonvi;
        }

        private static string Tach(string tach3)
        {
            var Ktach = "";
            if (tach3.Equals("000"))
                return "";
            if (tach3.Length == 3)
            {
                var tr = tach3.Trim().Substring(0, 1).Trim();
                var ch = tach3.Trim().Substring(1, 1).Trim();
                var dv = tach3.Trim().Substring(2, 1).Trim();
                if (tr.Equals("0") && ch.Equals("0"))
                    Ktach = " không trăm linh " + Chu(dv.Trim()) + " ";
                if (!tr.Equals("0") && ch.Equals("0") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm ";
                if (!tr.Equals("0") && ch.Equals("0") && !dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm linh " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (tr.Equals("0") && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = " không trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (tr.Equals("0") && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = " không trăm mười " + Chu(dv.Trim()).Trim() + " ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("0"))
                    Ktach = " không trăm mười ";
                if (tr.Equals("0") && ch.Equals("1") && dv.Equals("5"))
                    Ktach = " không trăm mười lăm ";
                if (Convert.ToInt32(ch) > 1 && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                {
                    if (dv.Trim() != "1")
                        Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi " +
                                Chu(dv.Trim()).Trim() + " ";
                    else
                        Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi mốt ";
                }

                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi ";
                if (Convert.ToInt32(tr) > 0 && Convert.ToInt32(ch) > 1 && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm " + Chu(ch.Trim()).Trim() + " mươi lăm ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && Convert.ToInt32(dv) > 0 && !dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười " + Chu(dv.Trim()).Trim() + " ";

                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("0"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười ";
                if (Convert.ToInt32(tr) > 0 && ch.Equals("1") && dv.Equals("5"))
                    Ktach = Chu(tr.Trim()).Trim() + " trăm mười lăm ";
            }


            return Ktach;
        }

        // Hàm đọc số thành chữ tiếng anh
        private static string ReadEng(decimal number, string currencyName)
        {
            //long inputNum;
            decimal inputNum;
            int dig1, dig2, dig3, level = 0, lasttwo, threeDigits;

            string dollars, cents;
            try
            {
                var Splits = new string[2];
                Splits = number.ToString().Split('.'); //notice that it is ' and not "
                //inputNum = Convert.ToInt64(Splits[0]);
                inputNum = decimal.Parse(Splits[0]);
                dollars = "";
                cents = Splits[1];
                if (cents.Length == 1) cents += "0"; // 12.5 is twelve and 50/100, not twelve and 5/100
            }
            catch
            {
                cents = "00";
                //inputNum = Convert.ToInt64(number);
                inputNum = number;
                dollars = "";
            }

            var x = "";

            //they had zero for ones and tens but that gave ninety zero for 90
            string[] ones =
            {
                "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve",
                "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
            };
            string[] tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
            string[] thou = { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion" };

            var isNegative = false;
            if (inputNum < 0)
            {
                isNegative = true;
                inputNum *= -1;
            }

            if (inputNum == 0)
                //return "zero and " + cents + "/100";
                return "zero " + currencyName;

            var s = inputNum.ToString();

            while (s.Length > 0)
            {
                //Get the three rightmost characters
                x = s.Length < 3 ? s : s.Substring(s.Length - 3, 3);

                // Separate the three digits
                threeDigits = int.Parse(x);
                lasttwo = threeDigits % 100;
                dig1 = threeDigits / 100;
                dig2 = lasttwo / 10;
                dig3 = threeDigits % 10;

                // append a "thousand" where appropriate
                if (level > 0 && dig1 + dig2 + dig3 > 0)
                {
                    dollars = thou[level] + " " + dollars;
                    dollars = dollars.Trim();
                }

                // check that the last two digits is not a zero
                if (lasttwo > 0)
                {
                    if (lasttwo < 20)
                    {
                        // if less than 20, use "ones" only
                        dollars = ones[lasttwo] + " " + dollars;
                    }
                    else
                    {
                        // otherwise, use both "tens" and "ones" array
                        //dollars = tens[dig2] + " " + ones[dig3] + " " + dollars;
                        if (dig3 == 0)
                            dollars = tens[dig2] + " " + dollars;
                        else
                            dollars = tens[dig2] + "-" + ones[dig3] + " " + dollars;
                    }

                    if (s.Length < 3)
                    {
                        if (isNegative) dollars = "negative " + dollars;
                        //return dollars + " and " + cents + "/100";
                        return dollars[0].ToString().ToUpper() + dollars.Substring(1, dollars.Length - 1).Trim() + " " +
                               currencyName;
                    }
                }

                // if a hundreds part is there, translate it
                if (dig1 > 0)
                {
                    dollars = ones[dig1] + " hundred " + dollars;
                    s = s.Length - 3 > 0 ? s.Substring(0, s.Length - 3) : "";
                    level++;
                }
                else
                {
                    if (s.Length > 3)
                    {
                        s = s.Substring(0, s.Length - 3);
                        level++;
                    }
                }
            }

            if (isNegative) dollars = "negative " + dollars;
            //return dollars + " and " + cents + "/100";
            return dollars[0].ToString().ToUpper() + dollars.Substring(1, dollars.Length - 1).Trim() + " " +
                   currencyName;
        }

        /// <summary>
        ///     đọc tiền bằng tiếng việt
        /// </summary>
        /// <param name="currency"> số tiền cần đọc</param>
        /// <param name="currencyNameVi"> cách đơn loại tiền tệ PHẦN NGUYÊN</param>
        /// <param name="minimumNameVi"> cách đọc tiền tệ phần thập phân</param>
        /// <param name="rouding">số số thập phân sau dấu phẩy</param>
        /// <param name="conversion">chuyển đổi đơn vị của tiền tệ từ đơn vị chính sang đơn vị bé nhất</param>
        /// <returns></returns>
        public static string ReadMoneyVi(decimal currency, string currencyNameVi, string minimumNameVi, int rouding,
            int conversion)
        {
            var number = Math.Abs(currency);
            var nguyen = Math.Floor(number);

            var thapPhan = number - nguyen;

            var nguyenChu = "";

            //trường hợp tiền tệ tổng k để thập phân hoặc phần thập phân = 0 hoặc sau khi nhân với số chuyển đổi số tiền thập phân dạng 0.abc < 1
            if (rouding == 0 || thapPhan == 0 || thapPhan * conversion < 1)
            {
                nguyen = Math.Round(number, MidpointRounding.AwayFromZero);
                nguyenChu = ReadVi(nguyen, currencyNameVi);
                return nguyenChu;
            }

            //trường hợp số lẻ thập phân != 0 => đọc theo cách chuyển đổi. 
            //vd thapphan = 0.001 mà conversion = 10 => doc thap phan = 0
            //nếu số tiền = 0 thì đọc đơn vị to, 0.abc thì không đọc phần tiền to
            //nếu giá trị tiền tệ nhỏ * giá trị chuyển đổi rồi làm tròn lên = giá trị chuyển đổi (tức là = 1 đơn vị tiền lớn)

            thapPhan = Math.Round(thapPhan * conversion, MidpointRounding.AwayFromZero);

            if (thapPhan == conversion)
            {
                //thì cộng đơn vị tiền lớn lên 1 và k đọc đơn vị tiền nhỏ
                nguyen++;
                return ReadVi(nguyen, currencyNameVi);
            }

            var thapPhanChu = ReadVi(thapPhan, minimumNameVi);
            if (nguyen == 0)
                return thapPhanChu.Trim();

            nguyenChu = ReadVi(nguyen, currencyNameVi);
            return nguyenChu.Trim() + " và " + thapPhanChu.ToLower().Trim();

            //var thapPhanChu = ReadVI(thapPhan, minimumNameVi);
            //if (nguyen == 0)
            //    return thapPhanChu.Trim();

            //nguyenChu = ReadVI(nguyen, currencyNameVi);
            //return nguyenChu.Trim() + " và " + thapPhanChu.ToLower().Trim();
        }

        /// <summary>
        ///     ddojc tiền tiếng anh
        /// </summary>
        /// <param name="currency"></param>
        /// <param name="currencyNameEn"></param>
        /// <param name="minimumNameEn"></param>
        /// <param name="rouding"></param>
        /// <param name="conversion"></param>
        /// <returns></returns>
        public static string ReadMoneyEn(decimal currency, string currencyNameEn, string minimumNameEn, int rouding,
            int conversion)
        {
            var number = Math.Abs(currency);
            var nguyen = Math.Floor(number);

            var thapPhan = number - nguyen;

            var nguyenChu = "";
            //trường hợp tiền tệ tổng k để thập phân hoặc phần thập phân = 0 hoặc sau khi nhân với số chuyển đổi số tiền thập phân dạng 0.abc < 1
            if (rouding == 0 || thapPhan == 0 || thapPhan * conversion <= 1)
            {
                nguyen = Math.Round(number, MidpointRounding.AwayFromZero);
                nguyenChu = ReadEng(nguyen, currencyNameEn);
                return nguyenChu;
            }

            //trường hợp số lẻ thập phân != 0 => đọc theo cách chuyển đổi. 
            //vd thapphan = 0.001 mà conversion = 10 => doc thap phan = 0
            //nếu số tiền = 0 thì đọc đơn vị to, 0.abc thì không đọc phần tiền to
            thapPhan = Math.Round(thapPhan * conversion, MidpointRounding.AwayFromZero);

            //nếu giá trị tiền tệ nhỏ * giá trị chuyển đổi rồi làm tròn lên = giá trị chuyển đổi (tức là = 1 đơn vị tiền lớn)
            if (thapPhan == conversion)
            {
                //thì cộng đơn vị tiền lớn lên 1 và k đọc đơn vị tiền nhỏ
                nguyen++;
                return ReadEng(nguyen, currencyNameEn);
            }

            var thapPhanChu = ReadEng(thapPhan, minimumNameEn);
            if (nguyen == 0)
                return thapPhanChu.Trim();
            nguyenChu = ReadEng(nguyen, currencyNameEn);

            return nguyenChu.Trim() + " and " + thapPhanChu.ToLower().Trim();

            //var thapPhanChu = ReadENG(thapPhan, minimumNameEn);
            //if (nguyen == 0)
            //    return thapPhanChu.Trim();

            //nguyenChu = ReadENG(nguyen, currencyNameEn);

            //return nguyenChu.Trim() + " and " + thapPhanChu.ToLower().Trim();
        }

        public static string FormatMoneyVi(this decimal? currency)
        {
            if (!currency.HasValue)
                currency = 0;
            return string.Format(new CultureInfo("vi-VN"), "{0:#,##0.00}", currency);
        }

        #region Hàm đọc tiền thành chữ v1

        public static string DocTienRaChu(this decimal? currency,string donViTien = " đồng")
        {
            try
            {
                if (!currency.HasValue)
                {
                    return string.Empty;
                }

                var spl = Math.Round(currency.Value, MidpointRounding.AwayFromZero).ToString(CultureInfo.InvariantCulture).Split('.', ',');
                var ret = new StringBuilder(TienRaChuKhongCoDong(long.Parse(spl[0]), true));
                if (spl.Length > 1)
                {
                    ret.Append(" phẩy ");
                    ret.Append(TienRaChuKhongCoDong(long.Parse(spl[1]), false));
                }

                ret.Append(donViTien);
                return ret.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        private static string TienRaChuKhongCoDong(long soTien, bool isChan)
        {
            String vR = "";
            if (soTien < 0)
                return vR;
            String vR1 = "";
            long d = 0, So1, So2, So3;
            long lRound = (long)Math.Round((decimal)soTien);
            String ChuSo = "không,một,hai,ba,bốn,năm,sáu,bảy,tám,chín";
            String DonViTien = ",nghìn,triệu,tỉ,nghìn tỉ, triệu tỉ, tỉ tỉ";
            String[] arr1 = ChuSo.Split(',');
            String[] arr2 = DonViTien.Split(',');
            do
            {
                So1 = lRound % 10;
                lRound = (lRound - So1) / 10;
                So2 = lRound % 10;
                lRound = (lRound - So2) / 10;
                So3 = lRound % 10;
                lRound = (lRound - So3) / 10;
                if (!(So1 == 0 && So2 == 0 && So3 == 0))
                {
                    vR1 = "";
                    if (So3 != 0 || lRound != 0)
                    {
                        vR1 = arr1[So3] + " trăm";
                    }
                    if (So2 == 0)
                    {
                        if (vR1 != "" && So1 != 0)
                        {
                            vR1 += " linh";
                        }
                    }
                    else if (So2 == 1)
                    {
                        vR1 += " mười";
                    }
                    else
                    {
                        vR1 += " " + arr1[So2] + " mươi";
                    }
                    if (So1 != 0)
                    {
                        if (So1 == 1 && So2 >= 2)
                        {
                            vR1 += " mốt";
                        }
                        else if (So1 == 5 && So2 >= 1)
                        {
                            vR1 += " lăm";
                        }
                        else
                        {
                            vR1 += " " + arr1[So1];
                        }
                    }
                    vR1 = vR1.Trim();
                    if (vR1 != "")
                    {
                        vR = vR1 + " " + arr2[d] + " " + vR.Trim();
                    }
                }
                d = d + 1;
            } while (lRound != 0);
            vR = vR.Trim();
            if (vR == "")
            {
                vR = "không";
            }
            if (isChan)
            {
                vR = vR.Substring(0, 1).ToUpper() + vR.Substring(1);
            }
            return vR;
        }


        #endregion
    }
}