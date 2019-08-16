using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Gateway
{
    public class ViewResultGateway : BaseGateway
    {
        public List<ViewResultModel> GetStudentResult(int studentId)
        {
            string query = "SELECT * FROM GetStudentResult WHERE StudentId = '" + studentId + "'";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<ViewResultModel> resultModels = new List<ViewResultModel>();
            while (Reader.Read())
            {
                ViewResultModel aModel = new ViewResultModel();
                aModel.StudentId = (int) Reader["StudentId"];
                aModel.CourseCode = Reader["CourseCode"].ToString();
                aModel.CourseName = Reader["CourseName"].ToString();
                aModel.GradeName = Reader["GradeName"].ToString();

                if (aModel.GradeName == "")
                {
                    aModel.GradeName = "Not Graded Yet";
                }

                resultModels.Add(aModel);
            }
            Reader.Close();
            Connection.Close();

            return resultModels;
        } 
    }
}