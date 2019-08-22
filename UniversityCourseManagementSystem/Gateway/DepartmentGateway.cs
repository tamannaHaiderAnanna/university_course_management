using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Gateway
{
    public class DepartmentGateway : BaseGateway
    {
        public List<Department> GetAllDepartments()
        {
            string query = "SELECT * FROM Department";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<Department> departments = new List<Department>();
            while (Reader.Read())
            {
                Department aDepartment = new Department();
                aDepartment.Id = (int)Reader["Id"];
                aDepartment.DeptCode = Reader["DeptCode"].ToString();
                aDepartment.DeptName = Reader["DeptName"].ToString();

                departments.Add(aDepartment);
            }

            Reader.Close();
            Connection.Close();
            return departments;
        }

        public bool IsDeptCodeExist(Department aDepartment)
        {
            string query = "SELECT * FROM Department WHERE DeptCode = '" + aDepartment.DeptCode + "' ";

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

        public bool IsDeptNameExist(Department aDepartment)
        {
            string query = "SELECT * FROM Department WHERE DeptName = '" + aDepartment.DeptName + "' ";

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

        public int Save(Department aDepartment)
        {
            string query = "INSERT INTO Department VALUES(@name,@code)";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("name", SqlDbType.VarChar);
            Command.Parameters["name"].Value = aDepartment.DeptName;

            Command.Parameters.Add("code", SqlDbType.VarChar);
            Command.Parameters["code"].Value = aDepartment.DeptCode;

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }
    }
}