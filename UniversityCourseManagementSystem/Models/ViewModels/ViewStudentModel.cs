using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystem.Models.ViewModels
{
    public class ViewStudentModel
    {
        public int DepartmentId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string CourseName { get; set; }
        public string Email { get; set; }
        public string DeptName { get; set; }
        public int CourseId { get; set; }
    }
}