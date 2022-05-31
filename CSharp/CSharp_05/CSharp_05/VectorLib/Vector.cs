using System;

namespace VectorLib
{
    public class Vector
    {
        public double X {get; private set;}
        public double Y { get; private set; }
        public double Z { get; private set; }
        public Vector(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1 == null || v2 == null)
            {
                throw new ArgumentNullException();
            }
            return new Vector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1 == null || v2 == null)
            {
                throw new ArgumentNullException();
            }
            return new Vector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }

        public static double operator *(Vector v1, Vector v2)
        {
            if (v1 == null || v2 == null)
            {
                throw new ArgumentNullException();
            }
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            if (ReferenceEquals(v1, v2))
            {
                return true;
            }
            if (((object)v1 == null) || ((object)v2 == null))
            {
                return false;
            }
            return v1.X == v2.X && v1.Y == v2.Y && v1.Z == v2.Z;
        }
        public override bool Equals(object other)
        {
            var toCompareWith = other as Vector;
            if (toCompareWith == null)
                return false;
            return this.X == toCompareWith.X && this.Y == toCompareWith.Y && this.Z == toCompareWith.Z;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }
    }
}
