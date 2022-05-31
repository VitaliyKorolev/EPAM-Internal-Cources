using System;
using System.Collections.Generic;

namespace AttendanceLib
{
    public class Attendance
    {
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int LectureId { get; set; }
        public Lecture Lecture { get; set; }
        public int Mark { get; set; }
    }
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Student(string name)
        {
            Name = name;
        }
        public List<Attendance> Attendances { get; set; } = new();
    }
    public class Lecture
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Topic { get; set; }

        public Lecture(DateTime date, string topic)
        {
            Date = date;
            Topic = topic;
        }

        public List<Attendance> Attendances { get; set; } = new();
    }
}
