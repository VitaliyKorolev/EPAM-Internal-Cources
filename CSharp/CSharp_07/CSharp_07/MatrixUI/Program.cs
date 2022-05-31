using MatrixLib;
using System;
using System.Globalization;

namespace MatrixUI
{
    class Program
    {
        private delegate bool Parser<T>(string s, NumberStyles style, CultureInfo cultureInfo, out T result);
        private delegate bool Predicate<T>(T num);
        static void Main(string[] args)
        {
            Console.WriteLine("This program performs operations with matrixes");
            Console.WriteLine("First matrix");
            int rows1;
            int columns1;
            Matrix m1 = MatrixParse(out rows1, out columns1);
            Console.WriteLine(m1.ToString());

            Console.WriteLine("Second matrix");
            int rows2;
            int columns2;
            Matrix m2 = MatrixParse(out rows2, out columns2);
            Console.WriteLine(m2.ToString());

            try
            {
                Console.WriteLine($"Sum of matrixes = ");
                Matrix matrixSum = Matrix.Sum(m1, m2);
                Console.WriteLine(matrixSum?.ToString());

                Console.WriteLine($"Subtract of matrixes = ");
                Matrix matrixSubtract = Matrix.Subtract(m1, m2);
                Console.WriteLine(matrixSubtract?.ToString());

                Console.WriteLine($"Product of matrixes = ");
                Matrix matrixProduct = Matrix.Multiply(m1, m2);
                Console.WriteLine(matrixProduct?.ToString());
            }
            catch (MatrixException ex)
            {
                Console.WriteLine("Invalid matrix dimensions for this operation");
                Console.WriteLine($"First matrix size = {ex.FirstMatrixRows}*{ex.FirstMatrixColumns}");
                Console.WriteLine($"Second matrix size = {ex.SecondMatrixRows}*{ex.SecondMatrixColumns}");
            }
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

        private static T Parse<T>(Parser<T> parser)
        {
            NumberStyles style = NumberStyles.AllowDecimalPoint;
            T result;
            while (!parser(Console.ReadLine(), style, CultureInfo.InvariantCulture, out result))
            {
                Console.WriteLine("Wrong input");
            }
            return result;
        }

        private static Matrix MatrixParse(out int rows, out int columns)
        {
            Console.WriteLine("Type number of rows");
            rows = Parse<int>(int.TryParse, x => x > 0);
            Console.WriteLine("Type number of columns");
            columns = Parse<int>(int.TryParse, x => x > 0);
            Console.WriteLine("Type matrix coefficients");
            double[,] coeffs = new double[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    coeffs[i, j] = Parse<double>(double.TryParse);
                }
            }
            return new Matrix(coeffs);
        }
    }
}


