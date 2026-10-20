-- University Course Management System: database schema
--
-- Rebuilt in October 2026 from the application code; the original 2019 database script
-- was not kept. Table names, column names and column order match what the code expects
-- (the INSERT statements in the gateways list values without column names, so the order matters).
--
-- Needs SQL Server 2017 or newer (the schedule view uses STRING_AGG). Run it on an empty database:
--   sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "CREATE DATABASE UniversityCourseManagement"
--   sqlcmd -S "(localdb)\MSSQLLocalDB" -d UniversityCourseManagement -i UniversityCourseManagement.sql
--
-- Every table has a single Id primary key. Indexes are plain (non-unique) and each one serves a
-- search the code runs; duplicates are prevented in the code, not by unique constraints.

SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
GO

-- Lookup tables -------------------------------------------------------------------------------

CREATE TABLE Department (
    Id       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Department PRIMARY KEY,
    DeptName VARCHAR(100) NOT NULL,
    DeptCode VARCHAR(7)   NOT NULL
);

CREATE TABLE Semester (
    Id       INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Semester PRIMARY KEY,
    Semester VARCHAR(20) NOT NULL
);

CREATE TABLE Designation (
    Id          INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Designation PRIMARY KEY,
    Designation VARCHAR(50) NOT NULL
);

CREATE TABLE Grade (
    Id    INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Grade PRIMARY KEY,
    Grade VARCHAR(5) NOT NULL
);

CREATE TABLE Room (
    Id     INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Room PRIMARY KEY,
    RoomNo VARCHAR(20) NOT NULL
);

-- Main tables ---------------------------------------------------------------------------------

CREATE TABLE Course (
    Id           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Course PRIMARY KEY,
    CourseName   VARCHAR(100) NOT NULL,
    CourseCode   VARCHAR(20)  NOT NULL,
    Credit       DECIMAL(4,2) NOT NULL,
    Description  VARCHAR(500) NOT NULL,
    DepartmentId INT NOT NULL CONSTRAINT FK_Course_Department REFERENCES Department (Id),
    SemesterId   INT NOT NULL CONSTRAINT FK_Course_Semester REFERENCES Semester (Id)
);

CREATE TABLE Teacher (
    Id              INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Teacher PRIMARY KEY,
    Name            VARCHAR(100) NOT NULL,
    Address         VARCHAR(500) NOT NULL,
    ContactNo       VARCHAR(15)  NOT NULL,
    Email           VARCHAR(50)  NOT NULL,
    DesignationId   INT NOT NULL CONSTRAINT FK_Teacher_Designation REFERENCES Designation (Id),
    DepartmentId    INT NOT NULL CONSTRAINT FK_Teacher_Department REFERENCES Department (Id),
    CreditToBeTaken DECIMAL(6,2) NOT NULL,
    RemainingCredit DECIMAL(6,2) NOT NULL   -- goes below zero when a teacher is given extra courses
);

CREATE TABLE AssignedCourse (
    Id           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AssignedCourse PRIMARY KEY,
    DepartmentId INT NOT NULL CONSTRAINT FK_AssignedCourse_Department REFERENCES Department (Id),
    TeacherId    INT NOT NULL CONSTRAINT FK_AssignedCourse_Teacher REFERENCES Teacher (Id),
    CourseId     INT NOT NULL CONSTRAINT FK_AssignedCourse_Course REFERENCES Course (Id),
    Status       BIT NOT NULL   -- 1 = assigned; 0 = released by "Unassign All Courses"
);

CREATE TABLE AllocatedRooms (
    Id           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_AllocatedRooms PRIMARY KEY,
    DepartmentId INT NOT NULL CONSTRAINT FK_AllocatedRooms_Department REFERENCES Department (Id),
    CourseId     INT NOT NULL CONSTRAINT FK_AllocatedRooms_Course REFERENCES Course (Id),
    RoomId       INT NOT NULL CONSTRAINT FK_AllocatedRooms_Room REFERENCES Room (Id),
    Day          VARCHAR(10) NOT NULL,
    StartTime    VARCHAR(10) NOT NULL,   -- as the page's time picker sends it, e.g. "09:00 AM";
                                         -- the clash check converts it to TIME (text order is not time order)
    FinishTime   VARCHAR(10) NOT NULL,
    Status       BIT NOT NULL
);

CREATE TABLE Student (
    Id                 INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Student PRIMARY KEY,
    Name               VARCHAR(50)  NOT NULL,
    Email              VARCHAR(50)  NOT NULL,
    ContactNo          VARCHAR(15)  NOT NULL,
    Date               VARCHAR(10)  NOT NULL,   -- registration date as text, "yyyy-mm-dd"; the code reads its
                                                -- first 4 characters as the year (SUBSTRING does not take a DATE)
    Address            VARCHAR(500) NOT NULL,
    DepartmentId       INT NOT NULL CONSTRAINT FK_Student_Department REFERENCES Department (Id),
    RegistrationNumber VARCHAR(20)  NOT NULL,
    Gender             VARCHAR(6)   NOT NULL,
    Session            VARCHAR(10)  NOT NULL
);

CREATE TABLE EnrolledCourses (
    Id           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_EnrolledCourses PRIMARY KEY,
    StudentId    INT NOT NULL CONSTRAINT FK_EnrolledCourses_Student REFERENCES Student (Id),
    CourseId     INT NOT NULL CONSTRAINT FK_EnrolledCourses_Course REFERENCES Course (Id),
    DepartmentId INT NOT NULL CONSTRAINT FK_EnrolledCourses_Department REFERENCES Department (Id),
    Date         DATE NOT NULL,
    Status       BIT  NOT NULL
);

