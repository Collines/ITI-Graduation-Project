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
        public List<T> GetAll();
        public T GetDetails(int id);
        public void Insert(T Dept);
        public void Update(int id, T Dept);
        public void Delete(int id);
    }
}
