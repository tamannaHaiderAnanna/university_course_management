-- University Course Management System: demo data (made-up names)
--
-- Run it after UniversityCourseManagement.sql. It can be run again at any time to reset a demo:
-- it deletes everything except the lookup lists (semesters, designations, grades, rooms) and then adds
-- the demo set again, all in one transaction.
--   sqlcmd -S "(localdb)\MSSQLLocalDB" -d UniversityCourseManagement -i DemoData.sql

SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

DELETE FROM Result;
DELETE FROM EnrolledCourses;
DELETE FROM AllocatedRooms;
DELETE FROM AssignedCourse;
DELETE FROM Student;
DELETE FROM Teacher;
DELETE FROM Course;
DELETE FROM Department;

INSERT INTO Department (DeptName, DeptCode) VALUES
    ('Computer Science and Engineering', 'CSE'),
    ('Electrical and Electronic Engineering', 'EEE'),
    ('Business Administration', 'BBA');

INSERT INTO Course (CourseName, CourseCode, Credit, Description, DepartmentId, SemesterId)
SELECT c.CourseName, c.CourseCode, c.Credit, c.Description, d.Id, s.Id
FROM (VALUES
    ('Structured Programming',             'CSE-1101', 3.0, 'Programming in C: variables, control flow, functions, arrays and pointers.', 'CSE', '1st'),
    ('Structured Programming Lab',         'CSE-1102', 1.5, 'Lab work for Structured Programming.',                                       'CSE', '1st'),
    ('Discrete Mathematics',               'CSE-1103', 3.0, 'Logic, sets, relations, counting and graphs.',                               'CSE', '1st'),
    ('Differential and Integral Calculus', 'MAT-1101', 3.0, 'Limits, derivatives and integrals.',                                         'CSE', '1st'),
    ('Digital Logic Design',               'CSE-1201', 3.0, 'Number systems, Boolean algebra and combinational circuits.',                'CSE', '2nd'),
    ('Object Oriented Programming',        'CSE-1203', 3.0, 'Classes, inheritance and polymorphism.',                                     'CSE', '2nd'),
    ('Electrical Circuits',                'EEE-1101', 3.0, 'DC circuits, network theorems and transients.',                              'EEE', '1st'),
    ('Electronic Devices',                 'EEE-1203', 3.0, 'Diodes, transistors and amplifiers.',                                        'EEE', '2nd'),
    ('Principles of Management',           'BBA-1101', 3.0, 'Planning, organising, leading and controlling.',                             'BBA', '1st')
) AS c (CourseName, CourseCode, Credit, Description, DeptCode, Semester)
JOIN Department d ON d.DeptCode = c.DeptCode
JOIN Semester s ON s.Semester = c.Semester;

INSERT INTO Teacher (Name, Address, ContactNo, Email, DesignationId, DepartmentId, CreditToBeTaken, RemainingCredit)
SELECT t.Name, 'Chittagong', t.ContactNo, t.Email, g.Id, d.Id, t.Credit, t.Credit
FROM (VALUES
    ('Dr. Farhana Islam', '01711000001', 'farhana@example.com',  'Professor',           'CSE', 12),
    ('Tanvir Hasan',      '01711000002', 'tanvir@example.com',   'Lecturer',            'CSE', 9),
    ('Nadia Chowdhury',   '01711000003', 'nadia@example.com',    'Assistant Professor', 'CSE', 9),
    ('Mahmudul Hasan',    '01711000004', 'mahmudul@example.com', 'Lecturer',            'EEE', 10)
) AS t (Name, ContactNo, Email, Designation, DeptCode, Credit)
JOIN Designation g ON g.Designation = t.Designation
JOIN Department d ON d.DeptCode = t.DeptCode;

INSERT INTO AssignedCourse (DepartmentId, TeacherId, CourseId, Status)
SELECT c.DepartmentId, t.Id, c.Id, 1
FROM (VALUES
    ('tanvir@example.com',   'CSE-1101'),
    ('tanvir@example.com',   'CSE-1102'),
    ('farhana@example.com',  'CSE-1103'),
    ('nadia@example.com',    'CSE-1201'),
    ('mahmudul@example.com', 'EEE-1101')
) AS a (Email, CourseCode)
JOIN Teacher t ON t.Email = a.Email
JOIN Course c ON c.CourseCode = a.CourseCode;

