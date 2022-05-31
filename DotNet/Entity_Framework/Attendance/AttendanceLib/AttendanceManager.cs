using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceLib
{
    public class AttendanceManager
    {
        private readonly string connectionString;

        public AttendanceManager(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task InitDataBaseAsync()
        {
            using (UniversityContext db = new UniversityContext(connectionString))
            {
                await db.Database.EnsureDeletedAsync();
                await db.Database.EnsureCreatedAsync();
            }
        }

        public async Task AddLectureAsync(DateTime date, string topic)
        {
            using (UniversityContext db = new UniversityContext(connectionString))
            {
                await db.Lectures.AddAsync(new Lecture(date, topic));
                await db.SaveChangesAsync();
            }
        }

        public async Task AddStudentAsync(string name)
        {
            using (UniversityContext db = new UniversityContext(connectionString))
            {
                await db.Students.AddAsync(new Student(name));
                await db.SaveChangesAsync();
            }
        }
        public async Task<bool> AttendLectureAsync(string studentName, DateTime lectureDate, int mark)
        {
            using (UniversityContext db = new UniversityContext(connectionString))
            {
                var student = db.Students.FirstOrDefault(s=> s.Name == studentName);
                var lecture = db.Lectures.FirstOrDefault(l => l.Date == lectureDate);

                if (lecture != null && student != null)
                {
                    await db.Attendaces.AddAsync(new Attendance() { Student = student, Lecture = lecture, Mark = mark});
                    await db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
        }

        public async Task<List<AttendanceReport>> GetAttendanceAsync()
        {
            using (UniversityContext db = new UniversityContext(connectionString))
            {
                var lectures = db.Lectures.Include(l => l.Attendances);
                var result = await lectures.SelectMany(
                        l => l.Attendances.Where(a=> a.LectureId == l.Id).DefaultIfEmpty(),
                        (l, a) => new AttendanceReport(l.Topic, l.Date, a.Student.Name) 
                    ).ToListAsync();
                var students = await db.Students.Where(s => !s.Attendances.Any()).Select(s=> new AttendanceReport(null, null, s.Name)).ToListAsync();
                result = result.Concat(students).ToList();

                return result;
            }
        }
    }
}
