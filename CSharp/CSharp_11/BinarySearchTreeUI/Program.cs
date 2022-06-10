using BinarySearchTreeLib;
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentData data = new StudentData("Anton", "Black", "Math", DateTime.Today, 5);
            StudentData data1 = new StudentData("Valeri", "Yellow", "Biology", DateTime.Today, 4);
            StudentData data3 = new StudentData("Anton", "Smith", "Biology", DateTime.Today, 3);

            IEnumerable<StudentData> collection = new StudentData[]
            {
                data1,
                data,
                data3,
            };
            IEnumerable<int> collection1 = new int[]
            {
                3,
                1,
                2,
                0,
                5,
                4,
                6
            };
            RecursiveTree<StudentData> studentTree = new RecursiveTree<StudentData>(collection);
            IterativeTree<int> intTree = new IterativeTree<int>(collection1);
            studentTree.Remove(data);
            intTree.Remove(3);

            foreach (var el in studentTree)
            {
                Console.WriteLine(el.Name);
            }

            foreach (var el in intTree)
            {
                Console.WriteLine(el);
            }
        }
    }
}
