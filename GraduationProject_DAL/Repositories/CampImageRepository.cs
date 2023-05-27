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
    public class CampImageRepository : IRepository<CampImage>
    {
        private readonly HospitalDBContext context;

        public CampImageRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<List<CampImage>> GetAllAsync()
        {
            return await context.CampImages.ToListAsync();
        }

        public async Task InsertAsync(CampImage item)
        {
            await context.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, CampImage item)
        {
            var image = await context.CampImages.FindAsync(id);
            if (image != null)
            {
                image.Image = item.Image;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var image = await context.CampImages.FindAsync(id);
            if (image != null)
            {
                context.Remove(image);
                await context.SaveChangesAsync();
            }
        }
    }
}
