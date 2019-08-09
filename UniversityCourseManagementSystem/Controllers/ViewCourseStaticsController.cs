using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;

namespace UniversityCourseManagementSystem.Controllers
{
    public class ViewCourseStaticsController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();
        AssignedCourseManager aAssignedCourseManager = new AssignedCourseManager();
        public ActionResult CourseStatics()
        {
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public ActionResult CourseStatics(int id)
        {
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }
        public JsonResult GetAllCoursesWithSemesterAndStatus(int departmentId)
        {
            var courses = aAssignedCourseManager.GetAllCoursesWithSemesterAndStatus(departmentId);
            //var courseList = courses.Where(c => c.DepartmentId == departmentId).ToList();
            return Json(courses);
        }
	}
}