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
    public class DoctorRepo : IDoctorRepo //:GeneralRepo<Department>
    {
        private readonly HospitalBDContext context;

        public DoctorRepo(HospitalBDContext _context)
        {
            context = _context;
        }


        public async Task<List<Doctor>> GetAllDoctors()
        {
            return await context.Doctors.Include(d => d.Department).ToListAsync();
        }

        public async Task<Doctor?> GetDoctorDetails(int id)
        {
            return await context.Doctors.Include(d => d.Department).FirstOrDefaultAsync(d => d.Id == id);
        }

        public async void InsertDoctor(Doctor doctor)
        {
            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();
        }

        public async void UpdateDoctor(int id, Doctor doctor)
        {
            Doctor? OldDoctor = await context.Doctors.FindAsync(id);
            if (OldDoctor != null)
            {
                OldDoctor.FName = doctor.FName;
                OldDoctor.FNameAR = doctor.FNameAR;
                OldDoctor.LName = doctor.LName;
                OldDoctor.LNameAR = doctor.LNameAR;
                OldDoctor.Title = doctor.Title;
                OldDoctor.TitleAR = doctor.TitleAR;
                OldDoctor.Bio = doctor.Bio;
                OldDoctor.BioAR = doctor.BioAR;
                if (doctor.Image != null)
                    OldDoctor.Image = doctor.Image;
                OldDoctor.DeptId = doctor.DeptId;

                await context.SaveChangesAsync();
            }
        }

        public async void DeleteDoctor(int id)
        {
            var doctor = await context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                context.Remove(doctor);
                await context.SaveChangesAsync();
            }
            
        }
    }
}
