using System;
using System.IO;
using System.Xml.Serialization;
using BinarySearchTreeLib;

namespace Services
{
    public class StudentDataService
    {
        private string pathToFile;
        private IterativeTree<StudentData> studentDatas;
        public StudentDataService(string pathToFile)
        {
            this.pathToFile = pathToFile;
        }

        public IterativeTree<StudentData> GetStudentDatas()
        {
            var xmlreader = new XmlSerializer(typeof(Node<StudentData>));
            Node<StudentData> node;
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                node = (Node<StudentData>)xmlreader.Deserialize(fs);
            }
            studentDatas = new IterativeTree<StudentData>(node); ;
            return studentDatas;
        }
        public void Add(StudentData studentData)
        {
            studentDatas.Add(studentData);
        }
        public void SaveStudentDatas()
        {
            var xmlwriter = new XmlSerializer(typeof(Node<StudentData>));
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                xmlwriter.Serialize(fs, studentDatas.Root);
            }
        }
    }
}
