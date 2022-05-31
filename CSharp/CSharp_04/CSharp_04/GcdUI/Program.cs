using System;
using System.Collections.Generic;
using GcdLib;

namespace GcdUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program calculates greatest common divisor of arbitrary amount of integer numbers");
            List<int> numbers = new List<int>();
            Console.WriteLine("Type integer numbers, to exit press Enter");
            int? number;
            while (numbers.Count < 2)
            {
                while ((number = Parse()) != null)
                {
                    numbers.Add(number.Value);
                }
                if (numbers.Count < 2)
                {
                    Console.WriteLine("Type at least 2 integer numbers");
                }
            }      
            int gcd = Gcd.EuclidGcd(numbers.ToArray());
            Console.WriteLine($"greatest common divisor = {gcd}");
        }

        private static int? Parse()
        {
            int result;
            string key;

            while (!Int32.TryParse(key = Console.ReadLine(), out result) || result < 0)
            {
                if (key == string.Empty)
                    return null;
                Console.WriteLine("Wrong input");
            }
            return result;
        }
    }
}
