using System;
using ConverterLib;

namespace ConverterUI
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = Parse();
            foreach (BaseSystem baseSystem in Enum.GetValues(typeof(BaseSystem)))
            {
                Console.WriteLine($"Number in {baseSystem} format");

                if (baseSystem == BaseSystem.Base32)
                {
                    Compare(Converter.ToBase(number, baseSystem), string.Empty);
                }
                else if (baseSystem == BaseSystem.Base64)
                {
                    Compare(Converter.ToBase(number, baseSystem), Converter.ToBase64Standart(number));
                }
                else
                {
                    Compare(Converter.ToBase(number, baseSystem), Convert.ToString(number, (int)baseSystem));
                }
                Console.WriteLine();
            }
        }
        private static int Parse()
        {
            int result;
            Console.WriteLine("Type a positive number");
            while (!Int32.TryParse(Console.ReadLine(), out result) || result < 0)
            {
                Console.WriteLine("Wrong input");
            }
            return result;
        }
        private static void Compare(string s1, string s2)
        {
            Console.WriteLine(s1 + "     " + s2);
            if (s1 == s2)
            {
                Console.WriteLine("Results are equal");
            }
            else
            {
                Console.WriteLine("Results are not equal");
            }
        }
    }
}
