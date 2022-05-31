using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BinarySearchTreeLib
{
    [Serializable]
    public class StudentData : IComparable<StudentData>
    {
        public string Name { get;  set; }
        public string LastName { get;  set; }
        public string TestName { get;  set; }
        public DateTime DateOfTest { get;  set; }
        public int TestScore { get;  set; }

        public StudentData() { }
        public StudentData(string name, string lastName, string testName, DateTime dateOfTest, int testScore)
        {
            Name = name;
            LastName = lastName;
            TestName = testName;
            DateOfTest = dateOfTest;
            TestScore = testScore;
        }

        public int CompareTo(StudentData other)
        {
            if (Name.CompareTo(other.Name) == 0)
            {
                if(LastName.CompareTo(other.LastName) == 0)
                {
                    if (TestName.CompareTo(other.TestName) == 0)
                    {
                        if (DateOfTest.CompareTo(other.DateOfTest) == 0)
                        {
                            return TestScore.CompareTo(other.TestScore);
                        }
                        return DateOfTest.CompareTo(other.DateOfTest);
                    }
                    return TestName.CompareTo(other.TestName);
                }
                return LastName.CompareTo(other.LastName);
            }
            return Name.CompareTo(other.Name);
        }

        //XmlSchema IXmlSerializable.GetSchema()
        //{
        //    return null;
        //}

        //void IXmlSerializable.ReadXml(XmlReader reader)
        //{
        //    Name = reader.ReadString();
        //    LastName = reader.ReadString();
        //    TestName = reader.ReadString();
        //    DateOfTest = DateTime.Parse( reader.ReadString());
        //    TestScore = int.Parse(reader.ReadString());
        //}

        //void IXmlSerializable.WriteXml(XmlWriter writer)
        //{
        //    writer.WriteString(Name);
        //    writer.WriteString(LastName);
        //    writer.WriteString(TestName);
        //    writer.WriteString(DateOfTest.ToString());
        //    writer.WriteString(TestScore.ToString());
        //}
    }
}
