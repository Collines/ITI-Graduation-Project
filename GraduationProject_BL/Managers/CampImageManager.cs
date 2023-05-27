using GraduationProject_BL.DTO.CampImageDTOs;
using GraduationProject_BL.DTO.DepartmentDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.Managers
{
    public class CampImageManager:ICampImageManager
    {
        private readonly string retrievePath = "assets\\images";
        private readonly IRepository<CampImage> repository;
        public CampImageManager(IRepository<CampImage> _repository)
        {
            repository = _repository;
        }
        public async Task<List<CampImageDTO>> GetAllAsync()
        {
            var images = await repository.GetAllAsync();

            var imagesDTO = new List<CampImageDTO>();

            foreach (var image in images)
            {
                CampImageDTO dto;
                var path = "";
                if (image.Image != null)
                path = Path.Combine(retrievePath, image.Image);

                dto = new()
                        {
                            Id=image.Id,
                            Image = path
                        };
                imagesDTO.Add(dto);
            }
            return imagesDTO;
        }

        public async Task<CampImageDTO?> GetByIdAsync(int id)
        {
            var images = await repository.GetAllAsync();

            if (images != null)
            {
                var image = images.Find(x => x.Id == id);
                if (image != null)
                {
                    CampImageDTO dto;
                    dto = new()
                    {
                        Id = image.Id,
                        Image = image.Image
                    };

                    return dto;
                }
            }
            return null;
        }
        public async Task InsertAsync(CampImageInsertDTO item)
        {
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

                var campImage = new CampImage { Image = uniqueFileName };
                await repository.InsertAsync(campImage);
            }
        }

        public async Task DeleteAsync(int id)
        {
            await repository.DeleteAsync(id);
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

    }
}

