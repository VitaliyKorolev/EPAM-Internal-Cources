using System;
using System.Text;
namespace ConverterLib
{
    public class Converter
    {
        private const int offset = 10;  // A symbol corresponds to the value of 10, B - 11 and so on.
        public static readonly char[] AR = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".ToCharArray();
        public static string ToBin(int number)
        {
            return ToBase(number, BaseSystem.Bin);
        }
        public static string ToOct(int number)
        {
            return ToBase(number, BaseSystem.Oct);
        }
        public static string ToHex(int number)
        {
            return ToBase(number, BaseSystem.Hex);
        }
        public static string ToBase32(int number)
        {
            return ToBase(number, BaseSystem.Base32);
        }
        public static string ToBase64(int number)
        {
            return ToBase(number, BaseSystem.Base64);
        }
        public static string ToBase64Standart(int num)
        {
            char[] number = num.ToString().ToCharArray();
            var plainTextBytes = Encoding.UTF8.GetBytes(number);
            return Convert.ToBase64String(plainTextBytes);
        }
        public static string ToBase(int number, BaseSystem baseSystem)
        {
            string result = string.Empty;
            for (int i = 0; number > 0; i++)
            {
                if(number % (int)baseSystem >= offset)
                {
                    result = AR[number % (int)baseSystem - offset] + result;
                }
                else
                {
                    result = number % (int)baseSystem + result;
                }
                number = number / (int)baseSystem;
            }
            return result;
        }
    }
}
