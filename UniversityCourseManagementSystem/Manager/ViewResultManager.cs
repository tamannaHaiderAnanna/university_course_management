using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Controllers;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Manager
{
    public class ViewResultManager
    {
        ViewResultGateway aViewResultGateway = new ViewResultGateway();
        public List<ViewResultModel> GetStudentResult(int studentId)
        {
            return aViewResultGateway.GetStudentResult(studentId);
        }

       
    }
}