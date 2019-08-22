using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystem.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter course code")]
        [MinLength(5, ErrorMessage = "Code must be at least five character")]
        [Display(Name="Course Code")]
        public string CourseCode { get; set; }

        [Required(ErrorMessage = "Please enter course name")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Please enter credit")]
        [Range(0.5, 5, ErrorMessage = "Credit must be between .5 to 5")]

        public decimal Credit { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter department")]
        [Display(Name= "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Please enter semester")]
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }
    }
}