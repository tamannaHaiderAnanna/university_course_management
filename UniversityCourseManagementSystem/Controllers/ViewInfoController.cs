using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Controllers
{
    public class ViewInfoController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();
        AllocatedClassroomManager allocatedClassroomManager = new AllocatedClassroomManager();
        public ActionResult ScheduleInfo()
        {
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }

        [HttpPost]
        public ActionResult ScheduleInfo(ViewScheduleInfo aScheduleInfo)
        {
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }

        public JsonResult GetAllScheduleInfo(int departmentId)
        {
            var courses = allocatedClassroomManager.GetAllInfo();
            var courseList = courses.Where(c => c.DepartmentId == departmentId).ToList();
            return Json(courseList);
        }

	}
}