-- Remaining credit = credit to be taken minus the credit of the courses the teacher has.
UPDATE t
SET RemainingCredit = t.CreditToBeTaken - ISNULL((SELECT SUM(c.Credit)
                                                  FROM AssignedCourse ac
                                                  JOIN Course c ON c.Id = ac.CourseId
                                                  WHERE ac.TeacherId = t.Id AND ac.Status = 1), 0)
FROM Teacher t;

-- Times in the 12-hour format the page's time picker uses.
INSERT INTO AllocatedRooms (DepartmentId, CourseId, RoomId, Day, StartTime, FinishTime, Status)
SELECT c.DepartmentId, c.Id, r.Id, b.Day, b.StartTime, b.FinishTime, 1
FROM (VALUES
    ('CSE-1101', '101', 'Saturday',  '09:00 AM', '10:30 AM'),
    ('CSE-1101', '101', 'Monday',    '09:00 AM', '10:30 AM'),
    ('CSE-1102', '205', 'Wednesday', '10:00 AM', '01:00 PM'),
    ('CSE-1103', '102', 'Sunday',    '11:00 AM', '12:30 PM'),
    ('CSE-1201', '201', 'Tuesday',   '02:00 PM', '03:30 PM'),
    ('EEE-1101', '103', 'Sunday',    '09:00 AM', '10:30 AM')
) AS b (CourseCode, RoomNo, Day, StartTime, FinishTime)
JOIN Course c ON c.CourseCode = b.CourseCode
JOIN Room r ON r.RoomNo = b.RoomNo;

-- Registration numbers in the app's format: department letter, year, session (3 = Autumn),
-- gender (0 male, 2 female), serial. Dates are text "yyyy-mm-dd", as the app stores them.
INSERT INTO Student (Name, Email, ContactNo, Date, Address, DepartmentId, RegistrationNumber, Gender, Session)
SELECT s.Name, s.Email, s.ContactNo, '2026-08-20', 'Chittagong', d.Id, s.RegNo, s.Gender, 'Autumn'
FROM (VALUES
    ('Rafiul Karim',  'rafiul@example.com',  '01811000001', 'CSE', 'C263001', 'Male'),
    ('Mehedi Hasan',  'mehedi@example.com',  '01811000002', 'CSE', 'C263002', 'Male'),
    ('Sumaiya Akter', 'sumaiya@example.com', '01811000003', 'CSE', 'C263201', 'Female'),
    ('Tasnim Rahman', 'tasnim@example.com',  '01811000004', 'CSE', 'C263202', 'Female'),
    ('Arif Hossain',  'arif@example.com',    '01811000005', 'EEE', 'E263001', 'Male')
) AS s (Name, Email, ContactNo, DeptCode, RegNo, Gender)
JOIN Department d ON d.DeptCode = s.DeptCode;

INSERT INTO EnrolledCourses (StudentId, CourseId, DepartmentId, Date, Status)
SELECT st.Id, c.Id, st.DepartmentId, '2026-09-01', 1
FROM (VALUES
    ('rafiul@example.com',  'CSE-1101'),
    ('rafiul@example.com',  'CSE-1102'),
    ('rafiul@example.com',  'CSE-1103'),
    ('rafiul@example.com',  'MAT-1101'),
    ('sumaiya@example.com', 'CSE-1101'),
    ('sumaiya@example.com', 'CSE-1103'),
    ('arif@example.com',    'EEE-1101')
) AS e (Email, CourseCode)
JOIN Student st ON st.Email = e.Email
JOIN Course c ON c.CourseCode = e.CourseCode;

INSERT INTO Result (StudentId, DepartmentId, CourseId, GradeId)
SELECT st.Id, st.DepartmentId, c.Id, g.Id
FROM (VALUES
    ('rafiul@example.com',  'CSE-1101', 'A'),
    ('rafiul@example.com',  'CSE-1102', 'A+'),
    ('rafiul@example.com',  'CSE-1103', 'B+'),
    ('sumaiya@example.com', 'CSE-1101', 'A-'),
    ('arif@example.com',    'EEE-1101', 'B')
) AS r (Email, CourseCode, Grade)
JOIN Student st ON st.Email = r.Email
JOIN Course c ON c.CourseCode = r.CourseCode
JOIN Grade g ON g.Grade = r.Grade;

COMMIT;
