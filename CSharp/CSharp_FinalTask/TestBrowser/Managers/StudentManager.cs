using BinarySearchTreeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;

namespace Managers
{
    public class StudentManager
    {
        private StudentDataService studentService;

        public StudentManager(StudentDataService studentManager)
        {
            this.studentService = studentManager;
        }

        public IterativeTree<StudentData> GetStudentDatas()
        {
            return studentService.GetStudentDatas();
        }
        public void Add(StudentData studentData)
        {
            studentService.Add(studentData);
        }
        public void SaveStudentDatas()
        {
            studentService.SaveStudentDatas();
        }
    }
}
