using GraduationProject_BL.DTO.BannerDTOs;
using GraduationProject_BL.DTO.DepartmentDTOs;
using GraduationProject_BL.DTO.DoctorDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.Managers
{
    public class BannerManager : IBannerManger
    {
        private readonly IRepository<Banners> repository;
        private readonly ITranslations<BannerTranslation> translations;
        private readonly string retrievePath = "assets\\images";


        public BannerManager(IRepository<Banners> _repository, ITranslations<BannerTranslation> _translations)
        {
            repository = _repository;
            translations = _translations;
        }



        public async Task DeleteAsync(int id)
        {

            await translations.DeleteAsync(id);
            await repository.DeleteAsync(id);
        }

        public async Task<List<BannerDTO>> GetAllAsync(string lang)
        {
            var Banners = await repository.GetAllAsync();

            var BannerDTO = new List<BannerDTO>();

            foreach (var banner in Banners)
            {
                var translation = await translations.FindAsync(banner.Id);
                if (translation != null)
                {
                    BannerDTO dto;
                    var path = "";
                    if (banner.ImagePath != null)
                        path = Path.Combine(retrievePath, banner.ImagePath);

                    if (lang == "ar")
                    {
                        dto = new()
                        {
                            Id = translation.BannerId,
                            Title = translation.Title_AR,
                            Description = translation.Description_AR,
                            ImagePath = path,

                        };
                    }
                    else
                    {
                        dto = new()
                        {
                            Id = translation.BannerId,
                            Title = translation.Title_EN,
                            Description = translation.Description_EN,
                            ImagePath = path,

                        };
                    }

                    BannerDTO.Add(dto);
                }

            }
            return BannerDTO;
        }

        public async Task<BannerDTO> GetByIdAsync(int id, string lang)
        {
            var banners = await repository.GetAllAsync();

            if (banners != null)
            {
                var banner = banners.Find(x => x.Id == id);
                if (banner != null)
                {
                    var translation = await translations.FindAsync(banner.Id);
                    if (translation != null)
                    {
                        BannerDTO dto;
                        var path = "";
                        if (banner.ImagePath != null)
                            path = Path.Combine(retrievePath, banner.ImagePath);

                        if (lang == "ar")
                        {
                            dto = new()
                            {
                                Id = translation.BannerId,
                                Title = translation.Title_AR,
                                Description = translation.Description_AR,
                                ImagePath = path
                            };
                        }
                        else
                        {
                            dto = new()
                            {
                                Id = translation.BannerId,
                                Title = translation.Title_EN,
                                Description = translation.Description_EN,
                                ImagePath = path
                            };
                        }

                        return dto;
                    }
                }
            }

            return null;

        }

        public async Task InsertAsync(BannerFormData formData)
        {
            BannerInsertDTO item = await BannerFormDataToBannerInsertDTO(formData);
            if (item != null)
            {
                Banners banner = new()
                {
                    Description = item.Description_EN,
                    Title = item.Title_EN,
                    ImagePath = item.Image,
                };

                await repository.InsertAsync(banner);

                BannerTranslation translation = new()
                {
                    Description_EN = item.Description_EN,
                    Description_AR = item.Description_AR,
                    Title_EN = item.Title_EN,
                    Title_AR = item.Title_AR,
                    BannerId = banner.Id
                };
                await translations.InsertAsync(translation);
            }
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

        private void DeleteBannerImage(string imageName)
        {
            var dasboardPreviousImagePath = Path.Combine(GetDashboardImagesPath(), imageName);
            if (File.Exists(dasboardPreviousImagePath))
                File.Delete(dasboardPreviousImagePath);

            var clientPreviousImagePath = Path.Combine(GetClientImagesPath(), imageName);
            if (File.Exists(clientPreviousImagePath))
                File.Delete(clientPreviousImagePath);
        }


        public async Task<BannerInsertDTO> BannerFormDataToBannerInsertDTO(BannerFormData item)
        {
            BannerInsertDTO banner = new()
            {
                Description_AR = item.Description_AR,
                Description_EN = item.Description_EN,
                Title_EN = item.Title_EN,
                Title_AR = item.Title_AR,
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

                banner.Image = uniqueFileName;
            }

            return banner;
        }

        public async Task UpdateAsync(int id, BannerFormData formData)
        {

            var banners = await repository.GetAllAsync();

            if (banners != null)
            {
                var banner = banners.Find(x => x.Id == id);
                if (banner != null)
                {
                    var translation = await translations.FindAsync(banner.Id);
                    BannerInsertDTO item = await BannerFormDataToBannerInsertDTO(formData);
                    if (translation != null)
                    {
                        translation.Description_EN = item.Description_EN;
                        translation.Description_AR = item.Description_AR;
                        translation.Title_EN = item.Title_EN;
                        translation.Title_AR = item.Title_AR;
                        await translations.UpdateAsync(translation.Id, translation.BannerId, translation);
                    }

                    banner.Description = item.Description_EN;
                    banner.Title = item.Title_EN;
                    if (item.Image != null)
                    {
                        if (banner.ImagePath != null)
                        {
                            DeleteBannerImage(banner.ImagePath);
                        }
                        banner.ImagePath = item.Image;
                    }


                    await repository.UpdateAsync(banner.Id, banner);
                }
            }
        }


        public async Task<BannerInsertDTO?> GetInsertDTOByIdAsync(int id)
        {
            var Banners = await repository.GetAllAsync();

            if (Banners != null)
            {
                var Banner = Banners.Find(x => x.Id == id);
                if (Banner != null)
                {
                    var translation = await translations.FindAsync(Banner.Id);
                    if (translation != null)
                    {
                        var path = Path.Combine(retrievePath, Banner.ImagePath);
                        Banner.ImagePath = path;
                        BannerInsertDTO dto = new()
                        {
                            Id = translation.BannerId,
                            Title_EN = translation.Title_EN,
                            Title_AR = translation.Title_AR,
                            Description_EN = translation.Description_EN,
                            Description_AR = translation.Description_AR,
                            Image = Banner.ImagePath,
                        };

                        return dto;
                    }
                }
            }
            return null;
        }
    }
}