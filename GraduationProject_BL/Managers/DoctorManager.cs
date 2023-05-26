using GraduationProject_BL.DTO.DoctorDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using System.Numerics;

namespace GraduationProject_BL.Managers
{
    public class DoctorManager : IDoctorManager
    {
        private readonly string retrievePath = "assets\\images";
        private readonly IRepository<Doctor> repository;
        private readonly ITranslations<DoctorTranslations> translations;
        private readonly ITranslations<DepartmentTranslations> departmentTranslations;

        public DoctorManager(IRepository<Doctor> _repository, ITranslations<DoctorTranslations> _translations, ITranslations<DepartmentTranslations> _departmentTranslations)
        {
            repository = _repository;
            translations = _translations;
            departmentTranslations = _departmentTranslations;
        }

        public async Task<List<DoctorDTO>> GetAllAsync(string lang)
        {
            var doctors = await repository.GetAllAsync();

            var doctorsDTO = new List<DoctorDTO>();

            foreach (var doctor in doctors)
            {
                var translation = await translations.FindAsync(doctor.Id);
                if (translation != null)
                {
                    var departmentTranslation = await departmentTranslations.FindAsync(doctor.DepartmentId);

                    DoctorDTO dto;
                    var path = "";
                    if (doctor.Image != null)
                        path = Path.Combine(retrievePath, doctor.Image.Name);

                    if (lang == "ar")
                    {
                        dto = new()
                        {
                            Id = translation.DoctorId,
                            FirstName = translation.FirstName_AR,
                            LastName = translation.LastName_AR,
                            Gender = doctor.Gender,
                            Title = translation.Title_AR,
                            Bio = translation.Bio_AR,
                            Image = path,
                            DepartmentId = doctor.DepartmentId,
                            DepartmentTitle = departmentTranslation.Title_AR

                        };
                    }
                    else
                    {
                        dto = new()
                        {
                            Id = translation.DoctorId,
                            FirstName = translation.FirstName_EN,
                            LastName = translation.LastName_EN,
                            Gender = doctor.Gender,
                            Title = translation.Title_EN,
                            Bio = translation.Bio_EN,
                            Image = path,
                            DepartmentId = doctor.DepartmentId,
                            DepartmentTitle = departmentTranslation.Title_EN
                        };
                    }

                    doctorsDTO.Add(dto);
                }
            }

            return doctorsDTO;
        }

        public async Task<DoctorDTO?> GetByIdAsync(int id, string lang)
        {
            var doctors = await repository.GetAllAsync();

            if (doctors != null)
            {
                var doctor = doctors.Find(x => x.Id == id);
                if (doctor != null)
                {
                    var translation = await translations.FindAsync(doctor.Id);
                    if (translation != null)
                    {
                        var departmentTranslation = await departmentTranslations.FindAsync(doctor.DepartmentId);

                        DoctorDTO dto;
                        var path = "";
                        if (doctor.Image != null)
                            path = Path.Combine(retrievePath, doctor.Image.Name);
                        if (lang == "ar")
                        {
                            dto = new()
                            {
                                Id = translation.DoctorId,
                                FirstName = translation.FirstName_AR,
                                LastName = translation.LastName_AR,
                                Gender = doctor.Gender,
                                Title = translation.Title_AR,
                                Bio = translation.Bio_AR,
                                Image = path,
                                DepartmentId = doctor.DepartmentId,
                                DepartmentTitle = departmentTranslation.Title_AR
                            };
                        }
                        else
                        {
                            dto = new()
                            {
                                Id = translation.DoctorId,
                                FirstName = translation.FirstName_EN,
                                LastName = translation.LastName_EN,
                                Gender = doctor.Gender,
                                Title = translation.Title_EN,
                                Bio = translation.Bio_EN,
                                Image = path,
                                DepartmentId = doctor.DepartmentId,
                                DepartmentTitle = departmentTranslation.Title_EN
                            };
                        }

                        return dto;
                    }
                }
            }

            return null;
        }

