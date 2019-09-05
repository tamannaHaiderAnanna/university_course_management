using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Gateway
{
    public class StudentGateway : BaseGateway
    {
        public bool IsEmailExists(Student aStudent)
        {

            string query = "SELECT * FROM Student WHERE Email = '" + aStudent.Email + "' ";

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
        public bool IsContactNoExists(Student aStudent)
        {

            string query = "SELECT * FROM Student WHERE ContactNo = '" + aStudent.ContactNo + "' ";

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
        public int Save(Student aStudent)
        {

            string query = "INSERT INTO Student VALUES(@name,@email,@contactNo,@date,@address,@departmentId,@registrationNo,@gender,@session)";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();

            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = aStudent.Name;

            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = aStudent.Email;

            Command.Parameters.Add("contactNo", SqlDbType.VarChar);
            Command.Parameters["contactNo"].Value = aStudent.ContactNo;

            Command.Parameters.Add("date", SqlDbType.Date);
            Command.Parameters["date"].Value = aStudent.Date;

            Command.Parameters.Add("address", SqlDbType.VarChar);
            Command.Parameters["address"].Value = aStudent.Address;

            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = aStudent.DepartmentId;

            Command.Parameters.Add("registrationNo", SqlDbType.VarChar);
            Command.Parameters["registrationNo"].Value = aStudent.RegistrationNo;

            Command.Parameters.Add("gender", SqlDbType.VarChar);
            Command.Parameters["gender"].Value = aStudent.Gender;

            Command.Parameters.Add("session", SqlDbType.VarChar);
            Command.Parameters["session"].Value = aStudent.SessionName;

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }

        public List<Student> GetDepartmentId(Student student)
        {
           
             string query = "SELECT * FROM Student WHERE DepartmentId = '" + student.DepartmentId + "' AND SUBSTRING(Date,1,4) ='" + student.Date.Substring(6, 4) + "'" +
                           " AND Gender = '" + student.Gender + "' " +
                           " AND Session = '" + student.SessionName + "' ";
            
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<Student> students = new List<Student>();
            while (Reader.Read())
            {
                Student aStudent = new Student();
                aStudent.Id = (int)Reader["Id"];
                aStudent.Name = Reader["Name"].ToString();
                aStudent.Email = Reader["Email"].ToString();
                aStudent.ContactNo = Reader["ContactNo"].ToString();
                aStudent.DepartmentId = (int)Reader["DepartmentId"];
                aStudent.Address = Reader["Address"].ToString();
                aStudent.RegistrationNo = Reader["RegistrationNumber"].ToString();
                aStudent.Date = Reader["Date"].ToString();

                students.Add(aStudent);
            }

            Reader.Close();
            Connection.Close();
            return students;
        }

        public Department GetDepartmentById(int departmentId)
        {
            string query = "SELECT * FROM Department WHERE Id = '" + departmentId + "'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            Department aDepartment = null;
            if (Reader.HasRows)
            {
                aDepartment = new Department();
                Reader.Read();
                //aDepartment.Id = (int) Reader["Id"];
                aDepartment.DeptCode = Reader["DeptCode"].ToString();
                aDepartment.DeptName = Reader["DeptName"].ToString();
            }
            Reader.Close();
            Connection.Close();

            return aDepartment;
        }
    }
}