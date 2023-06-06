using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GraduationProject_DAL.Repositories
{
    public class ArticleTranslationRepository : ITranslations<ArticleTranslations>
    {
        private readonly HospitalDBContext context;

        public ArticleTranslationRepository(HospitalDBContext _context)
        {
            context = _context;
        }

        public async Task<ArticleTranslations?> FindAsync(int articleId)
        {
            return await context.ArticleTranslations.FirstOrDefaultAsync(a => a.ArticleId == articleId);
        }

        public async Task InsertAsync(ArticleTranslations item)
        {
            if(item!=null)
            {
                await context.AddAsync(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, int articleId, ArticleTranslations item)
        {
            var translation = await context.ArticleTranslations.FirstOrDefaultAsync(a => a.Id == id && a.ArticleId == articleId);
            if (translation != null)
            {
                translation.Title_EN = item.Title_EN;
                translation.Title_AR = item.Title_AR;
                translation.Description_EN = item.Description_EN;
                translation.Description_AR = item.Description_AR;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int articleId)
        {
            var translation = await context.ArticleTranslations.FirstOrDefaultAsync(a => a.ArticleId == articleId);
            if (translation != null)
            {
                context.Remove(translation);
                await context.SaveChangesAsync();
            }
        }
    }
}
