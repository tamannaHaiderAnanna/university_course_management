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
    public class TeacherGateway : BaseGateway
    {
        public List<Designation> GetAllDesignations()
        {
            string query = "SELECT * FROM Designation";

            Command = new SqlCommand(query, Connection);

            Connection.Open();

           Reader = Command.ExecuteReader();

            List<Designation> designations = new List<Designation>();

            while (Reader.Read())
            {
                Designation aDesignation = new Designation();
                aDesignation.Id = (int)Reader["Id"];
                aDesignation.Name = Reader["Designation"].ToString();

                designations.Add(aDesignation);
            }

            Reader.Close();
            Connection.Close();

            return designations;
        }
        public bool IsEmailExists(Teacher aTeacher)
        {

            string query = "SELECT * FROM Teacher WHERE Email = '" + aTeacher.Email + "' ";

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
        public int Save(Teacher aTeacher)
        {
            string query = "INSERT INTO Teacher VALUES(@name,@address,@contactNo,@email,@designation,@department,@creditToBeTaken,@remainingCredit)";

             Command = new SqlCommand(query, Connection);

             Command.Parameters.Clear();
             Command.Parameters.Add("name", SqlDbType.VarChar);
             Command.Parameters["name"].Value = aTeacher.Name;

             Command.Parameters.Add("address", SqlDbType.VarChar);
             Command.Parameters["address"].Value = aTeacher.Address;

             Command.Parameters.Add("email", SqlDbType.VarChar);
             Command.Parameters["email"].Value = aTeacher.Email;

             Command.Parameters.Add("contactNo", SqlDbType.VarChar);
             Command.Parameters["contactNo"].Value = aTeacher.ContactNo;

             Command.Parameters.Add("designation", SqlDbType.Int);
             Command.Parameters["designation"].Value = aTeacher.DesignationId;

             Command.Parameters.Add("department", SqlDbType.Int);
             Command.Parameters["department"].Value = aTeacher.DepartmentId;

             Command.Parameters.Add("creditToBeTaken", SqlDbType.Decimal);
             Command.Parameters["creditToBeTaken"].Value = aTeacher.CreditToBeTaken;

             Command.Parameters.Add("remainingCredit", SqlDbType.Decimal);
             Command.Parameters["remainingCredit"].Value = aTeacher.CreditToBeTaken;

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }
        public List<Teacher> GetAllTeachers()
        {
            string query = "SELECT * FROM Teacher";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<Teacher> teachers = new List<Teacher>();
            while (Reader.Read())
            {
                Teacher aTeacher = new Teacher();
                aTeacher.Id = (int)Reader["Id"];
                aTeacher.Name = Reader["Name"].ToString();
                aTeacher.Address = Reader["Address"].ToString();
                aTeacher.Email = Reader["Email"].ToString();
                aTeacher.ContactNo = Reader["ContactNo"].ToString();
                aTeacher.DepartmentId = (int)Reader["DepartmentId"];
                aTeacher.DesignationId = (int)Reader["DesignationId"];
                aTeacher.CreditToBeTaken = (decimal)Reader["CreditToBeTaken"];

                teachers.Add(aTeacher);
            }

            Reader.Close();
            Connection.Close();
            return teachers;
        }

        public Teacher GetCreditToBeTaken(int teacherId)
        {
            string query = "SELECT CreditToBeTaken,RemainingCredit FROM Teacher WHERE Id = '" + teacherId + "'";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();
            Teacher teacher = null;

            if (Reader.HasRows)
            {
                teacher = new Teacher();
                Reader.Read();

                teacher.CreditToBeTaken = (decimal)Reader["CreditToBeTaken"];
                teacher.RemainingCredit = (decimal)Reader["RemainingCredit"];
            }

            Reader.Close();
            Connection.Close();

            return teacher;
        }
    }
}