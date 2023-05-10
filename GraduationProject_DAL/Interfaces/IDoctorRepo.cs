using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Interfaces
{
    public interface IDoctorRepo
    {
        public List<Doctor> GetAllDoctors();
        public Doctor GetDoctorDetails(int id);
        public void InsertDoctor(Doctor doctor);
        public void UpdateDoctor(int id, Doctor doctor);
        public void DeleteDoctor(int id);
    }
}
