
using System.Text.RegularExpressions;
using System.Text;

namespace PTS.Shared.Utilities
{
    public static class StringUtility
    {

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static string[] VietnameseSigns = new string[]
       {

       "aAeEoOuUiIdDyY",

       "áàạảãâấầậẩẫăắằặẳẵ",

       "ÁÀẠẢÃ ẤẦẬẨẪĂẮẰẶẲẴ",

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
        public static string RemoveSign4VietnameseString(string str, bool toLower = false)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            if (toLower)
            {
                return str.ToLower();
            }
            else
            {
                return str;
            }

        }
        public static string ConvertToUnsign(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string ReplaceYCharToIChar(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            s = s.ToLower()
                .Replace("y", "i")
                .Replace("ỳ", "i")
                .Replace("ỷ", "i")
                .Replace("ý", "i")
                .Replace("ỵ", "i");
            return s;
        }

    }
}
