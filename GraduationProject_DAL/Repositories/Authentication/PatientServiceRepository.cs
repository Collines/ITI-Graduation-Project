using GraduationProject_DAL.Data.Context;
using GraduationProject_DAL.Data.Models;
using GraduationProject_DAL.Handlers;
using GraduationProject_DAL.Interfaces.Authentication;

namespace GraduationProject_DAL.Repositories.Authentication
{
    public class PatientServiceRepository : IPatientServiceRepository
    {
        private readonly HospitalDBContext Context;

        public PatientServiceRepository(HospitalDBContext db)
        {
            Context = db;
        }

        public PatientRefreshTokens AddUserRefreshTokens(PatientRefreshTokens prt)
        {
            Context.PatientRefreshTokens.Add(prt);
            return prt;
        }

        public void DeleteUserRefreshTokens(string email, string refreshToken)
        {
            var item = Context.PatientRefreshTokens.FirstOrDefault(x => x.Email == email && x.RefreshToken == refreshToken);
            if (item != null)
            {
                Context.PatientRefreshTokens.Remove(item);
            }
        }

        public PatientRefreshTokens? GetSavedRefreshTokens(string email, string refreshToken)
        {
            return Context.PatientRefreshTokens.FirstOrDefault(x => x.Email == email && x.RefreshToken == refreshToken && x.IsActive == true);
        }

        public int SaveCommit()
        {
            return Context.SaveChanges();
        }

        public bool IsValidUser(string email, string password)
        {
            Patient? patient = Context.Patients.FirstOrDefault(p => p.Email == email);
            if (patient != null)
            {
                return PasswordHandler.VerifyPassword(password, patient.Password, Convert.FromHexString(patient.PasswordSalt));
            }
            return false;
        }
    }
}
