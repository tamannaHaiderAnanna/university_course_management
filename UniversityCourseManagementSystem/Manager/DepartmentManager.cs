using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;

namespace UniversityCourseManagementSystem.Manager
{
    public class DepartmentManager
    {
        DepartmentGateway aDepartmentGateway = new DepartmentGateway();
        public List<Department> GetAllDepartments()
        {
            return aDepartmentGateway.GetAllDepartments();
        }
         public List<SelectListItem> GetDepartmentsForDropDownList()
        {
            var departments = GetAllDepartments();

            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() {Value = "", Text = "--Select--"}
            };

            foreach (Department item in departments)
            {
                SelectListItem selectListItem = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.DeptName
                };
                items.Add(selectListItem);
            }
            return items;
        }

        public string Save(Department aDepartment)
        {
            if (aDepartmentGateway.IsDeptCodeExist(aDepartment) && aDepartmentGateway.IsDeptNameExist(aDepartment))
            {
                return "DeptCode and DeptName both are existing";
            }
            if (aDepartmentGateway.IsDeptCodeExist(aDepartment))
            {
                return "DeptCode already exists";
            }
            if (aDepartmentGateway.IsDeptNameExist(aDepartment))
            {
                return "DeptName already exists";
            }
            else
            {
                int rowAffected = aDepartmentGateway.Save(aDepartment);
                if (rowAffected > 0)
                {
                    return "Saved successfully";
                }
                return "Saving failed";
            }
        }
    }
}