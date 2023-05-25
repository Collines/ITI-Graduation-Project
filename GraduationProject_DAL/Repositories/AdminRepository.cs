using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class AdminsRepository : IRepository<Admin>
    {
        private readonly HospitalDBContext context;

        public AdminsRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<List<Admin>> GetAllAsync()
        {
            return await context.Admins.ToListAsync();
        }

        public async Task InsertAsync(Admin item)
        {
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Admin item)
        {
            var admin = await context.Admins.FindAsync(id);
            if (admin != null)
            {
                admin.UserName = item.UserName;
                admin.Password = item.Password;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var admin = await context.Admins.FindAsync(id);
            if (admin != null)
            {
                context.Remove(admin);
                await context.SaveChangesAsync();
            }
        }
    }
}
