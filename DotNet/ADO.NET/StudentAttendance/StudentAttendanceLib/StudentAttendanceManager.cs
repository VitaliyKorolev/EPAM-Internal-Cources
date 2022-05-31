using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace StudentAttendanceLib
{
    public class StudentAttendanceManager
    {
        private readonly string connectionString;

        public StudentAttendanceManager(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public async Task InitDataBaseAsync()
        {
            string sqlExpression = "IF object_id('Attendance') is not null " +
                                    "DROP TABLE Attendance " +
                                    "IF object_id('Students') is not null " +
                                    "DROP TABLE Students " +
                                    "IF object_id('Lectures') is not null " +
                                    "DROP TABLE Lectures " +
                                    "CREATE TABLE Students " +
                                    "(Id INT PRIMARY KEY IDENTITY, " +
                                    "Name NVARCHAR(100), " +
                                    "CHECK(Name != ''), " +
                                    "CONSTRAINT UQ_Student_Name UNIQUE(Name)) " +
                                    "CREATE TABLE Lectures( " +
                                    "ID INT PRIMARY KEY IDENTITY, " +
                                    "Date DATETIME, " +
                                    "Topic NVARCHAR(200), " +
                                    "CHECK((Topic != '') AND(Date IS NOT NULL)), " +
                                    "CONSTRAINT UQ_Lecture_Date UNIQUE(Date)) " +
                                    "CREATE TABLE Attendance " +
                                    "(LectureID INT REFERENCES Lectures(ID) ON DELETE CASCADE, " +
                                    "StudentID INT REFERENCES Students(ID) ON DELETE CASCADE, " +
                                    "Mark INT " +
                                    "PRIMARY KEY(LectureID, StudentID)) ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AddLectureAsync(DateTime date, string topic)
        {
            string sqlExpression = "INSERT INTO Lectures (Date, Topic) VALUES (@date, @topic)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter dateParam = new SqlParameter("@date", date);
                command.Parameters.Add(dateParam);
                SqlParameter topicParam = new SqlParameter("@topic", topic);
                command.Parameters.Add(topicParam);

                int number = await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AddStudentAsync(string name)
        {
            string sqlExpression = "INSERT INTO Students (Name) VALUES (@name)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                SqlParameter nameParam = new SqlParameter("@name", name);
                command.Parameters.Add(nameParam);
                int number = await command.ExecuteNonQueryAsync();
            }
        }

        public async Task AttendLectureAsync(string studentName, DateTime lectureDate, int mark)
        {
            string sqlExpression = "MarkAttendance";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@StudentName", studentName);
                command.Parameters.AddWithValue("@LectureDate", lectureDate);
                command.Parameters.AddWithValue("@Mark", mark);

                int number = await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<List<AttendanceEntity>> GetAttendanceAsync()
        {
            string sqlExpression = "SELECT L.Topic, L.Date, S.Name " +
                                    "FROM Attendance A " +
                                    "RIGHT JOIN Students S ON S.Id = A.StudentID " +
                                    "FULL JOIN Lectures L ON L.ID = A.LectureID " +
                                    "ORDER BY L.Date ";

            var attendance = new List<AttendanceEntity>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        string topic = null;
                        DateTime? date = null;
                        string name = null;
                        if (!reader.IsDBNull("Topic"))
                            topic = reader.GetString("Topic");

                        if (!reader.IsDBNull("Date"))
                            date = reader.GetDateTime("Date");

                        if (!reader.IsDBNull("Name"))
                            name = reader.GetString("Name");

                        attendance.Add(new AttendanceEntity(topic, date, name));
                    }
                }
                return attendance;
            }
        }
    }
}
