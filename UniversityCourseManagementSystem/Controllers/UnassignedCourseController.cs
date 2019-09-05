using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class UnassignedCourseController : Controller
    {
       AssignedCourseManager aCourseManager = new AssignedCourseManager();
        public ActionResult UnassignedCourse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UnassignedCourse(Course aCourse)
        {

            ViewBag.Update = aCourseManager.UpdateCredit();
            ViewBag.Message = aCourseManager.UnassignedCourses();
            return View();
        }
	}
}