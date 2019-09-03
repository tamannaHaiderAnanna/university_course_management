using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Microsoft.Owin.Security.Facebook;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Manager
{
   
    public class AllocatedClassroomManager
    {
        AllocatedClassroomGateway allocatedClassroomGateway = new AllocatedClassroomGateway();

        public string Save(AllocatedClassroom aClassroom)
        {
            //aClassroom.Status = true;
            if ((allocatedClassroomGateway.CheckRoomAndDay(aClassroom)) && (allocatedClassroomGateway.CheckTimeSchedule(aClassroom)))
            {
               
                    return "This room is already allocated"; 
            }
            //if ((allocatedClassroomGateway.CheckRoomAndDay(aClassroom)) && (allocatedClassroomGateway.CheckTimeSchedule(aClassroom) == false) && (allocatedClassroomGateway.CheckSameCourseAndDepartment(aClassroom)))
            //{
            //    int rowAffected = allocatedClassroomGateway.UpdateStatus(aClassroom);
            //    int rowAffected1 = allocatedClassroomGateway.UpdateSameCourse(aClassroom);
            //    if (rowAffected > 0 || rowAffected1 > 0)
            //    {
            //        return "Course Update successfully";
            //    }
            //    return "Updating Failed";
            //}
            else
            {
                aClassroom.Status = true;
                int rowAffected = allocatedClassroomGateway.Save(aClassroom);
                if (rowAffected > 0)
                {
                    return "Room allocated successfully";
                }
                return "Allocating failed";
            }
        }
        public List<AllocatedClassroom> GetAllRooms()
        {
            return allocatedClassroomGateway.GetAllRooms();
        }

        public List<string> GetDays()
        {
            return allocatedClassroomGateway.GetAllDays();
        }

        public List<ViewScheduleInfo> GetAllInfo()
        {
            return allocatedClassroomGateway.GetAllInfo();
        }

        public string UnallocatedClassrooms()
        {
            int rowAffected = allocatedClassroomGateway.UnallocatedClassrooms();
            if (rowAffected > 0)
            {
                return "All classrooms are unallocated successfully";
            }
            return "Unallocating Failed";
        }
    }
}