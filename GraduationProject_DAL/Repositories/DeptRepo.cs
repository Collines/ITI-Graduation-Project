using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Repositories
{
    internal class DeptRepo : IDepartmentRepo //:GeneralRepo<Department>
    {
        private readonly HospitalBDContext context;
        public DeptRepo(HospitalBDContext _context)
        {
            context = _context;
        }
        public void DeleteDept(int id)
        {
            Department dept = context.Departments.Find(id);
            if (dept != null)
            {
                context.Departments.Remove(dept);
                context.SaveChanges();
            }
        }

        public List<Department> GetAllDept()
        {
            return context.Departments.ToList();

        }

        public Department GetDeptDetails(int id)
        {
            return context.Departments.FirstOrDefault(d => d.Id == id);
        }

        public void InsertDept(Department Dept)
        {
            context.Departments.Add(Dept);
            context.SaveChanges();
        }

        public void UpdateDept(int id, Department Dept)
        {
            Department OldDept = context.Departments.Find(id);
            if (OldDept != null) { 
            OldDept.Title=Dept.Title;
            OldDept.Description = Dept.Description;
            OldDept.Id =Dept.Id;
            OldDept.DescriptionAR = Dept.DescriptionAR;
            OldDept.TitleAR = Dept.TitleAR;
            context.SaveChanges();
            }
        }
    }
}
