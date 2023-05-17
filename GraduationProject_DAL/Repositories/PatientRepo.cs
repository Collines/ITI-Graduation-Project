using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Repositories
{
    public class PatientRepo : IPatientRepo//:GeneralRepo<Department>
    {
        private readonly HospitalBDContext context;

        public PatientRepo(HospitalBDContext _context)
        {
            context = _context;
        }

        public async Task<List<Patient>> GetAllPatients()
        {
           return await context.Patients.ToListAsync();
        }

        public async Task<Patient?> GetPatientDetails(int id)
        {
            return await context.Patients.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async void InsertPatient(Patient patient)
        {
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
        }

        public async void UpdatePatient(int id, Patient patient)
        {
            Patient? pat= await context.Patients.FindAsync(id);
            if(pat !=null)
            {
				pat.FName = patient.FName;
				pat.FNameAR = patient.FNameAR;
				pat.LName = patient.LName;
				pat.LNameAR = patient.LNameAR;
				pat.Gender = patient.Gender;
				pat.Email = patient.Email;
				pat.Password = patient.Password;
				pat.DOB = patient.DOB;
				pat.PhoneNumber = patient.PhoneNumber;
				pat.MedicalHistory = patient.MedicalHistory;
				await context.SaveChangesAsync();
			}
        }
        public async void DeletePatient(int id)
        {
            Patient? P = await context.Patients.FindAsync(id);
            if(P!=null)
            {
				context.Remove(P);
				await context.SaveChangesAsync();
			}
        }

		public async Task<bool> IsExist(string email)
		{
			return await context.Patients.AnyAsync(p=> p.Email == email);
		}

        public async Task<Patient?> GetPatient(string email)
        {
            return await context.Patients.FirstOrDefaultAsync(p => p.Email == email);
        }

		public async Task<bool> IsExist(int id)
		{
			return await context.Patients.AnyAsync(p => p.Id == id);
		}
	}
}
