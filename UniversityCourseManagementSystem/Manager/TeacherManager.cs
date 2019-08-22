using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Manager
{
    public class TeacherManager
    {
        TeacherGateway aTeacherGateway = new TeacherGateway();


        public string Save(Teacher aTeacher)
        {
            if (aTeacherGateway.IsEmailExists(aTeacher))
            {
                return "Email already exits";
            }
            else
            {
                int rowAffected = aTeacherGateway.Save(aTeacher);
                if (rowAffected > 0)
                {
                    return "Saved Successfully";
                }
                return "Saving Failed";
            }
           
        }
        public List<Designation> GetAllDesignations()
        {
            return aTeacherGateway.GetAllDesignations();
        }
        public List<Teacher> GetAllTeachers()
        {
            return aTeacherGateway.GetAllTeachers();
        }

        public Teacher GetCreditToBeTaken(int teacherId)
        {
            return aTeacherGateway.GetCreditToBeTaken(teacherId);
        }
    }
}