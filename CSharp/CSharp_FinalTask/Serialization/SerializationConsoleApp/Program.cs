using BinarySearchTreeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SerializationConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\Netlab_Vitalii\CSharp\CSharp_FinalTask\Serialization\";
            string intTreeFileName = "integerTree.dat";
            string studentTreeFileName = "studentTree.dat";

            StudentData data = new StudentData("Anton", "Black", "Math", DateTime.Today, 5);
            StudentData data1 = new StudentData("Valeri", "Yellow", "Biology", DateTime.Today, 4);
            StudentData data3 = new StudentData("Anton", "Smith", "Biology", DateTime.Today, 3);

            IEnumerable<StudentData> studentCollection = new StudentData[]
            {
                data1,
                data,
                data3,
            };
            IEnumerable<int> intCollection = new int[]
            {
                3,
                1,
                2,
                0,
                5,
                4,
                6
            };
            IterativeTree<StudentData> studentTree = new IterativeTree<StudentData>(studentCollection);
            IterativeTree<int> intTree = new IterativeTree<int>(intCollection);
            BinaryFormatter formatter = new BinaryFormatter();

            BinarySerialize(intTree, Path.Combine(path, intTreeFileName), formatter);
            IterativeTree<int> newIntTree = (IterativeTree<int>)BinaryDeserialize<int>(Path.Combine(path, intTreeFileName), formatter);
            Console.WriteLine($"Trees after serialization and befor are equal - {CompareTrees(intTree, newIntTree)}");

            BinarySerialize(studentTree, Path.Combine(path, studentTreeFileName), formatter);
            IterativeTree<StudentData> newStudentTree = (IterativeTree<StudentData>)BinaryDeserialize<StudentData>(Path.Combine(path, studentTreeFileName), formatter);
            Console.WriteLine($"Trees after serialization and befor are equal - {CompareTrees(studentTree, newStudentTree)}");

            //var xmlwriter = new XmlSerializer(typeof(Node<StudentData>));
            //using (FileStream fs = new FileStream(Path.Combine(path, "SudentDatas.xml"), FileMode.OpenOrCreate))
            //{
            //    xmlwriter.Serialize(fs, studentTree.Root);
            //}
            var xmlwriter = new XmlSerializer(typeof(IterativeTree<StudentData>));
            using (FileStream fs = new FileStream(Path.Combine(path, "Tree.xml"), FileMode.OpenOrCreate))
            {
                xmlwriter.Serialize(fs, studentTree);
            }

            IterativeTree<StudentData> newTreeXML;
            var xmlreader = new XmlSerializer(typeof(IterativeTree<StudentData>));
            using (FileStream fs = new FileStream(Path.Combine(path, "Tree.xml"), FileMode.OpenOrCreate))
            {
                newTreeXML = (IterativeTree<StudentData>)xmlreader.Deserialize(fs);
            }
            Console.WriteLine($"Trees after serialization and befor are equal - {CompareTrees(studentTree, newTreeXML)}");
        }

        static bool CompareTrees<TItem>(BinarySearchTree<TItem> tree1, BinarySearchTree<TItem> tree2) where TItem : IComparable<TItem>
        {
            List<TItem> list1 = tree1.ToList();
            List<TItem> list2 = tree2.ToList();
            if (list1.Count != list2.Count)
            {
                return false;
            }
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].CompareTo(list2[i]) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        static void BinarySerialize<TItem>(BinarySearchTree<TItem> tree, string fullPath, BinaryFormatter formatter) where TItem : IComparable<TItem>
        {
            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, tree);
            }
        }

        static BinarySearchTree<TItem> BinaryDeserialize<TItem>(string fullPath, BinaryFormatter formatter) where TItem : IComparable<TItem>
        {
            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                return (BinarySearchTree<TItem>)formatter.Deserialize(fs);
            }
        }
    }
}
