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

        // Undoes a transaction after an error and closes the connection. SQL Server may already have
        // rolled the transaction back itself (then transaction.Connection is null), so only roll back if it is still open.
        protected void RollbackAndClose(SqlTransaction transaction)
        {
            if (transaction.Connection != null)
            {
                transaction.Rollback();
            }
            Connection.Close();
        }
    }
}