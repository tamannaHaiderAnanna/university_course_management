using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.xml.xmp;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Manager
{
    public class AssignedCourseManager
    {
        AssignedCourseGateway aAssignedCourseGateway = new AssignedCourseGateway();
        //TeacherManager aTeacherManager = new TeacherManager();
        public string Save(AssignedCourse assignedCourse)
        {
            if ((aAssignedCourseGateway.IsCourseExists(assignedCourse)) && (aAssignedCourseGateway.CheckStatus(assignedCourse)))
            {
                return "This course is already assigned";
            }
            if ((aAssignedCourseGateway.IsCourseExists(assignedCourse)) && (aAssignedCourseGateway.CheckStatus(assignedCourse) == false))
            {
                assignedCourse.Status = true;
                int rowAffected = aAssignedCourseGateway.UpdateStatus(assignedCourse);
                var rowAffected1 = aAssignedCourseGateway.Update(assignedCourse);
                if (rowAffected > 0)
                {
                    return "Course update successfully";
                }
                return "Updating Failed";
            }
            else
            {
                assignedCourse.Status = true;
                int rowAffected = aAssignedCourseGateway.Save(assignedCourse);
                var rowAffected1 = aAssignedCourseGateway.Update(assignedCourse);
                if (rowAffected > 0)
                {
                    return "Course assingn successful";
                }
                return "Save failed";
            }

        }

        //public decimal Update(AssignedCourse assignedCourse)
        //{
        //    if (aAssignedCourseGateway.IsCourseExists(assignedCourse))
        //    {
        //        return assignedCourse.RemainingCredit;
        //    }
        //    else
        //    {
        //        var rowAffected = aAssignedCourseGateway.Update(assignedCourse);
        //        if (rowAffected > 0)
        //        {
        //            return assignedCourse.RemainingCredit - assignedCourse.Credit;
        //        }
        //        else
        //        {
        //            return assignedCourse.RemainingCredit;
        //        }
        //    }

        //}

        public List<ViewCourseModel> GetAllCoursesWithSemesterAndStatus(int departmentId)
        {
            return aAssignedCourseGateway.GetAllCoursesWithSemesterAndStatus(departmentId);
        }

        public string UnassignedCourses()
        {
            if (aAssignedCourseGateway.UnassignedCourses() > 0)
            {
                return "All courses unssign successfully";
            }
            return "Unassigning Failed";
        }

        public string UpdateCredit()
        {
            int rowAffected = aAssignedCourseGateway.UpdateRemainingCredit();
            if (rowAffected > 0)
            {
                return "Update Successful";
            }
            return "Updating Failed";
        }
    }
}