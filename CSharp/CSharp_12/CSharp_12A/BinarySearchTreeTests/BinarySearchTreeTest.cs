using NUnit.Framework;
using BinarySearchTreeLib;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace BinarySearchTreeTests
{
    public class Tests
    {
        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_DateOfTheFirstTest_ReturnValue(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);
            var query = tree.Select(d => d.DateOfTest).Min();

            var expected = DateTime.Today.AddDays(-90);
            Assert.AreEqual(query, expected);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_NumberOfTests_ReturnValue(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);
            var query = tree.Where(d => d.Name != string.Empty && d.TestName != string.Empty).Count();

            var expected = 9;
            Assert.AreEqual(query, expected);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_ThreeMaxScores_Return3Values(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);
            var query = tree.OrderByDescending(d => d.TestScore).Select(d => d.TestScore).Take(3).ToArray();

            int[] expected = { 5, 4, 4 };
            Assert.AreEqual(query, expected);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_GetAllStudents_ReturnStudentsFullNames(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);
            var query = tree.Where(d => d.Name != string.Empty)
                .Select(d => $"{d.Name} {d.LastName}").Distinct().ToArray();

            string[] expected = { "Andrey Smith", "Anton Black", "Valeri Yellow" };
            Assert.AreEqual(query, expected);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_GetAllStudentsWithGoodScores_ReturnStudentsFullNames(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);
            //var intermediate = tree.Where(d => d.Name != string.Empty)
            //    .Select(d => new
            //    {
            //        Fullname = $"{d.Name} {d.LastName}",
            //        Score = d.TestScore
            //    });

            //var query = intermediate.GroupBy(
            //    d => d.Fullname,
            //    d => d.Score,
            //    (key, g) => new { Fullname = key, Scores = g.ToList() })
            //    .Where(g => !g.Scores.Contains(2) && !g.Scores.Contains(3)).Select(g => g.Fullname).OrderBy(name => name).ToArray();

            //var query = intermediate.GroupBy(d => d.Fullname).Select(g => new
            //{
            //    Fullname = g.Key,
            //    Scores = g.Select(s => s.Score).ToList(),
            //}).Where(g => !g.Scores.Contains(2) && !g.Scores.Contains(3)).Select(g => g.Fullname).OrderBy(name => name).ToArray();

            var studentsWithBadScores = tree.Where(d => d.TestScore == 2 || d.TestScore == 3).Select(d => $"{d.Name} {d.LastName}").Distinct().ToArray();
            var query = tree.Select(d => $"{d.Name} {d.LastName}").Except(studentsWithBadScores).Distinct().ToArray();
            string[] expected = { "Anton Black", "Valeri Yellow" };
            Assert.AreEqual(query, expected);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_GetTwoScoredTests_ReturnTests(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);
            var query = tree.Where(d => d.TestScore == 2)
                .Select(d => d.TestName).Distinct().OrderBy(d => d).ToArray();

            string[] expected = { "CSS-300", "HTML-100" };
            Assert.AreEqual(query, expected);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_AverageScoreForEveryStudent_ReturnScores(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);
            var intermediate = tree.Where(d => d.Name != string.Empty)
                .Select(d => new
                {
                    Fullname = $"{d.Name} {d.LastName}",
                    Score = d.TestScore
                }).ToArray();

            var query = intermediate.GroupBy(
                d => d.Fullname,
                d => d.Score,
                (key, g) => new { Fullname = key, AverageScore = g.Average() })
                .ToDictionary(g => g.Fullname, g => g.AverageScore);

            Dictionary<string, double> expected = new Dictionary<string, double>()
            {
                ["Andrey Smith"] = (double)(2 + 4 + 2) / 3,
                ["Anton Black"] = (double)(4 + 4 + 5) / 3,
                ["Valeri Yellow"] = (double)(4 + 4 + 4) / 3,
            };

            Assert.AreEqual(query.Keys, expected.Keys);
            Assert.AreEqual(query.Values, expected.Values);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_AllTestsForCertainMounth_ReturnValues(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);
            DateTime date = DateTime.Today.AddDays(-90);
            var query = tree.Where(d => d.DateOfTest.Month == date.Month && d.DateOfTest.Year == date.Year).ToArray();

            StudentData[] expected = new StudentData[]
            {
                new StudentData("Valeri", "Yellow", "SQL-200", DateTime.Today.AddDays(-90), 4)
            };

            Assert.AreEqual(query.Length, expected.Length);
            Assert.AreEqual(query[0].Name, expected[0].Name);
            Assert.AreEqual(query[0].TestName, expected[0].TestName);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_AllTestsThatDontMatchThePattern_ReturnValues(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);
            string pattern = @"[A-Za-z]+-(100|200|300)";
            var query = tree.Where(d => !Regex.IsMatch(d.TestName, pattern)).Select(d => d.TestName).ToArray();

            string[] expected = 
            {
                "Biology",
                "C#-123",
            };

            Assert.AreEqual(query, expected);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_AllTestsThatStudentPassed_ReturnValues(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);

            var query = tree.Where(d => d.Name != string.Empty).GroupBy(d => new { Name = d.Name, LastName = d.LastName })
                .Select(g => new
                {
                    FullName = $"{g.Key.Name} {g.Key.LastName}",
                    Tests = g.Select(t => t.TestName).ToArray()
                }).ToDictionary(key =>key.FullName, value =>value.Tests);

            Dictionary<string, string[]> expected = new Dictionary<string, string[]>()
            {
                ["Andrey Smith"] = new string[] { "CSS-300" , "HTML-100", "HTML-100" },
                ["Anton Black"] = new string[] { "Math-100", "Math-100", "SQL-200" },
                ["Valeri Yellow"] = new string[] { "Biology", "C#-123", "SQL-200" }
            };

            Assert.AreEqual(query.Keys, expected.Keys);
            Assert.AreEqual(query.Values, expected.Values);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_AllTestsThatStudentRetaked_ReturnValues(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);

            var query = tree.Where(d => d.Name != string.Empty).GroupBy(d => new { Name = d.Name, LastName = d.LastName, Test = d.TestName})
                .Where(g =>g.Count() > 1)
                .Select(g => new
                {
                    FullName = $"{g.Key.Name} {g.Key.LastName}",
                    Tests = g.Select(t => t.TestName).Distinct().ToArray()
                }).ToDictionary(key => key.FullName, value => value.Tests);

            Dictionary<string, string[]> expected = new Dictionary<string, string[]>()
            {
                ["Andrey Smith"] = new string[] { "HTML-100" },
                ["Anton Black"] = new string[] { "Math-100" },
            };

            Assert.AreEqual(query.Keys, expected.Keys);
            Assert.AreEqual(query.Values, expected.Values);
        }

        [TestCaseSource(nameof(data))]
        public void TestLinqToBinarySearchTree_AllTestsStudentsDidntPassInCertainYear_ReturnValues(IEnumerable<StudentData> data)
        {
            var tree = new IterativeTree<StudentData>(data);

            var query = tree.Where(d => d.DateOfTest.Year != 2022).Select(d => d.TestName);

            string[] expected = new string[]
            {
                "SQL-200"
            };

            Assert.AreEqual(query, expected);
        }
        static IEnumerable<StudentData> data1 = new StudentData[]
        {
            new StudentData("Anton", "Black", "Math-100", DateTime.Today.AddDays(-32), 4),
            new StudentData("Anton", "Black", "Math-100", DateTime.Today.AddDays(-10), 5),
            new StudentData("Anton", "Black", "SQL-200", DateTime.Today.AddDays(-50), 4),

            new StudentData("Valeri", "Yellow", "SQL-200", DateTime.Today.AddDays(-90), 4),
            new StudentData("Valeri", "Yellow", "Biology", DateTime.Today.AddDays(-80), 4),
            new StudentData("Valeri", "Yellow", "C#-123", DateTime.Today.AddDays(-60), 4),

            new StudentData("Andrey", "Smith", "HTML-100", DateTime.Today.AddDays(-45), 2),
            new StudentData("Andrey", "Smith", "HTML-100", DateTime.Today.AddDays(-32), 4),
            new StudentData("Andrey", "Smith", "CSS-300", DateTime.Today.AddDays(-45), 2),

        };
        static IEnumerable<StudentData>[] data = new IEnumerable<StudentData>[]
        {
            data1,
        };
    }
}