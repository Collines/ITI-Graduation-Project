using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class PatientTranslationsRepository : ITranslations<PatientTranslations>
    {
        private readonly HospitalDBContext context;

        public PatientTranslationsRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<PatientTranslations?> FindAsync(int parentId)
        {
            return await context.PatientTranslations.FirstOrDefaultAsync(t => t.PatientId == parentId);
        }

        public async Task InsertAsync(PatientTranslations item)
        {
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, int parentId, PatientTranslations item)
        {
            var translation = await context.PatientTranslations.FirstOrDefaultAsync(t => t.Id == id && t.PatientId == parentId);
            if (translation != null)
            {
                translation.FirstName_EN = item.FirstName_EN;
                translation.FirstName_AR = item.FirstName_AR;
                translation.LastName_EN = item.LastName_EN;
                translation.LastName_AR = item.LastName_AR;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int parentId)
        {
            var translation = await context.PatientTranslations.FirstOrDefaultAsync(t => t.PatientId == parentId);
            if (translation != null)
            {
                context.Remove(translation);
                await context.SaveChangesAsync();
            }
        }
    }
}
