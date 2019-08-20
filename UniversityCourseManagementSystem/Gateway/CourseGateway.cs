using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Gateway
{
    public class CourseGateway : BaseGateway
    {
        public List<Semester> GetAllSemesters()
        {
            string query = "SELECT * FROM Semester";

            Command = new SqlCommand(query, Connection);

            Connection.Open();

            Reader = Command.ExecuteReader();

            List<Semester> semesters = new List<Semester>();

            while (Reader.Read())
            {
                Semester aSemester = new Semester();
                aSemester.Id = (int)Reader["Id"];
                aSemester.Name = Reader["Semester"].ToString();

                semesters.Add(aSemester);
            }

            Reader.Close();
            Connection.Close();

            return semesters;
        }
        public int Save(Course aCourse)
        {
            string query = "INSERT INTO Course VALUES(@name,@code,@credit,@description,@department,@semester)";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = aCourse.CourseName;

            Command.Parameters.Add("code", SqlDbType.VarChar);
            Command.Parameters["code"].Value = aCourse.CourseCode;

            Command.Parameters.Add("credit", SqlDbType.Decimal);
            Command.Parameters["credit"].Value = aCourse.Credit;

            Command.Parameters.Add("description", SqlDbType.VarChar);
            Command.Parameters["description"].Value = aCourse.Description;


            Command.Parameters.Add("department", SqlDbType.Int);
            Command.Parameters["department"].Value = aCourse.DepartmentId;

            Command.Parameters.Add("semester", SqlDbType.Int);
            Command.Parameters["semester"].Value = aCourse.SemesterId;


            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }
        public bool IsCodeExists(Course aCourse)
        {
            string query = "SELECT * FROM Course WHERE CourseCode = '" + aCourse.CourseCode + "' ";

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
        public bool IsNameExists(Course aCourse)
        {
           
            string query = "SELECT * FROM Course WHERE CourseName = '" + aCourse.CourseName + "' ";

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
        public List<Course> GetAllCourses(int departmentId)
        {
            string query = "SELECT * FROM Course WHERE DepartmentId = '"+ departmentId +"'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<Course> courses = new List<Course>();
            while (Reader.Read())
            {
                Course aCourse = new Course();
                aCourse.Id = (int)Reader["Id"];
                aCourse.CourseName = Reader["CourseName"].ToString();
                aCourse.CourseCode = Reader["CourseCode"].ToString();
                aCourse.Description = Reader["Description"].ToString();
                aCourse.Credit = (decimal)Reader["Credit"];
                aCourse.DepartmentId = (int)Reader["DepartmentId"];
                aCourse.SemesterId = (int)Reader["SemesterId"];

                courses.Add(aCourse);
            }

            Reader.Close();
            Connection.Close();
            return courses;
        }
        public Course GetCourseNameAndCredit(int courseId)
        {
            string query = "SELECT CourseName,Credit FROM Course WHERE Id = '" + courseId + "'";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Course course = null;

            if (Reader.HasRows)
            {
                course = new Course();
                Reader.Read();

                course.CourseName = Reader["CourseName"].ToString();
                course.Credit = (decimal)Reader["Credit"];
            }

            Reader.Close();
            Connection.Close();

            return course;
        }
    }
}