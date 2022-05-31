using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using StudentAttendanceLib;

namespace StudentAttendanceUI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            StudentAttendanceManager manager = new StudentAttendanceManager(connection);
            try
            {
                if (args.Length!=0 && args[0] == "-init")
                {
                    await manager.InitDataBaseAsync();
                    Console.WriteLine($"Database was successfully initialized ");
                }
                if (args.Length != 0 && args[0] == "-lecture")
                {
                    await manager.AddLectureAsync(DateTime.Parse(args[1]), args[2]);
                    Console.WriteLine($"Lecture {args[1]} - {args[2]} was successfully added to data base");
                }

                if (args.Length != 0 && args[0] == "-student")
                {
                    await manager.AddStudentAsync(args[1]);
                    Console.WriteLine($"Student {args[1]} was successfully added to data base");
                }

                if (args.Length != 0 && args[0] == "-attend")
                {
                    await manager.AttendLectureAsync(args[1], DateTime.Parse(args[2]), int.Parse(args[3]));
                    Console.WriteLine($"Attendance record was successfully added to data base");
                }
                if (args.Length != 0 && args[0] == "-report")
                {
                    var report = await manager.GetAttendanceAsync();
                    var lectures = report.Where(r => r.LectureDate != null && r.LectureTopic != null).GroupBy(r => new { r.LectureTopic, r.LectureDate });
                    foreach (var lecture in lectures)
                    {
                        Console.WriteLine($"Lecture {lecture.Key.LectureTopic} - Date {lecture.Key.LectureDate:d}");
                        foreach (var student in lecture)
                        {
                            Console.WriteLine($"   {student.StudentName}");
                        }
                    }
                    var students = report.Where(r => r.LectureDate == null && r.LectureTopic == null).Select(r => r.StudentName);
                    if(students.Count() != 0)
                    {
                        Console.WriteLine($"Students that havent been on any lecture:");
                        foreach (var student in students)
                        {
                            Console.WriteLine($"   {student}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
