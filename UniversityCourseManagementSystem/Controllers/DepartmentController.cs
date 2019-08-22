using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class DepartmentController : Controller
    {
     
        DepartmentManager aDepartmentManager = new DepartmentManager();
        public ActionResult Department()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Department(Department aDepartment)
        {
            ViewBag.Message = aDepartmentManager.Save(aDepartment);
            return View();
        }
	}
}