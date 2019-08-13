using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystem.Models
{
    public class StudentResult
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        public int GradeId { get; set; }
    }
}