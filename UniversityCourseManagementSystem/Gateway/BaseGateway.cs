using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace UniversityCourseManagementSystem.Gateway
{
    public class BaseGateway
    {
        public SqlCommand Command { get; set; }
        public SqlConnection Connection { get; set; }
        public SqlDataReader Reader { get; set; }

        public BaseGateway()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["UniversityManagementConnectionString"].ConnectionString;
            Connection = new SqlConnection(connectionString);
        }
    }
}