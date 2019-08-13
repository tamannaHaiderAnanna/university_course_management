using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class RegisteredCourseController : Controller
    {
        RegisteredCourseManager aRegisteredCourseManager = new RegisteredCourseManager();
        public ActionResult RegisteredCourse()
        {
            ViewBag.RegistrationNo = aRegisteredCourseManager.GetRegistrationNo();
            return View();
        }

        [HttpPost]
        public ActionResult RegisteredCourse(RegisteredCourse aRegisteredCourse)
        {
            ViewBag.Message = aRegisteredCourseManager.Save(aRegisteredCourse);
            ViewBag.RegistrationNo = aRegisteredCourseManager.GetRegistrationNo();
            return View();
        }
        public JsonResult GetStudentInfoWithDeptName(int studentId)
        {
            var studentInfo = aRegisteredCourseManager.GetStudentInfoWithDeptName(studentId);

            return Json(studentInfo);
        }
        public JsonResult GetCourseName(int studentId)
        {
            var studentInfo = aRegisteredCourseManager.GetCourseName(studentId);

            return Json(studentInfo);
        }

    }
}