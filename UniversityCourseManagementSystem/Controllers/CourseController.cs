using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class CourseController : Controller
    {
        CourseManager aCourseManager = new CourseManager();
        DepartmentManager aDepartmentManager = new DepartmentManager();
        public ActionResult Index()
        {
            ViewBag.Departments = aDepartmentManager.GetDepartmentsForDropDownList();
            ViewBag.Semester = GetSemestersForDropDownList();
            return View();
        }

        [HttpPost]
        public ActionResult Index(Course aCourse)
        {
            ViewBag.Message = aCourseManager.Save(aCourse);
            ViewBag.Departments = aDepartmentManager.GetDepartmentsForDropDownList();
            ViewBag.Semester = GetSemestersForDropDownList();
            return View();
        }
        public List<SelectListItem> GetSemestersForDropDownList()
        {
            var semesters = aCourseManager.GetAllSemesters();

            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() {Value = "", Text = "--Select--"}
            };

            foreach (Semester item in semesters)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                };
                items.Add(selectListItem);
            }
            return items;
        }
	}
}