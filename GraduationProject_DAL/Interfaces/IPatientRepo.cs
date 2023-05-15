using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Interfaces
{
    public interface IPatientRepo
    {
        public List<Patient> GetAllPatients();
        public Patient? GetPatientDetails(int id);
        public void InsertPatient(Patient patient);
        public void UpdatePatient(int id, Patient patient);
        public void DeletePatient(int id);
        public bool IsExist(string email);
        public bool IsExist(int id);
    }
}
