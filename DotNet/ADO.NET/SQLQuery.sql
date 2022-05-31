CREATE DATABASE University
USE University

IF object_id('Attendance') is not null
    DROP TABLE Attendance
IF object_id('Students') is not null
    DROP TABLE Students
IF object_id('Lectures') is not null
    DROP TABLE Lectures
CREATE TABLE Students
(	
	Id INT PRIMARY KEY IDENTITY,
	Name NVARCHAR(100), 
	CHECK (Name !=''),
	CONSTRAINT UQ_Student_Name UNIQUE (Name)
)
CREATE TABLE Lectures
(	
	ID INT PRIMARY KEY IDENTITY,
	Date DATETIME,
	Topic NVARCHAR(200), 
	CHECK ((Topic !='') AND (Date IS NOT NULL)),
	CONSTRAINT UQ_Lecture_Date UNIQUE (Date)
)
CREATE TABLE Attendance
(	
	LectureID INT REFERENCES Lectures (ID) ON DELETE CASCADE,
	StudentID INT REFERENCES Students (ID) ON DELETE CASCADE,
	Mark INT 
	PRIMARY KEY(LectureID, StudentID)
)

INSERT Students VALUES ('Ivan'), ('Andrey')
INSERT Students VALUES ('Anton')

INSERT Lectures VALUES 
	('20220520 10:35:00 AM', 'ADO.NET'), 
	('20220521 10:35:00 AM' , 'ASP.NET')

--INSERT Attendance (LectureID, StudentID, Mark) VALUES
--	(2, 4, 5),
--	(2, 5, 5),
--	(2, 5, 3)


EXEC MarkAttendance @LectureDate = '20220520 10:35:00 AM', @StudentName ='Ivan', @Mark = 5

EXEC MarkAttendance @LectureDate = '20220520 10:35:00 AM', @StudentName ='Andrey', @Mark = 2

GO
CREATE OR ALTER PROCEDURE MarkAttendance
    @LectureDate DATETIME,
	@StudentName NVARCHAR(100),
	@Mark INT
AS
	DECLARE @LectureID INT
	SET @LectureID = (SELECT ID FROM Lectures L WHERE L.Date = @LectureDate)
	DECLARE @StudentID INT
	SET @StudentID = (SELECT ID FROM Students S WHERE S.Name = @StudentName)
    INSERT Attendance (LectureID, StudentID, Mark) VALUES
	(@LectureID, @StudentID, @Mark)
GO

SELECT L.Topic, L.Date, S.Name
FROM Attendance A
RIGHT JOIN Students S ON S.Id = A.StudentID
FULL JOIN Lectures L ON L.ID =A.LectureID
ORDER BY L.Date