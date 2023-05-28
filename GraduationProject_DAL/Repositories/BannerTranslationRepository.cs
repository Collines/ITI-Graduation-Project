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
    public class BannerTranslationRepository : ITranslations<BannerTranslation>
    {
        private readonly HospitalDBContext context;

        public BannerTranslationRepository(HospitalDBContext _context)
        {
            context = _context;
        }


        public async Task<BannerTranslation?> FindAsync(int parentId)
        {
            return await context.BannerTranslations.FirstOrDefaultAsync(t => t.BannerId == parentId);
        }

        public async Task InsertAsync(BannerTranslation item)
        {
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, int parentId, BannerTranslation item)
        {
            var translation = await context.BannerTranslations.FirstOrDefaultAsync(t => t.Id == id && t.BannerId == parentId);
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
            var translation = await context.BannerTranslations.FirstOrDefaultAsync(t => t.BannerId == parentId);
            if (translation != null)
            {
                context.Remove(translation);
                await context.SaveChangesAsync();
            }
        }
    }
}
