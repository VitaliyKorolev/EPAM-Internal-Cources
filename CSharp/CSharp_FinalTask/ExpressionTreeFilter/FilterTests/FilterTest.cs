using BinarySearchTreeLib;
using FilterLib;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FilterTests
{
    public class Tests
    {
        [TestCaseSource(nameof(data))]
        public void TestPropertyInRange_TestScore_ReturnValues(IEnumerable<StudentData> data)
        {
            Filter<StudentData> filter = new();
            filter.PropertyInRange<int>("TestScore", 4, 5);
            StudentData[] actual = filter.Apply(studentCollection).ToArray();
            StudentData[] expected =
            {
                new StudentData("Anton", "Black", "Math", DateTime.Today, 5),
                new StudentData("Valeri", "Yellow", "Biology", DateTime.Today.AddDays(-10), 4),
                new StudentData("Tyler", "Derden", "Phylosophy", DateTime.Today, 5),
            };

            IStructuralEquatable se1 = actual;
            IStructuralEquatable se2 = expected;
            bool b = se1.Equals(se2);
            Assert.IsTrue(se1.Equals(se2, new StudentDataComparer()));
        }

        [TestCaseSource(nameof(data))]
        public void TestPropertyGreaterThanValue_TestScore_ReturnValues(IEnumerable<StudentData> data)
        {
            Filter<StudentData> filter = new();
            filter.PropertyGreaterThanValue<int>("TestScore", 4); ;
            StudentData[] actual = filter.Apply(studentCollection).ToArray();
            StudentData[] expected =
            {
                new StudentData("Anton", "Black", "Math", DateTime.Today, 5),
                new StudentData("Tyler", "Derden", "Phylosophy", DateTime.Today, 5),
            };

            IStructuralEquatable se1 = actual;
            IStructuralEquatable se2 = expected;
            bool b = se1.Equals(se2);
            Assert.IsTrue(se1.Equals(se2, new StudentDataComparer()));
        }

        [TestCaseSource(nameof(data))]
        public void TestPropertyEqualsToValue_LastName_ReturnValues(IEnumerable<StudentData> data)
        {
            Filter<StudentData> filter = new();
            filter.PropertyEqualsToValue<string>("LastName", "Derden");
            StudentData[] actual = filter.Apply(studentCollection).ToArray();
            StudentData[] expected =
            {
                new StudentData("Tyler", "Derden", "Phylosophy", DateTime.Today, 5),
            };

            IStructuralEquatable se1 = actual;
            IStructuralEquatable se2 = expected;
            bool b = se1.Equals(se2);
            Assert.IsTrue(se1.Equals(se2, new StudentDataComparer()));
        }

        [TestCaseSource(nameof(data))]
        public void TestPropertyLessThanValue_TestScore_ReturnValues(IEnumerable<StudentData> data)
        {
            Filter<StudentData> filter = new();
            filter.PropertyLessThanValue<int>("TestScore", 4);
            StudentData[] actual = filter.Apply(studentCollection).ToArray();
            StudentData[] expected =
            {
                new StudentData("Andrey", "Ragetlli", "English", DateTime.Today, 3),
                new StudentData("Benjamin", "Fortune", "Biology", DateTime.Today.AddDays(-10), 3),
                new StudentData("Mathev", "Unicorn", "Chemistry", DateTime.Today, 3),
                new StudentData("Aaron", "Smith", "Biology", DateTime.Today.AddDays(-10), 3),
            };

            IStructuralEquatable se1 = actual;
            IStructuralEquatable se2 = expected;
            bool b = se1.Equals(se2);
            Assert.IsTrue(se1.Equals(se2, new StudentDataComparer()));
        }

        [TestCaseSource(nameof(data))]
        public void TestPropertyContainsString_TestName_ReturnValues(IEnumerable<StudentData> data)
        {
            Filter<StudentData> filter = new();
            filter.PropertyContainsString("TestName", "Bio");
            StudentData[] actual = filter.Apply(studentCollection).ToArray();
            StudentData[] expected =
            {
                new StudentData("Valeri", "Yellow", "Biology", DateTime.Today.AddDays(-10), 4),
                new StudentData("Benjamin", "Fortune", "Biology", DateTime.Today.AddDays(-10), 3),
                new StudentData("Aaron", "Smith", "Biology", DateTime.Today.AddDays(-10), 3),
            };

            IStructuralEquatable se1 = actual;
            IStructuralEquatable se2 = expected;
            bool b = se1.Equals(se2);
            Assert.IsTrue(se1.Equals(se2, new StudentDataComparer()));
        }

        [TestCaseSource(nameof(data))]
        public void TestThrowingException_PropertyTypeDontMatch_ThrowsArgumentException(IEnumerable<StudentData> data)
        {
            Filter<StudentData> filter = new();

            //Assert.That(()=> filter.PropertyLessThanValue<string>("TestScore", "Andrey"), Throws.TypeOf<ArgumentException>());
            Assert.Throws<ArgumentException>(() => filter.PropertyLessThanValue<string>("TestScore", "Andrey"));
        }

        [TestCaseSource(nameof(data))]
        public void TestThrowingException_WrongPropertyName_ThrowsArgumentException(IEnumerable<StudentData> data)
        {
            Filter<StudentData> filter = new();

            //Assert.That(()=> filter.PropertyLessThanValue<string>("TestScore", "Andrey"), Throws.TypeOf<ArgumentException>());
            Assert.Throws<ArgumentException>(() => filter.PropertyLessThanValue<string>("Length", "Andrey"));
        }

        static IEnumerable<StudentData> studentCollection = new StudentData[]
        {
                new StudentData("Anton", "Black", "Math", DateTime.Today, 5),
                new StudentData("Valeri", "Yellow", "Biology", DateTime.Today.AddDays(-10), 4),
                new StudentData("Andrey", "Ragetlli", "English", DateTime.Today, 3),
                new StudentData("Benjamin", "Fortune", "Biology", DateTime.Today.AddDays(-10), 3),
                new StudentData("Mathev", "Unicorn", "Chemistry", DateTime.Today, 3),
                new StudentData("Aaron", "Smith", "Biology", DateTime.Today.AddDays(-10), 3),
                new StudentData("Tyler", "Derden", "Phylosophy", DateTime.Today, 5),
        };
        static IEnumerable<StudentData>[] data = new IEnumerable<StudentData>[]
        {
            studentCollection
        };
    }
}