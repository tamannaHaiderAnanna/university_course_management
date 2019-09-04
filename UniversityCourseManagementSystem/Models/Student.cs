using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace UniversityCourseManagementSystem.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter name!")]
        [StringLength(50,MinimumLength = 5,ErrorMessage = "Name must be between 5 to 50 character")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter address!")]
        public string Address { get; set; }
        [RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$",ErrorMessage = "Please enter a valid contact no!")]
        [Required(ErrorMessage = "Please enter contact no!")]
        [Display(Name = "Contact No")]
        [StringLength(15,MinimumLength = 11,ErrorMessage = "Contact no must be between 11 to 15 number")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Please enter email!")]
        [StringLength(50, ErrorMessage = "Email can not exceed 20 characters")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$", ErrorMessage = "Please enter a valid email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter date!")]
        public string Date { get; set; }
        [Required(ErrorMessage = "Please enter a department!")]
        [Display(Name = "Dapartment")]
        public int DepartmentId { get; set; }
        public string RegistrationNo { get; set; }
        [Required(ErrorMessage = "Please select gender")]
        [StringLength(6)]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please enter session")]
        [Display(Name = "Session")]
        public string SessionName { get; set; }
        public int SessionId { get; set; }
       
    }
}