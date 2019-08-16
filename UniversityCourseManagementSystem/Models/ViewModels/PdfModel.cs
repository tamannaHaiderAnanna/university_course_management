using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;


namespace UniversityCourseManagementSystem.Models.ViewModels
{
    
    public class PdfModel
    {
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string DeptName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string GradeName { get; set; }
    }
}