using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace UniversityCourseManagementSystem.Models
{
    public class AllocatedClassroom
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter a department!")]
        [Display(Name = "Dapartment")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Please enter a course!")]
        [Display(Name = "Course")]
        public int CourseId { get; set; }
        [Required(ErrorMessage = "Please enter a room no!")]
        [Display(Name = "Room No")]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Please enter a day of week!")]
        public string Day { get; set; }
        [Required(ErrorMessage = "Please enter from time!")]
        [Display(Name = "From Time")]
        public string FromTime { get; set; }
        [Required(ErrorMessage = "Please enter to time!")]
        [Display(Name = "To Time")]
        public string ToTime { get; set; }

        public string RoomNo { get; set; }
        public bool Status { get; set; }
    }
}