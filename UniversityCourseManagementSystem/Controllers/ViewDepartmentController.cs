using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;

namespace UniversityCourseManagementSystem.Controllers
{
    public class ViewDepartmentController : Controller
    {
       DepartmentManager aDepartmentManager = new DepartmentManager();
        public ActionResult ViewDepartment()
        {
            ViewBag.Departments = aDepartmentManager.GetAllDepartments();
            return View();
        }
	}
}