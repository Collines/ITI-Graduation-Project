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
    public class BannerRepository : IRepository<Banners>
         
    {

        private readonly HospitalDBContext context;


        public BannerRepository(HospitalDBContext _context)
        {
            context= _context;
        }
        public async Task DeleteAsync(int id)
        {
            var banner = await context.Banners.FindAsync(id);
            if (banner != null)
            {
                context.Remove(banner);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Banners>> GetAllAsync()
        {
            return await context.Banners.ToListAsync();
        }

        public async Task InsertAsync(Banners item)
        {
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, Banners item)
        {
            var banner = await context.Banners.FindAsync(id);
            if (banner != null)
            {
                banner.Title = item.Title;
                banner.Description = item.Description;
                banner.ImagePath = item.ImagePath;
                await context.SaveChangesAsync();
            }
        }
    }
}
