using GraduationProject_BL.DTO.DepartmentDTOs;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;

namespace GraduationProject_BL.Managers
{
    public class DepartmentManager : IDepartmentManager
    {
        private readonly IRepository<Department> repository;
        private readonly ITranslations<DepartmentTranslations> translations;

        public DepartmentManager(IRepository<Department> _repository, ITranslations<DepartmentTranslations> _translations)
        {
            repository = _repository;
            translations = _translations;
        }

        public async Task<List<DepartmentDTO>> GetAllAsync(string lang)
        {
            var departments = await repository.GetAllAsync();

            var departmentsDTO = new List<DepartmentDTO>();

            foreach (var department in departments)
            {
                var translation = await translations.FindAsync(department.Id);
                if (translation != null)
                {
                    DepartmentDTO dto;
                    if (lang == "ar")
                    {
                        dto = new()
                        {
                            Id = translation.DepartmentId,
                            Title = translation.Title_AR,
                            Description = translation.Description_AR,
                            NumberOfBeds = department.NoOfBeds
                        };
                    }
                    else
                    {
                        dto = new()
                        {
                            Id = translation.DepartmentId,
                            Title = translation.Title_EN,
                            Description = translation.Description_EN,
                            NumberOfBeds = department.NoOfBeds
                        };
                    }

                    departmentsDTO.Add(dto);
                }
            }

            return departmentsDTO;
        }

        public async Task<DepartmentDTO?> GetByIdAsync(int id, string lang)
        {
            var departments = await repository.GetAllAsync();

            if (departments != null)
            {
                var department = departments.Find(x => x.Id == id);
                if (department != null)
                {
                    var translation = await translations.FindAsync(department.Id);
                    if (translation != null)
                    {
                        DepartmentDTO dto;
                        if (lang == "ar")
                        {
                            dto = new()
                            {
                                Id = translation.DepartmentId,
                                Title = translation.Title_AR,
                                Description = translation.Description_AR,
                                NumberOfBeds = department.NoOfBeds
                            };
                        }
                        else
                        {
                            dto = new()
                            {
                                Id = translation.DepartmentId,
                                Title = translation.Title_EN,
                                Description = translation.Description_EN,
                                NumberOfBeds = department.NoOfBeds
                            };
                        }

                        return dto;
                    }
                }
            }

            return null;
        }

        public async Task InsertAsync(DepartmentInsertDTO item)
        {
            Department department = new()
            {
                Title = item.Title_EN,
                Description = item.Description_EN,
                NoOfBeds = item.NumberOfBeds
            };

            await repository.InsertAsync(department);

            DepartmentTranslations translation = new()
            {
                Title_EN = item.Title_EN,
                Title_AR = item.Title_AR,
                Description_EN = item.Description_EN,
                Description_AR = item.Description_AR,
                DepartmentId = department.Id
            };
            await translations.InsertAsync(translation);
        }

        public async Task UpdateAsync(int id, DepartmentInsertDTO item)
        {
            var departments = await repository.GetAllAsync();

            if (departments != null)
            {
                var department = departments.Find(x => x.Id == id);
                if (department != null)
                {
                    var translation = await translations.FindAsync(department.Id);
                    if (translation != null)
                    {
                        translation.Title_EN = item.Title_EN;
                        translation.Title_AR = item.Title_AR;
                        translation.Description_EN = item.Description_EN;
                        translation.Description_AR = item.Description_AR;

                        await translations.UpdateAsync(translation.Id, translation.DepartmentId, translation);
                    }

                    department.Title = item.Title_EN;
                    department.Description = item.Description_EN;

                    await repository.UpdateAsync(department.Id, department);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            await translations.DeleteAsync(id);
            await repository.DeleteAsync(id);
        }
        public async Task<DepartmentInsertDTO?> GetInsertDTOByIdAsync(int id)
        {
            var departments = await repository.GetAllAsync();

            if (departments != null)
            {
                var department = departments.Find(x => x.Id == id);
                if (department != null)
                {

                    var translation = await translations.FindAsync(department.Id);
                    if (translation != null)
                    {

                        DepartmentInsertDTO dto = new()
                        {
                            Title_EN = translation.Title_EN,
                            Title_AR = translation.Title_AR,
                            Description_EN = translation.Description_EN,
                            Description_AR = translation.Description_AR,
                            NumberOfBeds = department.NoOfBeds
                        };

                        return dto;

                    }
                }
            }
            return null;

        }
    }
}

