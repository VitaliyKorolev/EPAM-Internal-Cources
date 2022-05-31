using System;

namespace PowLib
{
    public class MyImplementationPow
    {
        public static double CalculateSqrtN(double A, int n, double eps = 0.11)
        {
            double x0 = A / n;
            var x1 = (1 / (double)n) * ((n - 1) * x0 + A / Pow(x0, (int)n - 1));
            while (Abs(x1 - x0) > eps)
            {
                x0 = x1;
                x1 = (1 / (double)n) * ((n - 1) * x0 + A / Pow(x0, (int)n - 1));
            }
            return x1;
        }
        private static double Pow(double a, int pow)
        {
            double result = 1;
            for (int i = 0; i < pow; i++) result *= a;
            return result;
        }
        private static double Abs(double x)
        {
            return x < 0 ? -x : x;
        }
        public static double StandartSqrtN(double A, int n)
        {
            return Math.Pow(A, 1/(double)n);
        }
    }
}
