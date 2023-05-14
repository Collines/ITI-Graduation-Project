using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Interfaces
{
    public interface GeneralRepo<T>
    {
        public List<T> GetAllDept();
        public T GetDeptDetails(int id);
        public void InsertDept(T Dept);
        public void UpdateDept(int id, T Dept);
        public void DeleteDept(int id);
    }
}
