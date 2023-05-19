using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class DepartmentTranslationsRepository : ITranslations<DepartmentTranslations>
    {
        private readonly HospitalDBContext context;

        public DepartmentTranslationsRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<DepartmentTranslations?> FindAsync(int parentId)
        {
            return await context.DepartmentTranslations.FirstOrDefaultAsync(t => t.DepartmentId == parentId);
        }

        public async Task InsertAsync(DepartmentTranslations item)
        {
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, int parentId, DepartmentTranslations item)
        {
            var translation = await context.DepartmentTranslations.FirstOrDefaultAsync(t => t.Id == id && t.DepartmentId == parentId);
            if (translation != null)
            {
                translation.Title_EN = item.Title_EN;
                translation.Title_AR = item.Title_AR;
                translation.Description_EN = item.Description_EN;
                translation.Description_AR = item.Description_AR;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int parentId)
        {
            var translation = await context.DepartmentTranslations.FirstOrDefaultAsync(t => t.DepartmentId == parentId);
            if (translation != null)
            {
                context.Remove(translation);
                await context.SaveChangesAsync();
            }
        }
    }
}
