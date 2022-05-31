using System;
using FilterLib;
using BinarySearchTreeLib;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace FilterUI
{
    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<StudentData> studentCollection = new StudentData[]
            {
                new StudentData("Anton", "Black", "Math", DateTime.Today, 5),
                new StudentData("Valeri", "Yellow", "Biology", DateTime.Today.AddDays(-10), 4),
                new StudentData("Andrey", "Ragetlli", "English", DateTime.Today, 3),
                new StudentData("Benjamin", "Fortune", "Biology", DateTime.Today.AddDays(-10), 3),
                new StudentData("Mathev", "Unicorn", "Chemistry", DateTime.Today, 3),
                new StudentData("Aaron", "Smith", "Biology", DateTime.Today.AddDays(-10), 3),
                new StudentData("Tyler", "Derden", "Phylosophy", DateTime.Today, 5),
            };
            Filter<StudentData> filter1 = new();
            Filter<StudentData> filter2 = new();
            Filter<StudentData> filter3 = new();
            Filter<StudentData> filter4 = new();
            Filter<StudentData> filter5 = new();

            filter1.PropertyInRange<int>("TestScore", 4, 5);
            var collection1 = filter1.Apply(studentCollection);

            filter2.PropertyGreaterThanValue<string>("Name", "Valeri");
            var collection2 = filter2.Apply(studentCollection);

            filter3.PropertyEqualsToValue<string>("LastName", "Derden");
            var collection3 = filter3.Apply(studentCollection);

            filter4.PropertyEqualsToValue<string>("TestName", "Math");
            var collection4 = filter4.Apply(studentCollection);

            filter5.PropertyLessThanValue<DateTime>("DateOfTest", DateTime.Today);
            var collection5 = filter5.Apply(studentCollection);

        }
    }
}