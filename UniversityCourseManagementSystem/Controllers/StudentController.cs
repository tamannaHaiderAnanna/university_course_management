using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.UI;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();
        StudentManager aStudentManager = new StudentManager();
       
        public ActionResult Student()
        {
            ViewBag.Departments = aDepartmentManager.GetDepartmentsForDropDownList();
            ViewBag.Sessions = GetSessionsForDropDownList();
            return View();
        }

        [HttpPost] 
        
        public ActionResult Student(Student aStudent)
        {
            string registrationPrefix = GetRegistrationPrefix(aStudent);
            ViewBag.Departments = aDepartmentManager.GetDepartmentsForDropDownList();
            ViewBag.Sessions = GetSessionsForDropDownList();
            ViewData["Student"] = aStudent;
            ViewBag.Message = aStudentManager.Save(aStudent, registrationPrefix);
            return PartialView("ViewStudentInfo");
        }
        public ActionResult ViewStudentInfo()
        {
            ViewBag.Student = ViewData["Student"];
            ViewBag.Department = ViewData["Department"];
            return View("Student");
        }
        
        // Registration number = first letter of the department code + last two digits of the year
        // + session (1 Spring, 3 Autumn) + gender (0 male, 2 female) + a serial number, e.g. C191001.
        // Only the prefix is built here. The serial is picked when the student is saved, in the same
        // transaction as the insert (see StudentGateway.Save).
        private string GetRegistrationPrefix(Student student)
        {
            int n = 0;
            int s = 1;
            Department aDepartment = aStudentManager.GetDepartmentById(student.DepartmentId);
            ViewData["Department"] = aDepartment;
            if (student.Gender == "Female")
            {
                n = 2;
            }
            if (student.SessionName == "Autumn")
            {
                s = 3;
            }
            return aDepartment.DeptCode.Substring(0,1) + student.Date.Substring(8,2) + s + n;
        }
        private List<SelectListItem> GetSessionsForDropDownList()
        {
            var sessions = GetAllSessions();

            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() {Value = "", Text = "--Select--"}
            };

            foreach (Student item in sessions)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Value = item.SessionName,
                    Text = item.SessionName
                };
                items.Add(selectListItem);
            }
            return items;
        }

        private List<Student> GetAllSessions()
        {
            List<Student> sessions = new List<Student>()
            {
                new Student() {SessionId = 1, SessionName = "Spring"},
                new Student() {SessionId = 3, SessionName = "Autumn"}
            };
            return sessions;
        }
	}
}