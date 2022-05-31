using NUnit.Framework;
using TriangleLib;

namespace TriangleTest
{
    [TestFixture]
    public class TriangleTests
    {
        [Test]
        public void TestConstructor_NegativeNumbers_InvalidTriangle()
        {
            Triangle triangle = new Triangle(-2.34, -100.23, 234);
            double expected = 0;

            Assert.IsFalse(triangle.IsValid);
            Assert.AreEqual(expected, triangle.a);
            Assert.AreEqual(expected, triangle.b);
            Assert.AreEqual(expected, triangle.c);
        }
        [Test]
        public void TestConstructor_NonexistentTriangle_InvalidTriangle()
        {
            Triangle triangle = new Triangle(1, 2.23, 234);
            double expected = 0;

            Assert.IsFalse(triangle.IsValid);
            Assert.AreEqual(expected, triangle.a);
            Assert.AreEqual(expected, triangle.b);
            Assert.AreEqual(expected, triangle.c);
        }
        [Test]
        public void TestConstructor_WithZeroSide_InvalidTriangle()
        {
            Triangle triangle = new Triangle(1, 0, 234);
            double expected = 0;

            Assert.IsFalse(triangle.IsValid);
            Assert.AreEqual(expected, triangle.a);
            Assert.AreEqual(expected, triangle.b);
            Assert.AreEqual(expected, triangle.c);
        }
        [Test]
        public void TestConstructor_ValidTriangle_CreateValidTriangle()
        {
            Triangle triangle = new Triangle(3.45, 2.23, 5.234);
            double expectedA = 3.45;
            double expectedB = 2.23;
            double expectedC = 5.234;

            Assert.IsTrue(triangle.IsValid);
            Assert.AreEqual(expectedA, triangle.a);
            Assert.AreEqual(expectedB, triangle.b);
            Assert.AreEqual(expectedC, triangle.c);
        }
        [Test]
        public void TestGetPerimeter_ValidTriangle_CulculatePerimeter()
        {
            Triangle triangle = new Triangle(3.45, 2.23, 5.234);
            double expected = 3.45 + 2.23 + 5.234;
            double actual = triangle.GetPerimeter();

            Assert.IsTrue(triangle.IsValid);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void TestGetPerimeter_InvalidTriangle_ThrowException()
        {
            Triangle triangle = new Triangle(1, 2.23, 234);

            Assert.That(() => triangle.GetPerimeter(), Throws.Exception);
        }
        [Test]
        public void TestGetSquare_ValidTriangle_Culculatesquare()
        {
            Triangle triangle = new Triangle(3, 4, 5);
            double expected = 6;
            double actual = triangle.GetSquare();

            Assert.IsTrue(triangle.IsValid);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestGetSquare_InvalidTriangle_ThrowException()
        {
            Triangle triangle = new Triangle(1, 2.23, 234);

            Assert.That(() => triangle.GetSquare(), Throws.Exception);
        }
        [Test]
        public void TestTostring_ValidTriangle_ReturnString()
        {
            Triangle triangle = new Triangle(3, 4, 5);
            string expected = "Triangle with sides of 3, 4 and 5 ";
            string actual = triangle.ToString();

            Assert.IsTrue(triangle.IsValid);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestTostring_InvalidTriangle_ReturnString()
        {
            Triangle triangle = new Triangle(3, 15.345, 2);
            string expected = "Triangle is not valid ";
            string actual = triangle.ToString();

            Assert.IsFalse(triangle.IsValid);
            Assert.AreEqual(expected, actual);
        }
    }
}