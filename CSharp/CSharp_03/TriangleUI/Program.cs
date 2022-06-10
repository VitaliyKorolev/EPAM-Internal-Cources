using System;
using System.Globalization;
using TriangleLib;

namespace TriangleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type sides of triangle");

            Console.WriteLine("Type side A of triangle");
            double a = Parse();

            Console.WriteLine("Type side B of triangle");
            double b = Parse();

            Console.WriteLine("Type side C of triangle");
            double c = Parse();

            Triangle triangle = new Triangle(a, b, c);
            Console.WriteLine(triangle.ToString());
            if (triangle.IsValid)
            {
                Console.WriteLine($"Triangle perimeter = {triangle.GetPerimeter()}");
                Console.WriteLine($"Triangle square = {triangle.GetSquare()}");
            }
        }
        private static double Parse()
        {
            NumberStyles style = NumberStyles.Any;
            double a;
            while (!Double.TryParse(Console.ReadLine(), style, CultureInfo.InvariantCulture, out a))
            {
                Console.WriteLine("Wrong input");
            }
            return a;
        }
    }
}
