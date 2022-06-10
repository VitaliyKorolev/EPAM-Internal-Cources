using NUnit.Framework;
using PolynomialLib;

namespace PolynomialLibTests
{
    public class Tests
    {
        [TestCaseSource(nameof(SumCases))]
        public void SumTest(Polynomial p1, Polynomial p2, Polynomial expected)
        {
            Polynomial actual = p1 + p2;

            Assert.AreEqual(expected.Coefficients, actual.Coefficients);
        }
        [TestCaseSource(nameof(SubtructCases))]
        public void SubtractTest(Polynomial v1, Polynomial v2, Polynomial expected)
        {
            Polynomial actual = v1 - v2;

            Assert.AreEqual(expected.Coefficients, actual.Coefficients);
        }
        [TestCaseSource(nameof(MultiplyCases))]
        public void MultiplyTest(Polynomial v1, double num, Polynomial expected)
        {
            Polynomial actual = v1 * num;

            Assert.AreEqual(expected.Coefficients, actual.Coefficients);
        }
        static object[] SumCases =
        {
            new object[] { new Polynomial(1, 2, 3, 2, 5), new Polynomial(2, 4, 2), new Polynomial(3, 6, 5, 2, 5) },
            new object[] { new Polynomial(2.34, 2.43, 5.34), new Polynomial(1.34, 4.5656, 7.45, 10.5), new Polynomial(2.34 + 1.34, 2.43 + 4.5656, 5.34 + 7.45, 10.5) },
        };
        static object[] SubtructCases =
        {
            new object[] { new Polynomial(1, 2, 3), new Polynomial(2, 4, 2, -3, 5), new Polynomial(1 - 2, 2 - 4, 3 - 2, 3, -5) },
            new object[] { new Polynomial(2.34, 2.43, 5.34, 4.65), new Polynomial(1.34, 4.5656, 7.45), new Polynomial(2.34 - 1.34, 2.43 - 4.5656, 5.34 - 7.45, 4.65) },
        };
        static object[] MultiplyCases =
        {
            new object[] { new Polynomial(1, 2, 3), 2, new Polynomial(1 * 2, 2 * 2, 3 * 2) },
            new object[] { new Polynomial(2.34, 2.43, 5.34), 2.43, new Polynomial(2.34 * 2.43, 2.43 * 2.43, 5.34 * 2.43) },
        };
    }
}