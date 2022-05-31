using NUnit.Framework;
using VectorLib;

namespace VectorLibTests
{
    public class VectorTest
    {
        [TestCaseSource(nameof(SumCases))]
        public void SumTest(Vector v1, Vector v2, Vector expected)
        {
            Vector actual = v1 + v2;

            Assert.AreEqual(expected.X, actual.X);
            Assert.AreEqual(expected.Y, actual.Y);
            Assert.AreEqual(expected.Z, actual.Z);
        }
        [TestCaseSource(nameof(SubstructCases))]
        public void SubtractTest(Vector v1, Vector v2, Vector expected)
        {
            Vector actual = v1 - v2;

            Assert.AreEqual(expected.X, actual.X);
            Assert.AreEqual(expected.Y, actual.Y);
            Assert.AreEqual(expected.Z, actual.Z);
        } 
        [TestCaseSource(nameof(MultiplyCases))]
        public void MultiplyTest(Vector v1, Vector v2, double expected)
        {
            double actual = v1 * v2;

            Assert.AreEqual(expected, actual);
        }
        [TestCaseSource(nameof(EqualCases))]
        public void EqualTest(Vector v1, Vector v2, bool expected)
        {
            bool actual = v1 == v2;

            Assert.AreEqual(expected, actual);
        }
        static object[] SumCases =
        {
            new object[] { new Vector(1, 2, 3), new Vector(2, 4, 2), new Vector(3, 6, 5) },
            new object[] { new Vector(2.34, 2.43, 5.34), new Vector(1.34, 4.5656, 7.45), new Vector(2.34 + 1.34, 2.43 + 4.5656, 5.34 + 7.45) },
        };
        static object[] SubstructCases =
        {
            new object[] { new Vector(1, 2, 3), new Vector(2, 4, 2), new Vector(1 - 2, 2 - 4, 3 - 2) },
            new object[] { new Vector(2.34, 2.43, 5.34), new Vector(1.34, 4.5656, 7.45), new Vector(2.34 - 1.34, 2.43 - 4.5656, 5.34 - 7.45) },
        };
        static object[] MultiplyCases =
        {
            new object[] { new Vector(1, 2, 3), new Vector(2, 4, 2), 1 * 2 + 2 * 4 + 3 * 2 },
            new object[] { new Vector(2.34, 2.43, 5.34), new Vector(1.34, 4.5656, 7.45), (2.34 * 1.34 + 2.43 * 4.5656 + 5.34 * 7.45) },
        };
        static object[] EqualCases =
        {
            new object[] { new Vector(1, 2, 3), new Vector(1, 2, 3), true },
            new object[] { new Vector(2.34, 2.43, 5.34), new Vector(1.34, 4.5656, 7.45), false },
        };
    }
}