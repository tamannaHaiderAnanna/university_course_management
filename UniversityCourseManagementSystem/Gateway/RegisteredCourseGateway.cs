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
    public class RegisteredCourseGateway : BaseGateway
    {

        public List<Student> GetRegistrationNo()
        {
            string query = "SELECT * FROM Student";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<Student> students = new List<Student>();
            while (Reader.Read())
            {
                Student aStudent = new Student();
                aStudent.Id = (int)Reader["Id"];
                aStudent.RegistrationNo = Reader["RegistrationNumber"].ToString();

                students.Add(aStudent);
            }

            Reader.Close();
            Connection.Close();
            return students;
        }

        public ViewStudentModel GetStudentInfoWithDeptName(int studentId)
        {
            string query = "SELECT * FROM GetStudentInfoWithDeptName WHERE Id = '"+ studentId +"'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            ViewStudentModel aStudentModel = null;
            if (Reader.HasRows)
            {
                aStudentModel = new ViewStudentModel();
                Reader.Read();

                aStudentModel.StudentId = (int)Reader["Id"];
                aStudentModel.DepartmentId = (int) Reader["DepartmentId"];
                aStudentModel.StudentName = Reader["Name"].ToString();
                aStudentModel.DeptName = Reader["DeptName"].ToString();
                aStudentModel.Email = Reader["Email"].ToString();
            }
            Reader.Close();
            Connection.Close();

            return aStudentModel;
        }
        public bool CheckIfEnrolledSameCourseBySamestudent(RegisteredCourse aRegisteredCourse)
        {

            string query = "SELECT * FROM EnrolledCourses WHERE CourseId = '" + aRegisteredCourse.CourseId + "' AND StudentId = '"+ aRegisteredCourse.StudentId +"' ";

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
        public List<ViewStudentModel> GetCourseName(int studentId)
        {
            string query = "SELECT * FROM GetCourseName WHERE StudentId = '" + studentId + "'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<ViewStudentModel> courses = new List<ViewStudentModel>();
            while (Reader.Read())
            {
                ViewStudentModel aStudentModel = new ViewStudentModel();

                aStudentModel.StudentId = (int)Reader["StudentId"];
                aStudentModel.CourseId = (int) Reader["CourseId"];
                aStudentModel.CourseName = Reader["CourseName"].ToString();

                courses.Add(aStudentModel);
            }
            Reader.Close();
            Connection.Close();

            return courses;
        }

        public int Save(RegisteredCourse aRegisteredCourse)
        {
            string query = "INSERT INTO EnrolledCourses VALUES(@studentId,@courseId,@departmentId,@date,@status)";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("studentId", SqlDbType.Int);
            Command.Parameters["studentId"].Value = aRegisteredCourse.StudentId;

            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = aRegisteredCourse.CourseId;

            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = aRegisteredCourse.DepartmentId;

            Command.Parameters.Add("date", SqlDbType.Date);
            Command.Parameters["date"].Value = aRegisteredCourse.Date;

            Command.Parameters.Add("status", SqlDbType.Bit);
            Command.Parameters["status"].Value = aRegisteredCourse.Status;

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }
    }
}