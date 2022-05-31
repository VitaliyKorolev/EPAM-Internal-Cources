using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentAttendanceLib
{
    public class AttendanceEntity
    {
        public string LectureTopic { get; init; }
        public DateTime? LectureDate { get; init; }
        public string StudentName { get; init; }

        public AttendanceEntity(string lectureTopic, DateTime? lectureDate, string studentName)
        {
            LectureTopic = lectureTopic;
            LectureDate = lectureDate;
            StudentName = studentName;
        }
    }
}
