using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class PatientRepository : IRepository<Patient>
    {
        private readonly HospitalDBContext context;

        public PatientRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await context.Patients.ToListAsync();
        }

        public async Task InsertAsync(Patient item)
        {
            context.Patients.Add(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Patient item)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.FirstName = item.FirstName;
                patient.LastName = item.LastName;
                patient.Gender = item.Gender;
                patient.Email = item.Email;
                patient.Password = item.Password;
                patient.DOB = item.DOB;
                patient.PhoneNumber = item.PhoneNumber;
                patient.MedicalHistory = item.MedicalHistory;
                await context.SaveChangesAsync();
            }
        }
        public async Task DeleteAsync(int id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient != null)
            {
                context.Remove(patient);
                await context.SaveChangesAsync();
            }
        }
    }
}
