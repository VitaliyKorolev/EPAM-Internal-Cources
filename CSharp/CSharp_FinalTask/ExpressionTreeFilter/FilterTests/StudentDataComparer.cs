using BinarySearchTreeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterTests
{
    public class StudentDataComparer : EqualityComparer<StudentData>
    {
        public override bool Equals(StudentData x, StudentData y)
        {
            return x.Name == y.Name && x.LastName == y.LastName && x.TestName == y.TestName
                   && x.TestScore == y.TestScore && x.DateOfTest == y.DateOfTest;
        }
        public override int GetHashCode(StudentData obj)
        {
            return HashCode.Combine(obj.Name, obj.LastName, obj.TestName, obj.TestScore, obj.DateOfTest);
        }
    }

}
