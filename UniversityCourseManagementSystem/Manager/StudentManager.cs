using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Manager
{
    public class StudentManager
    {
        StudentGateway aStudentGateway = new StudentGateway();
        public string Save(Student aStudent)
        {
            if (aStudentGateway.IsEmailExists(aStudent) && aStudentGateway.IsContactNoExists(aStudent))
            {
                return "Email and contact no both are existing";
            }
            if (aStudentGateway.IsEmailExists(aStudent))
            {
                return "Email already exists";
            }
            if (aStudentGateway.IsContactNoExists(aStudent))
            {
                return "Contact no already exists";
            }
            else
            {
                int rowAffected = aStudentGateway.Save(aStudent);
                if (rowAffected > 0)
                {
                    return "Regitration complete";
                }
                return "Registration failed";
            }

        }

        public List<Student> GetDepartmentId(Student student)
        {
            return aStudentGateway.GetDepartmentId(student);
        }

        public Department GetDepartmentById(int departmentId)
        {
            return aStudentGateway.GetDepartmentById(departmentId);
        }
    }
}