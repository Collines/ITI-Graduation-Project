using GraduationProject_BL.DTO.ArticleDTOs;
using GraduationProject_BL.DTO.DoctorDTOs;

namespace GraduationProject_BL.Interfaces
{
    public interface IArticleManager
    {
        public Task<List<ArticleDTO>?> GetAllAsync(string lang);
        public Task<ArticleDTO?> GetByIdAsync(int id, string lang);
        public Task InsertAsync(ArticleFormData formData);
        public Task UpdateAsync(int id, ArticleFormData formData);
        public Task DeleteAsync(int id);
        public Task<ArticleInsertDTO> ArticleFormDataToArticleInsertDTO(ArticleFormData item);
    }
}
