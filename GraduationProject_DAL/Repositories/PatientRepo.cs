using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Repositories
{
    public class PatientRepo : IPatientRepo
    {
        private readonly HospitalBDContext context;

        public PatientRepo(HospitalBDContext _context)
        {
            context = _context;
        }

        public List<Patient> GetAllPatients()
        {
           return context.Patients.ToList();
        }

        public Patient GetPatientDetails(int id)
        {
            return context.Patients.FirstOrDefault(p => p.Id == id);
        }

        public void InsertPatient(Patient patient)
        {
            context.Patients.Add(patient);
            context.SaveChanges();
        }

        public void UpdatePatient(int id, Patient patient)
        {
            Patient pat= context.Patients.Find(id);
            pat.FName = patient.FName;
            pat.FNameAR=patient.FNameAR;
            pat.LName= patient.LName;
            pat.LNameAR=patient.LNameAR;
            pat.Email=patient.Email;
            pat.Password=patient.Password;
            pat.DOB = patient.DOB;
            pat.PhoneNumber= patient.PhoneNumber;
            pat.MedicalHistory=patient.MedicalHistory;
            pat.Reservations=patient.Reservations;
            context.SaveChanges();
        }
        public void DeletePatient(int id)
        {
            context.Remove(context.Patients.Find(id));
            context.SaveChanges();
            
        }
    }
}
