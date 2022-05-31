using BinarySearchTreeLib;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SerializationTests
{
    public class Tests
    {

        [Test]
        public void TestBinnarySerialisation_DeserializeIntTree_TreesAreEqual()
        {
            IEnumerable<int> intCollection = new int[] { 3, 1, 2, 0, 5, 4, 6 };
            IterativeTree<int> expected = new IterativeTree<int>(intCollection);
            BinaryFormatter formatter = new BinaryFormatter();
            string fullPath = @"E:\Netlab_Vitalii\CSharp\CSharp_FinalTask\Serialization\integerTree.dat";
            IterativeTree<int> actual;
            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                actual = (IterativeTree<int>)formatter.Deserialize(fs);
            }

            CompareTrees<int>(expected, actual);
        }

        [Test]
        public void TestBinnarySerialisation_DeserializeStudentTree_TreesAreEqual()
        {
            StudentData data = new StudentData("Anton", "Black", "Math", DateTime.Parse("23.03.2022"), 5); ;
            StudentData data1 = new StudentData("Valeri", "Yellow", "Biology", DateTime.Parse("23.03.2022"), 4);
            StudentData data3 = new StudentData("Anton", "Smith", "Biology", DateTime.Parse("23.03.2022"), 3);

            IEnumerable<StudentData> studentCollection = new StudentData[] { data1, data, data3, };
            IterativeTree<StudentData> expected = new IterativeTree<StudentData>(studentCollection);
            BinaryFormatter formatter = new BinaryFormatter();
            string fullPath = @"E:\Netlab_Vitalii\CSharp\CSharp_FinalTask\Serialization\studentTree.dat";
            IterativeTree<StudentData> actual;
            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                actual = (IterativeTree<StudentData>)formatter.Deserialize(fs);
            }

            CompareTrees<StudentData>(expected, actual);
        }

        [Test]
        public void TestXmlSerialisation_DeserializeStudentTree_TreesAreEqual()
        {
            StudentData data = new StudentData("Anton", "Black", "Math", DateTime.Parse("23.03.2022"), 5); ;
            StudentData data1 = new StudentData("Valeri", "Yellow", "Biology", DateTime.Parse("23.03.2022"), 4);
            StudentData data3 = new StudentData("Anton", "Smith", "Biology", DateTime.Parse("23.03.2022"), 3);

            IEnumerable<StudentData> studentCollection = new StudentData[] { data1, data, data3, };
            IterativeTree<StudentData> expected = new IterativeTree<StudentData>(studentCollection);
            IterativeTree<StudentData> actual;
            var xmlreader = new XmlSerializer(typeof(IterativeTree<StudentData>));
            string fullPath = @"E:\Netlab_Vitalii\CSharp\CSharp_FinalTask\Serialization\Tree.xml";
            using (FileStream fs = new FileStream(fullPath, FileMode.OpenOrCreate))
            {
                actual = (IterativeTree<StudentData>)xmlreader.Deserialize(fs);
            }

            CompareTrees<StudentData>(expected, actual);
        }

        static void CompareTrees<TItem>(BinarySearchTree<TItem> tree1, BinarySearchTree<TItem> tree2) where TItem : IComparable<TItem>
        {
            List<TItem> list1 = tree1.ToList();
            List<TItem> list2 = tree2.ToList();
            Assert.AreEqual(list1.Count, list2.Count);

            for (int i = 0; i < list1.Count; i++)
            {
                Assert.AreEqual(list1[i].CompareTo(list2[i]), 0);
            }
        }
    }
}