        public async Task InsertAsync(DoctorFormData formData)
        {
            DoctorInsertDTO item = await DoctorFormDataToDoctorInsertDTO(formData);
            if (item != null)
            {
                Doctor doctor = new()
                {
                    FirstName = item.FirstName_EN,
                    LastName = item.LastName_EN,
                    Gender = item.Gender,
                    Title = item.Title_EN,
                    Bio = item.Bio_EN,
                    Image = item.Image,
                    DepartmentId = item.DepartmentId
                };

                await repository.InsertAsync(doctor);

                DoctorTranslations translation = new()
                {
                    FirstName_EN = item.FirstName_EN,
                    FirstName_AR = item.FirstName_AR,
                    LastName_EN = item.LastName_EN,
                    LastName_AR = item.LastName_AR,
                    Title_EN = item.Title_EN,
                    Title_AR = item.Title_AR,
                    Bio_EN = item.Bio_EN,
                    Bio_AR = item.Bio_AR,
                    DoctorId = doctor.Id
                };
                await translations.InsertAsync(translation);
            }
        }

        public async Task UpdateAsync(int id, DoctorFormData formData)
        {
            var doctors = await repository.GetAllAsync();

            if (doctors != null)
            {
                var doctor = doctors.Find(x => x.Id == id);
                if (doctor != null)
                {
                    var translation = await translations.FindAsync(doctor.Id);
                    DoctorInsertDTO item = await DoctorFormDataToDoctorInsertDTO(formData);
                    if (translation != null)
                    {
                        translation.FirstName_EN = item.FirstName_EN;
                        translation.FirstName_AR = item.FirstName_AR;
                        translation.LastName_EN = item.LastName_EN;
                        translation.LastName_AR = item.LastName_AR;
                        translation.Title_EN = item.Title_EN;
                        translation.Title_AR = item.Title_AR;
                        translation.Bio_EN = item.Bio_EN;
                        translation.Bio_AR = item.Bio_AR;

                        await translations.UpdateAsync(translation.Id, translation.DoctorId, translation);
                    }

                    doctor.FirstName = item.FirstName_EN;
                    doctor.LastName = item.LastName_EN;
                    doctor.Gender = item.Gender;
                    doctor.Title = item.Title_EN;
                    doctor.Bio = item.Bio_EN;
                    doctor.DepartmentId = item.DepartmentId;

                    if (item.Image != null)
                    {
                        if (doctor.Image != null)
                        {
                            DeleteDoctorImage(doctor.Image.Name);
                        }
                        doctor.Image = item.Image;
                    }


                    await repository.UpdateAsync(doctor.Id, doctor);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            var doctors = await repository.GetAllAsync();
            var doctor = doctors.Find(d => d.Id == id);
            if (doctor != null)
            {
                if (doctor.Image != null)
                {
                    DeleteDoctorImage(doctor.Image.Name);
                }
                await translations.DeleteAsync(id);
                await repository.DeleteAsync(id);
            }   
        }

        public async Task<DoctorInsertDTO?> GetInsertDTOByIdAsync(int id)
        {
            var doctors = await repository.GetAllAsync();

            if (doctors != null)
            {
                var doctor = doctors.Find(x => x.Id == id);
                if (doctor != null)
                {
                    var translation = await translations.FindAsync(doctor.Id);
                    if (translation != null)
                    {
                        var path = Path.Combine(retrievePath, doctor.Image.Name);
                        doctor.Image.Name = path;
                        DoctorInsertDTO dto = new()
                        {
                            Id = translation.DoctorId,
                            FirstName_EN = translation.FirstName_EN,
                            FirstName_AR = translation.FirstName_AR,
                            LastName_EN = translation.LastName_EN,
                            LastName_AR = translation.LastName_AR,
                            Gender = doctor.Gender,
                            Title_EN = translation.Title_EN,
                            Title_AR = translation.Title_AR,
                            Bio_EN = translation.Bio_EN,
                            Bio_AR = translation.Bio_AR,
                            Image = doctor.Image,
                            DepartmentId = doctor.DepartmentId,
                        };

                        return dto;
                    }
                }
            }
            return null;
        }

        // Convert formData to DoctorInsertDTO and create the image file
        public async Task<DoctorInsertDTO> DoctorFormDataToDoctorInsertDTO(DoctorFormData item)
        {
            DoctorInsertDTO doctor = new()
            {
                FirstName_EN = item.FirstName_EN,
                LastName_EN = item.LastName_EN,
                FirstName_AR = item.FirstName_AR,
                LastName_AR = item.LastName_AR,
                Title_EN = item.Title_EN,
                Title_AR = item.Title_AR,
                Bio_EN = item.Bio_EN,
                Bio_AR = item.Bio_AR,
                Gender = item.Gender,
                DepartmentId = item.DepartmentId,
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

                doctor.Image = new Image { Name = uniqueFileName };
            }

            return doctor;
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

        private void DeleteDoctorImage(string imageName)
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
