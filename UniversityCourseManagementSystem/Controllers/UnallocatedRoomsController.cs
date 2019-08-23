using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Manager;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Controllers
{
    public class UnallocatedRoomsController : Controller
    {
        AllocatedClassroomManager allocatedClassroomManager = new AllocatedClassroomManager();
        public ActionResult UnallocatedRooms()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UnallocatedRooms(AllocatedClassroom allocatedClassroom)
        {
            ViewBag.Message = allocatedClassroomManager.UnallocatedClassrooms();
            return View();
        }
	}
}