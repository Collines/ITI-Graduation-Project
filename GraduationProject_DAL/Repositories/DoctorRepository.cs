using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private readonly HospitalDBContext context;

        public DoctorRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await context.Doctors.Include(d => d.Department).ToListAsync();
        }

        public async Task InsertAsync(Doctor item)
        {
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Doctor item)
        {
            var doctor = await context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                doctor.FirstName = item.FirstName;
                doctor.LastName = item.LastName;
                doctor.Title = doctor.Title;
                doctor.Bio = item.Bio;
                doctor.DepartmentId = doctor.DepartmentId;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
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
