using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace PTS.Shared.Helper
{
    public static class StringHelper
    {
        public static string ConvertToUnsign(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            return regex.Replace(s.Normalize(NormalizationForm.FormD),
                    String.Empty).Replace('\u0111', 'd')
                .Replace('\u0110', 'D');
        }
        public static string ConvertToFts(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            var strBuild = new StringBuilder();
            strBuild.Append(s.ConvertToUnsign());
            if (strBuild.Length == 0)
            {
                return strBuild.ToString();
            }
            return strBuild.ToString()
                .Trim()
                .ToLower()
                .Replace("  ", " ");
        }

        public static string ConvertFtsAndRemoveAllSpaces(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            var strBuild = new StringBuilder();
            strBuild.Append(s.ConvertToUnsign());
            if (strBuild.Length == 0)
            {
                return strBuild.ToString();
            }
            return strBuild.ToString()
                .Trim()
                .ToLower()
                .Replace(" ", "");
        }

        public static string ReplaceLastOccurrence(this string s, string Find, string Replace)
        {
            int place = s.LastIndexOf(Find);

            if (place == -1)
                return s;

            string result = s.Remove(place, Find.Length).Insert(place, Replace);
            return result;
        }

        public static string ConvertFtsToUrlRoute(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return s;
            }
            var strBuild = new StringBuilder();
            strBuild.Append(s.ConvertToUnsign());
            if (strBuild.Length == 0)
            {
                return strBuild.ToString();
            }
            return strBuild.ToString()
                .Trim()
                .ToLower()
                .Replace(" ", "-");
        }
        public static string TextToTag(string text)
        {
            string strReturn = "";
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            strReturn = Regex.Replace(text, "[^\\w\\s]", string.Empty).Replace(" ", "-").ToLower();
            string strFormD = strReturn.Normalize(System.Text.NormalizationForm.FormD);
            return regex.Replace(strFormD, string.Empty).Replace("đ", "d");
        }

        public static string LikeTextSearch(this string s)
        {
            if (string.IsNullOrEmpty(s)) return s;

            return $"%{s.ConvertToFts()}%";
        }
        //public static string GetMimeType(this string fileName)
        //{
        //    var extension = System.IO.Path.GetExtension(fileName).ToLower();
        //    return FileExtensionMapping.Mappings.TryGetValue(extension, out var mimeType) ? mimeType : "application/octet-stream";
        //}

        public static string ViToUnicode(this string str)
        {
            string[] viChar =
            {
                "aAeEoOuUiIdDyY",
                "áàạảãâấầậẩẫăắằặẳẵ",
                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
                "éèẹẻẽêếềệểễ",
                "ÉÈẸẺẼÊẾỀỆỂỄ",
                "óòọỏõôốồộổỗơớờợởỡ",
                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
                "úùụủũưứừựửữ",
                "ÚÙỤỦŨƯỨỪỰỬỮ",
                "íìịỉĩ",
                "ÍÌỊỈĨ",
                "đ",
                "Đ",
                "ýỳỵỷỹ",
                "ÝỲỴỶỸ"
            };
            for (var i = 1; i < viChar.Length; i++)
            {
                for (var j = 0; j < viChar[i].Length; j++)
                    str = str.Replace(viChar[i][j], viChar[0][i - 1]);
            }
            return str;
        }
        public static string f_hex_to_string(this string v_hex_string)
        {
            if (string.IsNullOrEmpty(v_hex_string))
            {
                return string.Empty;
            }
            try
            {
                byte[] bytes = new byte[v_hex_string.Length / 2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    bytes[i] = Convert.ToByte(v_hex_string.Substring(i * 2, 2), 16);
                }
                return Encoding.UTF8.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static string CreateMd5(string input)
        {
            // step 1, calculate MD5 hash from input

            MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);

            byte[] hash = md5.ComputeHash(inputBytes);


            // step 2, convert byte array to hex string

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {

                sb.Append(hash[i].ToString("x2"));

            }

            return sb.ToString();

        }

        public static string ConvertDecimalToStringVn(this decimal? value, bool isDefaultZero = false)
        {
            return value.HasValue ? value.Value.ConvertDecimalToStringVn() : (isDefaultZero ? "0" : string.Empty);
        }
        public static string ConvertDecimalToStringVn(this decimal value)
        {
            if (value == 0) return "0";
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            return value.ToString("#,##", cul.NumberFormat);
        }

        public static string ConvertToBase64(this string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}
