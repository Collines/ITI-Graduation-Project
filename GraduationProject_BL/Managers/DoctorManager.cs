using GraduationProject_BL.DTO;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.Managers
{
    public class DoctorManager : IDoctorManager
    {
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
                            Images = doctor.Images,
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
                            Images = doctor.Images,
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
                                Images = doctor.Images,
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
                                Images = doctor.Images,
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

        public async Task InsertAsync(InsertDoctorDTO item)
        {
            Doctor doctor = new()
            {
                FirstName = item.FirstName_EN,
                LastName = item.LastName_EN,
                Gender = item.Gender,
                Title = item.Title_EN,
                Bio = item.Bio_EN,
                Images = item.Images,
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
                Title_AR= item.Title_AR,
                Bio_EN = item.Bio_EN,
                Bio_AR = item.Bio_AR,
                DoctorId = doctor.Id
            };
            await translations.InsertAsync(translation);
        }

        public async Task UpdateAsync(int id, InsertDoctorDTO item)
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
                    if (!item.Images.IsNullOrEmpty())
                        doctor.Images = item.Images;

                    await repository.UpdateAsync(doctor.Id, doctor);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            await translations.DeleteAsync(id);
            await repository.DeleteAsync(id);
        }
    }
}
