using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AttendanceLib
{
    internal class UniversityContext : DbContext
    {
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Attendance> Attendaces { get; set; }
        private string connectionString;

        public UniversityContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().HasIndex(u => u.Name).IsUnique(true);
            modelBuilder.Entity<Lecture>().HasIndex(l => l.Date).IsUnique(true);
            modelBuilder.Entity<Attendance>().HasKey(a => new {a.StudentId, a.LectureId});
        }
    }
}
