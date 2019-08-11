using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class AllocatedClassroomController : Controller
    {
        DepartmentManager aDepartmentManager = new DepartmentManager();
        CourseManager aCourseManager = new CourseManager();
        AllocatedClassroomManager aAllocatedClassroomManager = new AllocatedClassroomManager();
        public ActionResult AllocatedClassroom()
        {
            ViewBag.Days = GetDaysForDropDownList();
            ViewBag.Rooms = GetRoomsForDropDownList();
            ViewBag.Departments = aDepartmentManager.GetDepartmentsForDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult AllocatedClassroom(AllocatedClassroom aClassroom)
        {
            ViewBag.Message = aAllocatedClassroomManager.Save(aClassroom);
            ViewBag.Days = GetDaysForDropDownList();
            ViewBag.Rooms = GetRoomsForDropDownList();
            ViewBag.Departments = aDepartmentManager.GetDepartmentsForDropDownList();
            return View();
        }
        public JsonResult GetCourseByDepartmentId(int departmentId)
        {
            var courses = aCourseManager.GetAllCourses(departmentId);

            return Json(courses);
        }
        public List<SelectListItem> GetDaysForDropDownList()
        {
            var days = aAllocatedClassroomManager.GetDays();

            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem() {Value = "", Text = "--Select--"}
            };

            foreach (string day in days)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Value = day,
                    Text = day
                };
                list.Add(selectListItem);
            }
            return list;
        }
        public List<SelectListItem> GetRoomsForDropDownList()
        {
            var roooms = aAllocatedClassroomManager.GetAllRooms();

            List<SelectListItem> list = new List<SelectListItem>()
            {
                new SelectListItem() {Value = "", Text = "--Select--"}
            };

            foreach (AllocatedClassroom room in roooms)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Value = room.RoomId.ToString(),
                    Text = room.RoomNo
                };
                list.Add(selectListItem);
            }
            return list;
        }
	}
}