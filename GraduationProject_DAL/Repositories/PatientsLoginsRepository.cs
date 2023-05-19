using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class PatientsLoginsRepository : IRepository<PatientsLogins>
    {
        private readonly HospitalDBContext context;

        public PatientsLoginsRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<List<PatientsLogins>> GetAllAsync()
        {
            return await context.PatientsLogins.Include(p => p.Patient).ToListAsync();
        }

        public async Task InsertAsync(PatientsLogins item)
        {
            await context.PatientsLogins.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, PatientsLogins item)
        {
            var login = await context.PatientsLogins.FindAsync(id);
            if (login != null)
            {
                if (item != null)
                {
                    login.AccessToken = item.AccessToken;
                    login.RefreshToken = item.RefreshToken;
                    login.Expiration = item.Expiration;
                    login.LoggedIn = item.LoggedIn;
                    await context.SaveChangesAsync();
                }
            }
        }
        public async Task DeleteAsync(int id)
        {
            var patient = await context.PatientsLogins.FindAsync(id);
            if (patient != null)
            {
                context.Remove(patient);
                await context.SaveChangesAsync();
            }
        }
    }
}
