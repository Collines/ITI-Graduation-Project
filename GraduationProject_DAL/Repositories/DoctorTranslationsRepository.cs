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
    public class DoctorTranslationsRepository : ITranslations<DoctorTranslations>
    {
        private readonly HospitalDBContext context;

        public DoctorTranslationsRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<DoctorTranslations?> FindAsync(int parentId)
        {
            return await context.DoctorTranslations.FirstOrDefaultAsync(d => d.DoctorId == parentId);
        }

        public async Task InsertAsync(DoctorTranslations item)
        {
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, int parentId, DoctorTranslations item)
        {
            var translation = await context.DoctorTranslations.FirstOrDefaultAsync(d => d.Id == id && d.DoctorId == parentId);
            if (translation != null)
            {
                translation.FirstName_EN = item.FirstName_EN;
                translation.FirstName_AR = item.FirstName_AR;
                translation.LastName_EN = item.LastName_EN;
                translation.LastName_AR = item.LastName_AR;
                translation.Title_EN = item.Title_EN;
                translation.Title_AR = item.Title_AR;
                translation.Bio_EN = item.Bio_EN;
                translation.Bio_AR = item.Bio_AR;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int parentId)
        {
            var translation = await context.DoctorTranslations.FirstOrDefaultAsync(d => d.DoctorId == parentId);
            if (translation != null)
            {
                context.Remove(translation);
                await context.SaveChangesAsync();
            }
        }
    }
}
