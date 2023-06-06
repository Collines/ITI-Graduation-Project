using GraduationProject_BL.DTO.ArticleDTOs;
using GraduationProject_BL.DTO.DoctorDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;

namespace GraduationProject_BL.Managers
{
    public class ArticleManager : IArticleManager
    {
        private readonly string retrievePath = "assets\\images\\articles";
        private readonly IRepository<Article> repository;
        private readonly ITranslations<ArticleTranslations> translations;

        public ArticleManager(IRepository<Article> _repository, ITranslations<ArticleTranslations> _translations)
        {
            repository = _repository;
            translations = _translations;
        }

        public async Task<List<ArticleDTO>?> GetAllAsync(string lang)
        {
            var articles = await repository.GetAllAsync();

            var articlesDTO = new List<ArticleDTO>();

            foreach (var article in articles)
            {
                var translation = await translations.FindAsync(article.Id);
                if (translation != null)
                {

                    ArticleDTO dto;
                    var path = "";
                    if (article.Image != null)
                        path = Path.Combine(retrievePath, article.Image.Name);

                    if (lang == "ar")
                    {
                        dto = new()
                        {
                            Id = translation.ArticleId,
                            Title = translation.Title_AR,
                            Description = translation.Description_AR,
                            Image = path,
                            PostedAt = article.PostedAt
                        };
                    }
                    else
                    {
                        dto = new()
                        {
                            Id = translation.ArticleId,
                            Title = translation.Title_EN,
                            Description = translation.Description_EN,
                            Image = path,
                            PostedAt = article.PostedAt
                        };
                    }

                    articlesDTO.Add(dto);
                }
            }

            return articlesDTO;
        }


        public async Task<ArticleDTO?> GetByIdAsync(int id, string lang)
        {
            var articles = await repository.GetAllAsync();

            if (articles != null)
            {
                var article = articles.Find(x => x.Id == id);
                if (article != null)
                {
                    var translation = await translations.FindAsync(article.Id);
                    if (translation != null)
                    {

                        ArticleDTO dto;
                        var path = "";
                        if (article.Image != null)
                            path = Path.Combine(retrievePath, article.Image.Name);
                        if (lang == "ar")
                        {
                            dto = new()
                            {
                                Id = translation.ArticleId,
                                Title = translation.Title_AR,
                                Description = translation.Description_AR,
                                Image = path,
                                PostedAt = article.PostedAt
                            };
                        }
                        else
                        {
                            dto = new()
                            {
                                Id = translation.ArticleId,
                                Title = translation.Title_EN,
                                Description = translation.Description_EN,
                                Image = path,
                                PostedAt = article.PostedAt
                            };
                        }

                        return dto;
                    }
                }
            }

            return null;
        }
        public async Task<ArticleEditDTO?> GetArticeEditDTOByIdAsync(int id, string lang)
        {
            var articles = await repository.GetAllAsync();

            if (articles != null)
            {
                var article = articles.Find(x => x.Id == id);
                if (article != null)
                {
                    var translation = await translations.FindAsync(article.Id);
                    if (translation != null)
                    {

                        ArticleEditDTO dto;
                        var path = "";
                        if (article.Image != null)
                            path = Path.Combine(retrievePath, article.Image.Name);
                        dto = new()
                        {
                            Id = translation.ArticleId,
                            Title_EN = translation.Title_EN,
                            Description_EN = translation.Description_EN,
                            Title_AR = translation.Title_AR,
                            Description_AR = translation.Description_AR,
                            Image = path,
                        };

                        return dto;
                    }
                }
            }

            return null;
        }


        public async Task InsertAsync(ArticleFormData formData)
        {
            ArticleInsertDTO item = await ArticleFormDataToArticleInsertDTO(formData);
            if (item != null)
            {
                Article article = new()
                {
                    Title = item.Title_EN,
                    Description = item.Description_EN,
                    Image = item.Image,
                };

                await repository.InsertAsync(article);

                ArticleTranslations translation = new()
                {
                    Title_EN = item.Title_EN,
                    Title_AR = item.Title_AR,
                    Description_EN = item.Description_EN,
                    Description_AR = item.Description_AR,
                    ArticleId = article.Id,
                };
                await translations.InsertAsync(translation);
            }
        }

        public async Task UpdateAsync(int id, ArticleFormData formData)
        {
            var articles = await repository.GetAllAsync();

            if (articles != null)
            {
                var article = articles.Find(x => x.Id == id);
                if (article != null)
                {
                    var translation = await translations.FindAsync(article.Id);
                    ArticleInsertDTO item = await ArticleFormDataToArticleInsertDTO(formData);
                    if (translation != null)
                    {
                        translation.Title_EN = item.Title_EN;
                        translation.Title_AR = item.Title_AR;
                        translation.Description_EN = item.Description_EN;
                        translation.Description_AR = item.Description_AR;

                        await translations.UpdateAsync(translation.Id, translation.ArticleId, translation);
                    }

                    article.Title = item.Title_EN;
                    article.Description = item.Description_EN;

                    if (item.Image != null)
                    {
                        if (article.Image != null)
                        {
                            DeleteArticleImage(article.Image.Name);
                        }
                        article.Image = item.Image;
                    }


                    await repository.UpdateAsync(article.Id, article);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var articles = await repository.GetAllAsync();
            var article = articles.Find(d => d.Id == id);
            if (article != null)
            {
                if (article.Image != null)
                {
                    DeleteArticleImage(article.Image.Name);
                }
                await translations.DeleteAsync(id);
                await repository.DeleteAsync(id);
            }   
        }


        public async Task<ArticleInsertDTO> ArticleFormDataToArticleInsertDTO(ArticleFormData item)
        {
            ArticleInsertDTO article = new()
            {
                Title_EN= item.Title_EN,
                Title_AR=item.Title_AR,
                Description_EN = item.Description_EN,
                Description_AR = item.Description_AR,
            };

            if (item.Image != null && item.Image.Length > 0)
            {
                var uniqueFileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + "_" + item.Image.FileName;

                var dashboardFilePath = Path.Combine(GetDashboardImagesPath(), uniqueFileName);
                using (var stream = new FileStream(dashboardFilePath, FileMode.Create))
                {
                    await item.Image.CopyToAsync(stream);
                }

                var clientFilePath = Path.Combine(GetClientImagesPath(), uniqueFileName);
                using (var stream = new FileStream(clientFilePath, FileMode.Create))
                {
                    await item.Image.CopyToAsync(stream);
                }

                article.Image = new ArticleImage { Name = uniqueFileName };
            }

            return article;
        }

        private string GetDashboardImagesPath()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var newPath = Path.GetDirectoryName(currentPath) + "\\Dashboard\\src\\" + retrievePath;
            return newPath;
        }

        private string GetClientImagesPath()
        {
            var currentPath = Directory.GetCurrentDirectory();
            var newPath = Path.GetDirectoryName(currentPath) + "\\Client\\src\\" + retrievePath;
            return newPath;
        }

        private void DeleteArticleImage(string imageName)
        {
            var dasboardPreviousImagePath = Path.Combine(GetDashboardImagesPath(), imageName);
            if (File.Exists(dasboardPreviousImagePath))
                File.Delete(dasboardPreviousImagePath);

            var clientPreviousImagePath = Path.Combine(GetClientImagesPath(), imageName);
            if (File.Exists(clientPreviousImagePath))
                File.Delete(clientPreviousImagePath);
        }
    }
}
