using System;
using System.Collections.Generic;
using System.Globalization;
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
            // The time picker sends 12-hour times such as "01:00 PM", so compare them as times, not as text
            // (as text, "01:00 PM" comes before "12:00 PM").
            DateTime fromTime;
            DateTime toTime;
            if (!DateTime.TryParse(aClassroom.FromTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromTime) ||
                !DateTime.TryParse(aClassroom.ToTime, CultureInfo.InvariantCulture, DateTimeStyles.None, out toTime))
            {
                return "Please enter a valid time";
            }
            if (fromTime.TimeOfDay >= toTime.TimeOfDay)
            {
                return "From time must be earlier than to time";
            }

            aClassroom.Status = true;
            if (allocatedClassroomGateway.SaveIfRoomFree(aClassroom, fromTime.TimeOfDay, toTime.TimeOfDay))
            {
                return "Room allocated successfully";
            }
            return "This room is already allocated";
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