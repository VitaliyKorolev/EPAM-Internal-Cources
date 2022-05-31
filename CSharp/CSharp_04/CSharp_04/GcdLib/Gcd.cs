using System;
using System.Diagnostics;

namespace GcdLib
{
    public class Gcd
    {
        private delegate int Calculation(params int[] args);

        public static int EuclidGcd(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            if (a < b)
            {
                Swap(a, b);
            }
            int t;
            while (b != 0)
            {
                t = b;
                b = a % b;
                a = t;
            }
            return a;
        }

        public static int EuclidGcd(int a, int b, int c)
        {
            int temp = EuclidGcd(a, b);
            return EuclidGcd(temp, c);
        }

        public static int EuclidGcd(int a, int b, int c, int d)
        {
            int temp = EuclidGcd(a, b, c);
            return EuclidGcd(temp, d);
        }

        public static int EuclidGcd(int a, int b, int c, int d, int e)
        {
            int temp = EuclidGcd(a, b, c, d);
            return EuclidGcd(temp, e);
        }

        public static int EuclidGcd(params int[] args)
        {
            int temp = args[0];
            for(int i = 0; i < args.Length - 1; i++)
            {
                temp = EuclidGcd(temp, args[i + 1]);
            }
            return temp;
        }

        private static void Swap(int a, int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }

        public static int SteinGcd(int a, int b)
        {
            if (a == b)
                return a;

            if (a == 0)
                return b;

            if (b == 0)
                return a;

            if (a % 2 == 0)
            {
                if (b % 2 == 0)
                    return 2 * SteinGcd(a / 2, b / 2);
                else
                    return SteinGcd(a / 2, b);
            }
            if (b % 2 == 0)
                return SteinGcd(a, b / 2);

            if (a > b)
                return SteinGcd((a - b) / 2, b);

            return SteinGcd(a, (b - a) / 2);
        }

        public static int SteinGcd(params int[] args)
        {
            int temp = args[0];
            for (int i = 0; i < args.Length - 1; i++)
            {
                temp = SteinGcd(temp, args[i + 1]);
            }
            return temp;
        }

        public static int SteinGcd(out TimeSpan timeSpent, params int[] args)
        {
            int temp = CalculateTime(SteinGcd, out timeSpent, args);
            return temp;
        }

        public static int EuclidGcd(out TimeSpan timeSpent, params int[] args)
        {
            int temp = CalculateTime(EuclidGcd, out timeSpent, args);
            return temp;
        }

        private static int CalculateTime(Calculation calculation, out TimeSpan timeSpent, params int[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            int result = calculation(args);
            stopWatch.Stop();
            timeSpent = stopWatch.Elapsed;
            return result;
        }
    }
}
