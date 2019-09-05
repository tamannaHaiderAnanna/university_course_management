using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class AssignedCourseController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();
        TeacherManager aTeacherManager = new TeacherManager();
        CourseManager aCourseManager = new CourseManager();
        AssignedCourseManager aAssignedCourseManager = new AssignedCourseManager();
        public ActionResult AssignedCourse()
        {
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }

        [HttpPost]

        public ActionResult AssignedCourse(AssignedCourse assignedCourse)
        {
            
            //ViewBag.Update = aAssignedCourseManager.Update(assignedCourse);
            ViewBag.Message = aAssignedCourseManager.Save(assignedCourse);
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }

        public JsonResult GetTeacherByDepartmentId(int departmentId)
        {
            var teachers = aTeacherManager.GetAllTeachers();
            var teacherList = teachers.Where(t => t.DepartmentId == departmentId);

            return Json(teacherList);
        }

        public JsonResult GetCourseByDepartmentId(int departmentId)
        {
            var courseList = aCourseManager.GetAllCourses(departmentId);
            return Json(courseList);
        }

        public JsonResult GetCreditToBeTaken(int teacherId)
        {
            var creditTobeTaken = aTeacherManager.GetCreditToBeTaken(teacherId);

            return Json(creditTobeTaken);
        }

        public JsonResult GetCourseNameAndCredit(int courseId)
        {
            var course = aCourseManager.GetCourseNameAndCredit(courseId);

            return Json(course);
        }
	}
}