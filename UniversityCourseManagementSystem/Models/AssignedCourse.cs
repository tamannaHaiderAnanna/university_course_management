using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystem.Models
{
    public class AssignedCourse
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public bool Status { get; set; }
        public decimal Credit { get; set; }
        public decimal CreditToBeTaken { get; set; }
        public decimal RemainingCredit { get; set; }
    }
}