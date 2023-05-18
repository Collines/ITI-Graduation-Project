using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class DepartmentRepository : IRepository<Department>
    {
        private readonly HospitalDBContext context;

        public DepartmentRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await context.Departments.ToListAsync();
        }

        public async Task InsertAsync(Department item)
        {
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Department item)
        {
            var department = await context.Departments.FindAsync(id);
            if (department != null)
            {
                department.Title = item.Title;
                department.Description = item.Description;
                department.Id = item.Id;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var department = await context.Departments.FindAsync(id);
            if (department != null)
            {
                context.Remove(department);
                await context.SaveChangesAsync();
            }
        }
    }
}
