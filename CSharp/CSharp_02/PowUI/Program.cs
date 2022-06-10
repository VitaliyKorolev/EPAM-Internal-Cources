using System;
using System.Globalization;
using PowLib;

namespace PowUI
{
    class Program
    {
        private delegate bool Predicate<T>(T num);
        private delegate bool Parser<T>(string s, NumberStyles style, CultureInfo cultureInfo, out T result);

        static void Main(string[] args)
        {
            Console.WriteLine("This programm calculates root of the 'n' degree of the number");
            Console.WriteLine("Type a number");
            double number = Parse<double>(double.TryParse, n => n > 0);

            Console.WriteLine("Type a root degree (positive integer number)");
            int rootDegree = Parse<int>(int.TryParse, n => n > 0);

            Console.WriteLine("Type accuracy (positive number smaller than 0.11)");
            double accuracy = Parse<double>(double.TryParse, n => n <= 0.11 && n > 0);

            double calculatedByMyPow = MyImplementationPow.CalculateSqrtN(number, rootDegree, accuracy);
            double calculatedByStandartPow = MyImplementationPow.StandartSqrtN(number, rootDegree);
            Console.WriteLine($"Results culculated by my Pow {calculatedByMyPow}.");
            Console.WriteLine($"Results culculated by my standart Pow {calculatedByMyPow}.");

            Compare(calculatedByMyPow, calculatedByStandartPow, accuracy);
        }
        private static T Parse<T>(Parser<T> parser, Predicate<T> predicate)
        {
            NumberStyles style = NumberStyles.AllowDecimalPoint;
            T result;
            while (!parser(Console.ReadLine(), style, CultureInfo.InvariantCulture, out result) || !predicate(result))
            {
                Console.WriteLine("Wrong input");
            }
            return result;
        }
        private static void Compare(double number1, double number2, double accuracy)
        {
            if (number1 == number2)
            {
                Console.WriteLine($"Results are equal with accuracy {accuracy}.");
            }
            else
            {
                Console.WriteLine($"Results are not equal with accuracy {accuracy}.");
            }
        }
    }
}
