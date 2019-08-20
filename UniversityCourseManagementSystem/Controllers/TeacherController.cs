using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class TeacherController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();
        TeacherManager aTeacherManager = new TeacherManager();
        public ActionResult Index()
        {
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            ViewBag.Designation = aTeacherManager.GetAllDesignations();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Teacher aTeacher)
        {
            ViewBag.Message = aTeacherManager.Save(aTeacher);
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            ViewBag.Designation = aTeacherManager.GetAllDesignations();
            return View();
        }
	}
}