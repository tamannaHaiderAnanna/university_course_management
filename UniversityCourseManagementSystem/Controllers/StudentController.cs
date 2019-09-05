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
            aStudent.RegistrationNo = GetRegistrationNumber(aStudent);
            ViewBag.Departments = aDepartmentManager.GetDepartmentsForDropDownList();
            ViewBag.Sessions = GetSessionsForDropDownList();
            ViewData["Student"] = aStudent;
            ViewBag.Message = aStudentManager.Save(aStudent);
            return PartialView("ViewStudentInfo");
        }
        public ActionResult ViewStudentInfo()
        {
            ViewBag.Student = ViewData["Student"];
            ViewBag.Department = ViewData["Department"];
            return View("Student");
        }
        
        private string GetRegistrationNumber(Student student)
        {
            int n = 0;
            int s = 1;
            Department aDepartment = aStudentManager.GetDepartmentById(student.DepartmentId);
            ViewData["Department"] = aDepartment;
            List<Student> lastStudent = aStudentManager.GetDepartmentId(student);
            Student aStudent  = lastStudent.LastOrDefault();
            if (student.Gender == "Female")
            {
                n = 2;
            }
            if (student.SessionName == "Autumn")
            {
                s = 3;
            }
            if ( aStudent == null)
            {
                student.RegistrationNo = aDepartment.DeptCode.Substring(0,1) + student.Date.Substring(8,2) + s + n + "01";
            }
            else
            {
                student.RegistrationNo = aDepartment.DeptCode.Substring(0,1) + student.Date.Substring(8,2) + s + n + 
                    (Convert.ToInt32(aStudent.RegistrationNo.Substring(6, aStudent.RegistrationNo.Length - 6)) + 1).ToString("D2");
            }
            return student.RegistrationNo;
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