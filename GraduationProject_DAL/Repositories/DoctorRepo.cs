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


        public List<Doctor> GetAllDoctors()
        {
            return context.Doctors.Include(d => d.Department).ToList();
        }

        public Doctor GetDoctorDetails(int id)
        {
            return context.Doctors.Include(d => d.Department).FirstOrDefault(d => d.Id == id);
        }

        public void InsertDoctor(Doctor doctor)
        {
            context.Doctors.Add(doctor);
            context.SaveChanges();
        }

        public void UpdateDoctor(int id, Doctor doctor)
        {
            Doctor OldDoctor = context.Doctors.Find(id);
            OldDoctor.FName = doctor.FName;
            OldDoctor.FNameAR = doctor.FNameAR;
            OldDoctor.LName = doctor.LName;
            OldDoctor.LNameAR = doctor.LNameAR;
            OldDoctor.Title = doctor.Title;
            OldDoctor.TitleAR = doctor.TitleAR;
            OldDoctor.Bio = doctor.Bio;
            OldDoctor.BioAR = doctor.BioAR;
            if(doctor.Image != null )
                OldDoctor.Image = doctor.Image;
            OldDoctor.DeptId = doctor.DeptId;

            context.SaveChanges();
        }

        public void DeleteDoctor(int id)
        {
            var doctor = context.Doctors.Find(id);
            if (doctor != null)
            {
                context.Remove(doctor);
                context.SaveChanges();
            }
            
        }
    }
}
