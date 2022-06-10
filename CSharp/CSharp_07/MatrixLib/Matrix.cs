using System;
using System.Text;
using System.Linq;

namespace MatrixLib
{
    public class Matrix
    {
        private double[,] _matrix;
        public double this[int i, int j]
        {
            get
            {
                if (i >= this.Rows || j >= this.Columns || i < 0 || j < 0)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                return _matrix[i, j];
            }
            set 
            {
                if (i >= this.Rows || j >= this.Columns || i < 0 || j < 0)
                {
                    throw new IndexOutOfRangeException("Index out of range");
                }
                _matrix[i, j] = value;
            }
        }

        public int Rows
        {
            get { return _matrix.Rows(); }
        }

        public int Columns
        {
            get { return _matrix.Columns(); }
        }

        public Matrix(double[,] matrix)
        {
            _matrix = matrix;
        }

        public Matrix(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentOutOfRangeException("Invalid matrix dimensions");
            }
            _matrix = new double[rows, columns];
        }

        public static Matrix Sum(Matrix m1, Matrix m2)
        {
            MatrixesCheck(m1, m2, MatrixOperation.Subtract);
            double[,] temp = new double[m1.Rows, m2.Columns];
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    temp[i, j] = m1[i, j] + m2[i, j];
                }
            }
            return new Matrix(temp);
        }

        public static Matrix Subtract(Matrix m1, Matrix m2)
        {
            MatrixesCheck(m1, m2, MatrixOperation.Subtract);
            double[,] temp = new double[m1.Rows, m2.Columns];
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    temp[i, j] = m1[i, j] - m2[i, j];
                }
            }
            return new Matrix(temp);
        }

        public static Matrix Multiply(Matrix m1, Matrix m2)
        {
            MatrixesCheck(m1, m2, MatrixOperation.Multiply);
            double[,] temp = new double[m1.Rows, m2.Columns];
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m2.Columns; j++)
                {
                    for (int k = 0; k < m2.Rows; k++)
                    {
                        temp[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return new Matrix(temp);
        }

        public override string ToString()
        {
            const int widthOfNumberCell = 10;
            StringBuilder st = new StringBuilder();
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Columns; j++)
                {
                    st.Append($"{this[i, j], -widthOfNumberCell: 0.####} ");
                }
                st.Append(Environment.NewLine);
            }
            return st.ToString();
        }

        public override bool Equals(object other)
        {
            var toCompareWith = other as Matrix;
            if (toCompareWith == null)
                return false;
            return this == toCompareWith;
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            if (ReferenceEquals(m1, m2))
            {
                return true;
            }
            if (((object)m1 == null) || ((object)m2 == null))
            {
                return false;
            }
            return Compare(m1, m2);
        }

        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !(m1 == m2);
        }

        private static bool Compare(Matrix m1, Matrix m2)
        {
            if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
            {
                return false;
            }
            for (int i = 0; i < m1.Rows; i++)
            {
                for (int j = 0; j < m1.Columns; j++)
                {
                    if( m1[i, j] != m2[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_matrix);
        }

        private static bool MatrixesCheck(Matrix m1, Matrix m2, MatrixOperation operation)
        {
            if (m1 == null || m2 == null)
            {
                throw new MatrixException("One of the arguments are null", m1, m2);
            }
            if (operation == MatrixOperation.Sum || operation == MatrixOperation.Subtract)
            {
                if (m1.Rows != m2.Rows || m1.Columns != m2.Columns)
                {
                    throw new MatrixException("Matrix dimensions mismatch", m1, m2);
                }
            }
            if (operation == MatrixOperation.Multiply)
            {
                if (m1.Columns != m2.Rows)
                {
                    throw new MatrixException("Matrix dimensions mismatch", m1, m2);
                }
            }
            return true;
        }
    }
}
