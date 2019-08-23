using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Manager
{
    public class StudentResultManager
    {
        StudentResultGateway aStudentResultGateway = new StudentResultGateway();

        public List<Grade> GetAllGrades()
        {
            return aStudentResultGateway.GetAllGrades();
        } 
        public List<ViewStudentModel> GetEnrolledCourses(int studentId)
        {
            return aStudentResultGateway.GetEnrolledCourses(studentId);
        }

        public  string Save(StudentResult studentResult)
        {
            if (aStudentResultGateway.CheckResultOfSameCourseBySameStudent(studentResult))
            {
                int rowAffected = aStudentResultGateway.UpdateResult(studentResult);
                if (rowAffected >0)
                {
                    return "Result updated successfully";
                }
                else
                {
                    return "Updating failed";
                }
            }
            else
            {
                int rowAffected = aStudentResultGateway.Save(studentResult);
                if (rowAffected > 0)
                {
                    return "Saved Successfully";
                }
                return "Saving Failed";
            }
          
        }
    }
}