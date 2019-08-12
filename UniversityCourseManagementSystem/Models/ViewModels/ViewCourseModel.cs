using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystem.Models.ViewModels
{
    public class ViewCourseModel
    {
        public int DepartmentId { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Semester { get; set; }
        public string Status { get; set; }
        public string AssignedStatus { get; set; }
        public string TeacherName { get; set; }
    }
}