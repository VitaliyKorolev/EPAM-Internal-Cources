using MatrixLib;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] arr1 = { { 0.34, 1.34, 2.45 }, { 3.1, 4.54, 5.564 } };

            Matrix m1 = new Matrix(arr1);
            Console.WriteLine(m1);
        }
    }
}
