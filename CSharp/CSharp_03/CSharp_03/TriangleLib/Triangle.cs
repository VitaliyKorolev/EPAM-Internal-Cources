using System;

namespace TriangleLib
{
    public class Triangle
    {
        public double a { get; private set; }
        public double b { get; private set; }
        public double c { get; private set; }
        public bool IsValid { get; private set; }
        public Triangle(double a, double b, double c)
        {
            if (ValidateTriangle(a, b, c))
            {
                this.a = a;
                this.b = b;
                this.c = c;
                this.IsValid = true;
            }
            this.IsValid = false;
        }
        private bool ValidateTriangle(double a, double b, double c)
        {
            if(a <= 0 || b <= 0 || c <= 0)
            {
                return false;
            }
            double[] sides = { a, b, c };
            Array.Sort(sides);
            if (sides[0] + sides[1] <= sides[2])
            {
                return false;
            }
            return true;
        }
        public double GetPerimeter()
        {
            if (!IsValid)
            {
                throw new Exception("Cannot calculate perimeter because triangle is not valid");
            }
            return a + b + c;
        }
        public double GetSquare()
        {
            if (!IsValid)
            {
                throw new Exception("Cannot calculate square because triangle is not valid");
            }
            double p = GetPerimeter() / 2;
            return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }
        public override string ToString()
        {
            if(!IsValid)
                return string.Format($"Triangle is not valid ");
            return string.Format($"Triangle with sides of {a}, {b} and {c} ");
        }
    }
}
