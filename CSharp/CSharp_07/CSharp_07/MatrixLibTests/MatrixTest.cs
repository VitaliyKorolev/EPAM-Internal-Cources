using NUnit.Framework;
using MatrixLib;
using System;

namespace MatrixLibTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SumTest()
        {
            double[,] arr1 = { { 0.34, 1.34, 2.45 }, { 3.1, 4.54, 5.564 } };
            double[,] arr2 = { { 0.22, 1.54, 0.45 }, { 3.67, 3.45, 10.31 } };
            double[,] arr3 = { { 0.34 + 0.22, 1.34 + 1.54, 2.45 + 0.45 }, { 3.1 + 3.67, 4.54 + 3.45, 5.564 + 10.31 } };

            Matrix m1 = new Matrix(arr1);
            Matrix m2 = new Matrix(arr2);
            Matrix expected = new Matrix(arr3);
            Matrix actual = Matrix.Sum(m1, m2);

            AssertAreEqual(expected, actual);
        }
        private void AssertAreEqual(Matrix m1, Matrix m2)
        {
            Assert.AreEqual(m1.Rows, m2.Rows);
            Assert.AreEqual(m1.Columns, m2.Columns);
            for(int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    Assert.AreEqual(m1[i, j], m2[i, j]);
                }
            }
        }
        [Test]
        public void SubtractTest()
        {
            double[,] arr1 = { { 0.34, 1.34, 2.45 }, { 3.1, 4.54, 5.564 } };
            double[,] arr2 = { { 0.22, 1.54, 0.45 }, { 3.67, 3.45, 10.31 } };
            double[,] arr3 = { { 0.34 - 0.22, 1.34 - 1.54, 2.45 - 0.45 }, { 3.1 - 3.67, 4.54 - 3.45, 5.564 - 10.31 } };

            Matrix m1 = new Matrix(arr1);
            Matrix m2 = new Matrix(arr2);
            Matrix expected = new Matrix(arr3);
            Matrix actual = Matrix.Subtract(m1, m2);

            AssertAreEqual(expected, actual);
        }
        [Test]
        public void MultiplyTest()
        {
            double[,] arr1 = { { 0.34, 1.34, 2.45 }, { 3.1, 4.54, 5.564 } };
            double[,] arr2 = { { 0.22, 1.54}, { 3.67, 3.45}, { 4.59, 9.32 } };
            double[,] arr3 = { { 16.2381, 27.980600000000003}, { 42.88256, 72.29348 } };

            Matrix m1 = new Matrix(arr1);
            Matrix m2 = new Matrix(arr2);
            Matrix expected = new Matrix(arr3);
            Matrix actual = Matrix.Multiply(m1, m2);

            AssertAreEqual(expected, actual);
        }
        [Test]
        public void MultiplyTestThrowMatrixException()
        {
            double[,] arr1 = { { 0.34, 1.34, 2.45 }, { 3.1, 4.54, 5.564 } };
            double[,] arr2 = { { 0.22, 1.54 }, { 3.67, 3.45 } };

            Matrix m1 = new Matrix(arr1);
            Matrix m2 = new Matrix(arr2);

            Assert.That(()=>Matrix.Multiply(m1, m2), Throws.InstanceOf<MatrixException>());
        }

        [Test]
        public void ThrowIndexOutOfRageExceptionTest()
        {
            double[,] arr1 = { { 0.34, 1.34, 2.45 }, { 3.1, 4.54, 5.564 } };

            Matrix m1 = new Matrix(arr1);

            Assert.That(() => m1[4, 6], Throws.InstanceOf<IndexOutOfRangeException>());
            Assert.That(() => m1[-1, 0] = 4, Throws.InstanceOf<IndexOutOfRangeException>());
        }
        [Test]
        public void RowsPropertyTest()
        {
            double[,] arr1 = { { 0.34, 1.34, 2.45 }, { 3.1, 4.54, 5.564 } };

            Matrix m1 = new Matrix(arr1);
            int expected = 2;
            int actual = m1.Rows;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ColumnsPropertyTest()
        {
            double[,] arr1 = { { 0.34, 1.34, 2.45 }, { 3.1, 4.54, 5.564 } };

            Matrix m1 = new Matrix(arr1);
            int expected = 3;
            int actual = m1.Columns;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ArgumentOutOfRangeConstructorTest()
        {
            Assert.That(() => new Matrix(-2, -5), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void ToStringTest()
        {
            double[,] arr1 = { { 0.34, 1.34, 2.45 }, { 3.1, 4.54, 5.564 } };

            Matrix m1 = new Matrix(arr1);
            string expected = " 0,34       1,34       2,45      \r\n 3,1        4,54       5,564     \r\n";
            string actual = m1.ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}