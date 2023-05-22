using GraduationProject_BL.DTO;
using GraduationProject_BL.DTO.PatientDTOs;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Handlers;
using GraduationProject_DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GraduationProject_BL.Managers
{
    public class PatientManager : IPatientManager
    {
        private readonly IConfiguration iConfiguration;
        private readonly IRepository<Patient> repository;
        private readonly IRepository<PatientsLogins> logins;
        private readonly ITranslations<PatientTranslations> translations;

        public PatientManager(
            IConfiguration configuration,
            IRepository<Patient> _repository,
            IRepository<PatientsLogins> _logins,
            ITranslations<PatientTranslations> _translations)
        {
            iConfiguration = configuration;
            repository = _repository;
            logins = _logins;
            translations = _translations;
        }

        public async Task<List<PatientDTO>> GetAllAsync(string lang)
        {
            var patients = await repository.GetAllAsync();

            var patientsDTO = new List<PatientDTO>();

            foreach (var patient in patients)
            {
                var translation = await translations.FindAsync(patient.Id);
                if (translation != null)
                {
                    PatientDTO dto;
                    if (lang == "ar")
                    {
                        dto = new()
                        {
                            Id = translation.PatientId,
                            FullName = $"{translation.FirstName_AR} {translation.LastName_AR}"
                        };
                    }
                    else
                    {
                        dto = new()
                        {
                            Id = translation.PatientId,
                            FullName = $"{translation.FirstName_EN} {translation.LastName_EN}"
                        };
                    }

                    patientsDTO.Add(dto);
                }
            }

            return patientsDTO;
        }

        public async Task<PatientDTO?> GetByIdAsync(int id, string lang)
        {
            var patients = await repository.GetAllAsync();

            if (patients != null)
            {
                var patient = patients.Find(x => x.Id == id);
                if (patient != null)
                {
                    PatientDTO dto;
                    var translation = await translations.FindAsync(patient.Id);
                    if (translation != null)
                    {
                        if (lang == "ar")
                        {
                            dto = new()
                            {
                                Id = translation.PatientId,
                                FullName = $"{translation.FirstName_AR} {translation.LastName_AR}"
                            };
                        }
                        else
                        {
                            dto = new()
                            {
                                Id = translation.PatientId,
                                FullName = $"{translation.FirstName_EN} {translation.LastName_EN}"
                            };
                        }

                        return dto;

                    }
                    else
                    {
                        dto = new()
                        {
                            Id = patient.Id,
                            FullName = $"{patient.FirstName} {patient.LastName}"
                        };
                        return dto;
                    }
                }
            }

            return null;
        }

        public async Task<LoginDTO?> InsertAsync(PatientInsertDTO item)
        {
            Patient patient = new()
            {
                SSN = item.SSN,
                FirstName = item.FirstName_EN,
                LastName = item.LastName_EN,
                Gender = item.Gender,
                Email = item.Email.ToLower(),
                Password = PasswordHandler.Hash(item.Password, out byte[] salt),
                PasswordSalt = Convert.ToHexString(salt),
                DOB = item.DOB,
                PhoneNumber = item.Phone,
                MedicalHistory = item.MedicalHistory
            };

            await repository.InsertAsync(patient);

            PatientTranslations translation = new()
            {
                FirstName_EN = item.FirstName_EN,
                FirstName_AR = item.FirstName_AR,
                LastName_EN = item.LastName_EN,
                LastName_AR = item.LastName_AR,
                PatientId = patient.Id
            };
            await translations.InsertAsync(translation);

            return await GetLoginDTO(patient);
        }

        public async Task UpdateAsync(int id, PatientUpdateDTO item)
        {
            var patients = await repository.GetAllAsync();

            if (patients != null)
            {
                var patient = patients.Find(x => x.Id == id);
                if (patient != null)
                {
                    var translation = await translations.FindAsync(patient.Id);
                    if (translation != null)
                    {
                        translation.FirstName_EN = item.FirstName_EN;
                        translation.FirstName_AR = item.FirstName_AR;
                        translation.LastName_EN = item.LastName_EN;
                        translation.LastName_AR = item.LastName_AR;

                        await translations.UpdateAsync(translation.Id, translation.PatientId, translation);
                    }
                    byte[]? salt = null;
                    patient.FirstName = item.FirstName_EN;
                    patient.LastName = item.LastName_EN ;
                    patient.Email = item.Email.ToLower();
                    patient.Password = String.IsNullOrEmpty(item.Password)?patient.Password : PasswordHandler.Hash(item.Password,out salt);
                    patient.PasswordSalt = salt == null ? patient.PasswordSalt : Convert.ToHexString(salt);
                    patient.DOB = item.DOB;
                    patient.PhoneNumber = item.Phone;
                    patient.MedicalHistory = item.MedicalHistory;
                    await repository.UpdateAsync(patient.Id, patient);
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            await translations.DeleteAsync(id);
            await repository.DeleteAsync(id);
        }

        public async Task<bool> FindPatient(string email)
        {
            var patients = await repository.GetAllAsync();
            if (patients != null)
            {
                var patient = patients.Find(p => p.Email.ToLower() == email.ToLower());
                if (patient != null)
                {
                    return FindPatientByRefreshToken !=null;
                }
            }
            return false;
        }

        public async Task<bool> FindPatient(string email, string password)
        {
            var patients = await repository.GetAllAsync();
            if (patients != null)
            {
                var patient = patients.Find(p => p.Email.ToLower() == email.ToLower());
                if (patient != null)
                {
                    if (patient.PasswordSalt != null)
                    {
                        return PasswordHandler.VerifyPassword(password, patient.Password, Convert.FromHexString(patient.PasswordSalt));
                    }
                }
            }
            return false;
        }

        public async Task<PatientsLogins?> FindPatientByRefreshToken(string refreshToken)
        {
            var patientsLogins = await logins.GetAllAsync();
            if (patientsLogins != null)
            {
                var login = patientsLogins.Find(p => p.RefreshToken == refreshToken);
                if (login != null)
                {
                    return login;
                }
            }
            return null;
        }

        public async Task<PatientEditDTO?> GetPatientEditDTOByAccessToken(string accessToken)
        {
            var patient = await GetPatientByAccessToken(accessToken);
            if (patient != null)
            {
                var translation = await translations.FindAsync(patient.Id);
                var patientUDTO = new PatientEditDTO()
                {
                    SSN = patient.SSN,
                    Gender = patient.Gender,
                    FirstName_EN = patient.FirstName,
                    LastName_EN = patient.LastName,
                    FirstName_AR = translation?.FirstName_AR ?? "",
                    LastName_AR = translation?.LastName_AR ?? "",
                    Email = patient.Email,
                    Phone = patient.PhoneNumber,
                    MedicalHistory = patient.MedicalHistory,
                    DOB = patient.DOB,
                };
                return patientUDTO;
            }
            return null;
        }

        public async Task<LoginDTO?> Login(string email)
        {
            var patients = await repository.GetAllAsync();
            if (patients != null)
            {
                var patient = patients.Find(p => p.Email.ToLower() == email.ToLower());
                if (patient != null)
                {
                    return await GetLoginDTO(patient);
                }
            }

            return null;
        }

        public async Task<Patient?> GetPatientByAccessToken(string token)
        {
            if (token != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = iConfiguration["JWT:Key"];
                if (secretKey != null)
                {
                    var key = Encoding.UTF8.GetBytes(secretKey);

                    try
                    {
                        // Validate the token
                        var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = iConfiguration["JWT:Issuer"],
                            ValidAudience = iConfiguration["JWT:Audience"],
                            ValidateLifetime = true,
                            IssuerSigningKey = new SymmetricSecurityKey(key)
                        }, out var validatedToken);

                        if (principal != null && validatedToken != null)
                        {
                            var claim = principal.FindFirst("UserId");
                            if (claim != null)
                            {
                                // Retrieve user login details from the UserLogins table
                                var userId = claim.Value;
                                var patients = await repository.GetAllAsync();
                                var patient = patients.FirstOrDefault(p => $"{p.Id}" == userId);
                                if (patient != null)
                                    return patient;
                            }
                        }
                    }
                    catch
                    {
                        // Token validation failed
                        return null;
                    }
                }
            }
            return null;
        } 

        public async Task<LoginDTO?> Refresh(string refreshToken)
        {
            var patientsLogins = await logins.GetAllAsync();
            if (patientsLogins != null)
            {
                var login = patientsLogins.Find(p => p.RefreshToken == refreshToken);
                if (login != null)
                {
                    var patient = login.Patient;
                    if (patient != null)
                    {
                        return await GetLoginDTO(patient);
                    }
                }
            }

            return null;
        }

        private async Task<LoginDTO?> GetLoginDTO(Patient patient)
        {
            var secretKey = iConfiguration["JWT:Key"];
            if (secretKey != null)
            {
                var accessToken = TokenGenerator.GenerateAccessToken(patient, secretKey);
                var refreshToken = TokenGenerator.GenerateRefreshToken();
                var expiration = TokenGenerator.GetExpirationTime(accessToken);

                var patientDTO = await GetByIdAsync(patient.Id, "en");

                if (patientDTO != null)
                {
                    PatientsLogins login = new()
                    {
                        PatientId = patient.Id,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        Expiration = expiration,
                        LoggedIn = DateTime.Now
                    };

                    var patientsLogins = await logins.GetAllAsync();
                    if (patientsLogins != null)
                    {
                        var patientLoign = patientsLogins.Find(p => p.PatientId == patient.Id);
                        if (patientLoign != null)
                        {
                            await logins.UpdateAsync(patientLoign.Id, login);
                        }
                        else
                        {
                            await logins.InsertAsync(login);
                        }
                    }
                    else
                    {
                        await logins.InsertAsync(login);
                    }



                    LoginDTO dto = new()
                    {
                        Patient = patientDTO,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                        Expiration = expiration
                    };

                    return dto;
                }
            }

            return null;
        }
    }
}
