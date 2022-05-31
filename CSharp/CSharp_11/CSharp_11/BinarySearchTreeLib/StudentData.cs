using System;

namespace BinarySearchTreeLib
{
    [Serializable]
    public class StudentData : IComparable<StudentData>
    {
        public string Name { get; }
        public string LastName { get; }
        public string TestName { get; }
        public DateTime DateOfTest { get; }
        public int TestScore { get; }

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
            if(other == null)
                return 1;

            if (Name.CompareTo(other.Name) != 0)
                return Name.CompareTo(other.Name);

            if (LastName.CompareTo(other.LastName) != 0)
                return LastName.CompareTo(other.LastName); 

            if (TestName.CompareTo(other.TestName) != 0)
                return TestName.CompareTo(other.TestName);

            if (DateOfTest.CompareTo(other.DateOfTest) != 0)
                return DateOfTest.CompareTo(other.DateOfTest);

            if (TestScore.CompareTo(other.TestScore) != 0)
                return TestScore.CompareTo(other.TestScore);

            return 0;
        }
    }
}
