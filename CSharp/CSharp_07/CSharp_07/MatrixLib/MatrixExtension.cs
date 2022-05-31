using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixLib
{
    public static class MatrixExtension
    {
        public const int firstDimensionNumber = 0;
        public const int secondDimensionNumber = 1;
        public static int Rows(this double[,] array)
        {
            return array.GetUpperBound(firstDimensionNumber) + 1;
        }

        public static int Columns(this double[,] array)
        {
            return array.GetUpperBound(secondDimensionNumber) + 1;
        }
    }
}
