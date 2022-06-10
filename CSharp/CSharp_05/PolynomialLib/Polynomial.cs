using System;

namespace PolynomialLib
{
    public class Polynomial
    {
        private double[] coefficients;
        private delegate double Operate(double num1, double num2);
        public double[] Coefficients
        {
            get { return coefficients; }
        }

        public int Length
        {
            get { return coefficients.Length; }
        }

        public Polynomial(params double[] coefficients)
        {
            if (coefficients == null)
            {
                throw new ArgumentNullException();
            }
            this.coefficients = coefficients;
        }

        public double this[int index]
        {
            get { return coefficients[index]; }
        }

        private static Polynomial OperateMethod(Polynomial p1, Polynomial p2, Operate operate)
        {
            int length;
            double[] data1;
            double[] data2;
            if (p1.Length >= p2.Length)
            {
                length = p1.Length;
                data1 = p1.coefficients;
                data2 = new double[length];
                Array.Copy(p2.coefficients, data2, p2.Length);
            }
            else
            {
                length = p2.Length;
                data1 = new double[length];
                data2 = p2.coefficients;
                Array.Copy(p1.coefficients, data1, p1.Length);
            }
            for (int i = 0; i < length; i++)
            {
                data1[i] = operate(data1[i], data2[i]);
            }
            return new Polynomial(data1);
        }

        public static Polynomial operator +(Polynomial p1, Polynomial p2)
        {
            if (p1 == null || p2 == null)
            {
                throw new ArgumentNullException();
            }
            return OperateMethod(p1,p2, (a, b) => a + b);
        }

        public static Polynomial operator -(Polynomial p1, Polynomial p2)
        {
            if (p1 == null || p2 == null)
            {
                throw new ArgumentNullException();
            }
            return OperateMethod(p1, p2, (a, b) => a - b);
        }

        public static Polynomial operator *(Polynomial p1, double num)
        {
            if (p1 == null)
            {
                throw new ArgumentNullException();
            }
            double[] data = p1.coefficients;
            for (int i = 0; i < p1.Length; i++)
            {
                data[i] *=num ;
            }
            return new Polynomial(data);
        }

        public static Polynomial operator *(double num, Polynomial p1)
        {
            return p1 * num;
        }
    }
}
