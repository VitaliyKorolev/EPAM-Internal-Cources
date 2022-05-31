using System;

namespace PointProcessor
{
    public class Formatter
    {
        private const int outputStringLength = 12;
        private const int alignment = 5;
        public static string Format(Point point)
        {
            if (point == null)
            {
                return null;
            }
            string X = point.X.ToString("0.0###");
            string Y = point.Y.ToString("0.0###");

            string result = ("X:" + new string(' ', alignment - X.IndexOf(',')) + X).PadRight(outputStringLength + 1)+
                            ("Y:" + new string(' ', alignment - Y.IndexOf(',')) + Y).PadRight(outputStringLength);
            return result;
        }
    }
}
