using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Gateway
{
    public class StudentResultGateway : BaseGateway
    {
        public List<Grade> GetAllGrades()
        {
            string query = "SELECT * FROM Grade";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<Grade> grades = new List<Grade>();
            while (Reader.Read())
            {
                Grade aGrade = new Grade();
                aGrade.Id = (int)Reader["Id"];
                aGrade.GradeName = Reader["Grade"].ToString();

                grades.Add(aGrade);
            }

            Reader.Close();
            Connection.Close();
            return grades;
        }
        public List<ViewStudentModel> GetEnrolledCourses(int studentId)
        {
            string query = "SELECT * FROM GetEnrolledCourses WHERE StudentId = '" + studentId + "'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<ViewStudentModel> courses = new List<ViewStudentModel>();
            while (Reader.Read())
            {
                ViewStudentModel aStudentModel = new ViewStudentModel();

                aStudentModel.StudentId = (int)Reader["StudentId"];
                aStudentModel.CourseId = (int)Reader["CourseId"];
                aStudentModel.CourseName = Reader["CourseName"].ToString();

                courses.Add(aStudentModel);
            }
            Reader.Close();
            Connection.Close();

            return courses;
        }
        public bool CheckResultOfSameCourseBySameStudent(StudentResult studentResult)
        {

            string query = "SELECT * FROM Result WHERE CourseId = '" + studentResult.CourseId + "' AND StudentId = '" + studentResult.StudentId + "' ";

            Command = new SqlCommand(query, Connection);

            Connection.Open();

            Reader = Command.ExecuteReader();

            bool hasRow = false;

            if (Reader.HasRows)
            {
                hasRow = true;
            }

            Reader.Close();
            Connection.Close();

            return hasRow;
        }

        public int Save(StudentResult studentResult)
        {
            string query = "INSERT INTO  Result VALUES(@studentId,@departmentId,@courseId,@gradeId)";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();

            Command.Parameters.Add("studentId", SqlDbType.Int);
            Command.Parameters["studentId"].Value = studentResult.StudentId;

            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = studentResult.DepartmentId;

            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = studentResult.CourseId;

            Command.Parameters.Add("gradeId", SqlDbType.Int);
            Command.Parameters["gradeId"].Value = studentResult.GradeId;

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }

        public int UpdateResult(StudentResult studentResult)
        {
            string query =
                 "UPDATE Result SET GradeId = '"+ studentResult.GradeId +"'  WHERE StudentId = '" + studentResult.StudentId + "' AND CourseId = '"+ studentResult.CourseId +"'";

            Command = new SqlCommand(query, Connection);

            Connection.Open();

            var rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }
    }
}