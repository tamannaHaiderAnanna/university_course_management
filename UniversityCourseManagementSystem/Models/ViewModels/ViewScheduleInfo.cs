using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace UniversityCourseManagementSystem.Models.ViewModels
{
    public class ViewScheduleInfo
    {
        public int DepartmentId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string ScheduleInfo { get; set; }
        public string Status { get; set; }
    }
}