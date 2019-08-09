using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string ContactNo { get; set; }

        public string Email { get; set; }
        public int DesignationId { get; set; }
        public int DepartmentId { get; set; }

        public decimal CreditToBeTaken { get; set; }

        public decimal RemainingCredit { get; set; }
    }
}