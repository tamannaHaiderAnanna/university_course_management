using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Manager
{
    public class CourseManager
    {
        CourseGateway aCourseGateway = new CourseGateway();

        public List<Semester> GetAllSemesters()
        {
            return aCourseGateway.GetAllSemesters();
        }
        public string Save(Course aCourse)
        {
            if (aCourseGateway.IsCodeExists(aCourse) && aCourseGateway.IsNameExists(aCourse))
            {
                return "The code and the Name are already assigned";
            }

            if (aCourseGateway.IsCodeExists(aCourse))
            {
                return "This code is already assigned";
            }
            if (aCourseGateway.IsNameExists(aCourse))
            {
                return "This name is already assigned";
            }

            else
            {
                int rowAffected = aCourseGateway.Save(aCourse);
                if (rowAffected > 0)
                {
                    return "Course saved";
                }
                return "Save failed";
            }


        }
        public List<Course> GetAllCourses(int departmentId)
        {
            return aCourseGateway.GetAllCourses(departmentId);
        }
       
        public Course GetCourseNameAndCredit(int courseId)
        {
            return aCourseGateway.GetCourseNameAndCredit(courseId);
        }
    }
}