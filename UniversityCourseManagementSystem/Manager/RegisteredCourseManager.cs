using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Manager
{
    public class RegisteredCourseManager 
    {
        RegisteredCourseGateway aRegisteredCourseGateway = new RegisteredCourseGateway();

        public string Save(RegisteredCourse aRegisteredCourse)
        {
            aRegisteredCourse.Status = true;
            if (aRegisteredCourseGateway.CheckIfEnrolledSameCourseBySamestudent(aRegisteredCourse))
            {
                return "You already enroll this course";
            }
            else
            {
                int rowAffected = aRegisteredCourseGateway.Save(aRegisteredCourse);
                if (rowAffected > 0)
                {
                    return "Enrolled Successful";
                }
                else
                {
                    return "Enrolling Failed";
                }
            }
          
        }
        public List<Student> GetRegistrationNo()
        {
            return aRegisteredCourseGateway.GetRegistrationNo();
        }

        public ViewStudentModel GetStudentInfoWithDeptName(int studentId)
        {
            return aRegisteredCourseGateway.GetStudentInfoWithDeptName(studentId);
        }

        public List<ViewStudentModel> GetCourseName(int studentId)
        {
            return aRegisteredCourseGateway.GetCourseName(studentId);
        }
    }
}