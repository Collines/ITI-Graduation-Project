using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class ArticleRepository : IRepository<Article>
    {
        private readonly HospitalDBContext context;

        public ArticleRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await context.Articles.OrderByDescending(a=>a.PostedAt).Include(a => a.Image).ToListAsync();
        }

        public async Task InsertAsync(Article item)
        {
            if(item!=null)
            {
                await context.AddAsync(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, Article item)
        {
            var article = await context.Articles.FindAsync(id);
            if (article != null)
            {
                article.Title = item.Title;
                article.Description = item.Description;
                article.Image = article.Image;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var article = await context.Articles.FindAsync(id);
            if (article != null)
            {
                context.Remove(article);
                await context.SaveChangesAsync();
            }

        }
    }
}
