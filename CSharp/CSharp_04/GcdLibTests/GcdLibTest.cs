using NUnit.Framework;
using GcdLib;
using System;

namespace GcdLibTests
{
    [TestFixture]
    public class GcdLibTest
    {
        [Test]
        public void TestEuclidGcd_EvenNumbers_CulculateGcd()
        {
            int expected = 4;
            int actual = Gcd.EuclidGcd(12, 20);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEuclidGcd_OddNumbers_CulculateGcd()
        {
            int expected = 7;
            int actual = Gcd.EuclidGcd(21, 7);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSteinGcd_EvenNumbers_CulculateGcd()
        {
            int expected = 4;
            int actual = Gcd.SteinGcd(12, 20);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSteinGcd_OddNumbers_CulculateGcd()
        {
            int expected = 7;
            int actual = Gcd.SteinGcd(21, 7);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEuclidGcd_3NumbersNumbers_CulculateGcd()
        {
            int expected = 2;
            int actual = Gcd.EuclidGcd(12, 20, 18);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEuclidGcd_4NumbersNumbers_CulculateGcd()
        {
            int expected = 1;
            int actual = Gcd.EuclidGcd(3, 5, 7, 11);

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEuclidGcd_withTimeSpent_CulculateGcd()
        {
            int expected = 1;
            TimeSpan timespent;
            int actual = Gcd.EuclidGcd(out timespent, 3, 5, 7, 11);
            Assert.IsNotNull(timespent);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSteinGcd_First5PrimeNumbers_CulculateGcd()
        {
            int expected = 1;
            TimeSpan timespent;
            int actual = Gcd.SteinGcd(out timespent, 3, 5, 7, 11, 2);
            Assert.IsNotNull(timespent);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestEuclidGcd_15Numbers_CulculateGcd()
        {
            int expected = 2;
            TimeSpan timespent;
            int actual = Gcd.EuclidGcd(out timespent, 2, 4, 16, 20, 48, 42, 346, 122, 66, 56, 348, 22, 82, 546);
            Assert.IsNotNull(timespent);
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void TestSteinGcd_15Numbers_CulculateGcd()
        {
            int expected = 2;
            TimeSpan timespent;
            int actual = Gcd.SteinGcd(out timespent, 2, 4, 16, 20, 48, 42, 346, 122, 66, 56, 348, 22, 82, 546);
            Assert.IsNotNull(timespent);
            Assert.AreEqual(expected, actual);
        }
    }
}