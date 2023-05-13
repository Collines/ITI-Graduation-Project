using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Interfaces
{
    internal interface IDepartmentRepo
    {
        public List<Department> GetAllDept();
        public Department GetDeptDetails(int id);
        public void InsertDept(Department Dept);
        public void UpdateDept(int id, Department Dept);
        public void DeleteDept(int id);
    }
}
