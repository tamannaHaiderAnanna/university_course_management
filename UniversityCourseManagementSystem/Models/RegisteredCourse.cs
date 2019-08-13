using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystem.Models
{
    public class RegisteredCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public string Date { get; set; }
        public bool Status { get; set; }
    }
}