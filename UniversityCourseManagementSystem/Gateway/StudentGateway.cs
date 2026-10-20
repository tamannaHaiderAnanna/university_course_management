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

            string query = "SELECT * FROM Student WHERE Email = @email";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("email", SqlDbType.VarChar);
            Command.Parameters["email"].Value = aStudent.Email;

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

            string query = "SELECT * FROM Student WHERE ContactNo = @contactNo";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("contactNo", SqlDbType.VarChar);
            Command.Parameters["contactNo"].Value = aStudent.ContactNo;

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
        // Picks the next free registration number for this prefix and saves the student, in one
        // transaction, so two students registered at the same moment can't get the same number.
        public int Save(Student aStudent, string registrationPrefix)
        {
            Connection.Open();
            SqlTransaction transaction = Connection.BeginTransaction();
            try
            {
                // Highest serial already used with this prefix, in any department (departments can share a
                // first letter, e.g. CSE and CE). UPDLOCK + HOLDLOCK keep other registrations waiting until
                // this one is saved.
                string query = "SELECT MAX(TRY_CAST(SUBSTRING(RegistrationNumber, @serialStart, 10) AS INT)) " +
                               "FROM Student WITH (UPDLOCK, HOLDLOCK) WHERE LEFT(RegistrationNumber, @prefixLength) = @prefix";

                Command = new SqlCommand(query, Connection, transaction);

                Command.Parameters.Clear();
                Command.Parameters.Add("serialStart", SqlDbType.Int);
                Command.Parameters["serialStart"].Value = registrationPrefix.Length + 1;

                Command.Parameters.Add("prefixLength", SqlDbType.Int);
                Command.Parameters["prefixLength"].Value = registrationPrefix.Length;

                Command.Parameters.Add("prefix", SqlDbType.VarChar);
                Command.Parameters["prefix"].Value = registrationPrefix;

                object lastSerial = Command.ExecuteScalar();
                int serial = 1;
                if (lastSerial != DBNull.Value)
                {
                    serial = (int)lastSerial + 1;
                }
                aStudent.RegistrationNo = registrationPrefix + serial.ToString("D2");

                query = "INSERT INTO Student VALUES(@name,@email,@contactNo,@date,@address,@departmentId,@registrationNo,@gender,@session)";

                Command = new SqlCommand(query, Connection, transaction);

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

                int rowAffected = Command.ExecuteNonQuery();

                transaction.Commit();
                Connection.Close();

                return rowAffected;
            }
            catch
            {
                RollbackAndClose(transaction);
                throw;
            }
        }

        public Department GetDepartmentById(int departmentId)
        {
            string query = "SELECT * FROM Department WHERE Id = @departmentId";
            Command = new SqlCommand(query, Connection);
            Command.Parameters.Clear();
            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = departmentId;
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