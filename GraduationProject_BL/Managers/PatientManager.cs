using GraduationProject_BL.DTO;
using GraduationProject_BL.Interfaces;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Handlers;
using GraduationProject_DAL.Interfaces;
using Microsoft.Extensions.Configuration;

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

        public async Task<PatientDTO?> GetByIdAsync(int id, string lang)
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

                        return dto;
                    }
                    // TODO: Remove this section when Translations are added correctly in Register
                    else
                    {
                        PatientDTO dto = new()
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

        public async Task<bool> FindPatient(string email, string password)
        {
            var patients = await repository.GetAllAsync();
            if (patients != null)
            {
                var patient = patients.Find(p => p.Email == email);
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

        public async Task<LoginDTO?> Login(string email)
        {
            var patients = await repository.GetAllAsync();
            if (patients != null)
            {
                var patient = patients.Find(p => p.Email == email);
                if (patient != null)
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

                            await logins.InsertAsync(login);


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
                }
            }

            return null;
        }
    }
}
