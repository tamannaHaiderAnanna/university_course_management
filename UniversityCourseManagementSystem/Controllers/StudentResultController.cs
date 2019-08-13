using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class StudentResultController : Controller
    {   
        RegisteredCourseManager aRegisteredCourseManager = new RegisteredCourseManager();
        StudentResultManager aStudentResultManager = new StudentResultManager();
        public ActionResult StudentResult()
        {
            ViewBag.Grades = aStudentResultManager.GetAllGrades();
            ViewBag.RegistrationNo = aRegisteredCourseManager.GetRegistrationNo();
            return View();
        }

        [HttpPost]
        public ActionResult StudentResult(StudentResult studentResult)
        {
            ViewBag.Message = aStudentResultManager.Save(studentResult);
            ViewBag.RegistrationNo = aRegisteredCourseManager.GetRegistrationNo();
            ViewBag.Grades = aStudentResultManager.GetAllGrades();
            return View();
        }
        public JsonResult GetStudentInfoWithDepartment(int studentId)
        {
            var studentInfo = aRegisteredCourseManager.GetStudentInfoWithDeptName(studentId);

            return Json(studentInfo);
        }
        public JsonResult GetEnrolledCourses(int studentId)
        {
            var studentInfo = aStudentResultManager.GetEnrolledCourses(studentId);

            return Json(studentInfo);
        }
	}
}