CREATE TABLE Result (
    Id           INT IDENTITY(1,1) NOT NULL CONSTRAINT PK_Result PRIMARY KEY,
    StudentId    INT NOT NULL CONSTRAINT FK_Result_Student REFERENCES Student (Id),
    DepartmentId INT NOT NULL CONSTRAINT FK_Result_Department REFERENCES Department (Id),
    CourseId     INT NOT NULL CONSTRAINT FK_Result_Course REFERENCES Course (Id),
    GradeId      INT NOT NULL CONSTRAINT FK_Result_Grade REFERENCES Grade (Id)
);

-- Indexes: one per search the code runs -------------------------------------------------------

CREATE INDEX IX_Course_DepartmentId ON Course (DepartmentId);                      -- courses of a department
CREATE INDEX IX_Course_CourseCode ON Course (CourseCode);                          -- is this course code taken?
CREATE INDEX IX_Course_CourseName ON Course (CourseName);                          -- is this course name taken?
CREATE INDEX IX_Teacher_Email ON Teacher (Email);                                  -- is this teacher email taken?
CREATE INDEX IX_Student_Email ON Student (Email);                                  -- is this student email taken?
CREATE INDEX IX_Student_ContactNo ON Student (ContactNo);                          -- is this contact no taken?
CREATE INDEX IX_AssignedCourse_CourseId ON AssignedCourse (CourseId);              -- is this course assigned?
CREATE INDEX IX_AllocatedRooms_RoomId_Day ON AllocatedRooms (RoomId, Day);         -- room clash check
CREATE INDEX IX_EnrolledCourses_StudentId_CourseId ON EnrolledCourses (StudentId, CourseId); -- a student's courses
CREATE INDEX IX_Result_StudentId_CourseId ON Result (StudentId, CourseId);         -- has this result been saved?
GO

-- Lookup data (the app has no screens for these) ----------------------------------------------

INSERT INTO Semester (Semester) VALUES ('1st'), ('2nd'), ('3rd'), ('4th'), ('5th'), ('6th'), ('7th'), ('8th');
INSERT INTO Designation (Designation) VALUES ('Lecturer'), ('Assistant Professor'), ('Associate Professor'), ('Professor');
INSERT INTO Grade (Grade) VALUES ('A+'), ('A'), ('A-'), ('B+'), ('B'), ('B-'), ('C+'), ('C'), ('D'), ('F');
INSERT INTO Room (RoomNo) VALUES ('101'), ('102'), ('103'), ('104'), ('105'), ('201'), ('202'), ('203'), ('204'), ('205');
GO

-- Views used by the screens -------------------------------------------------------------------

-- View Course Statics: every course of a department with its semester and assigned teacher.
CREATE VIEW GetAllCoursesWithSemesterAndStatus AS
SELECT c.DepartmentId AS Id,
       c.CourseCode,
       c.CourseName,
       s.Semester,
       t.Name,
       ac.Status
FROM Course c
JOIN Semester s ON s.Id = c.SemesterId
LEFT JOIN AssignedCourse ac ON ac.CourseId = c.Id
LEFT JOIN Teacher t ON t.Id = ac.TeacherId;
GO

-- Class Schedule: one row per course, all of its room bookings in one text.
CREATE VIEW GetScheduleInfo AS
SELECT c.DepartmentId AS Id,
       c.CourseCode,
       c.CourseName,
       (SELECT STRING_AGG('R. No: ' + r.RoomNo + ', ' + ar.Day + ', ' + ar.StartTime + ' - ' + ar.FinishTime, '; ')
                   WITHIN GROUP (ORDER BY ar.Id)
        FROM AllocatedRooms ar
        JOIN Room r ON r.Id = ar.RoomId
        WHERE ar.CourseId = c.Id AND ar.Status = 1) AS [Schedule Info],
       CAST(CASE WHEN EXISTS (SELECT 1 FROM AllocatedRooms ar WHERE ar.CourseId = c.Id AND ar.Status = 1)
                 THEN 1 ELSE 0 END AS BIT) AS Status
FROM Course c;
GO

-- Enroll / Save Result / View Result: a student's name, email and department.
CREATE VIEW GetStudentInfoWithDeptName AS
SELECT st.Id,
       st.DepartmentId,
       st.Name,
       d.DeptName,
       st.Email
FROM Student st
JOIN Department d ON d.Id = st.DepartmentId;
GO

-- Enroll In a Course: the courses of the student's department.
CREATE VIEW GetCourseName AS
SELECT st.Id AS StudentId,
       c.Id AS CourseId,
       c.CourseName
FROM Student st
JOIN Course c ON c.DepartmentId = st.DepartmentId;
GO

-- Save Student Result: the courses a student is enrolled in.
CREATE VIEW GetEnrolledCourses AS
SELECT e.StudentId,
       e.CourseId,
       c.CourseName
FROM EnrolledCourses e
JOIN Course c ON c.Id = e.CourseId
WHERE e.Status = 1;
GO

-- View Result: every enrolled course with its grade (empty until a grade is saved).
CREATE VIEW GetStudentResult AS
SELECT e.StudentId,
       c.CourseCode,
       c.CourseName,
       g.Grade AS GradeName
FROM EnrolledCourses e
JOIN Course c ON c.Id = e.CourseId
LEFT JOIN Result r ON r.StudentId = e.StudentId AND r.CourseId = e.CourseId
LEFT JOIN Grade g ON g.Id = r.GradeId
WHERE e.Status = 1;
GO
