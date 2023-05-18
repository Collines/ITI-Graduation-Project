using GraduationProject_DAL.Data.Models;


namespace GraduationProject_DAL.Interfaces.Authentication
{
    public interface IPatientServiceRepository
    {
        bool IsValidUser(string email, string password);

        PatientRefreshTokens AddUserRefreshTokens(PatientRefreshTokens patient);

        PatientRefreshTokens? GetSavedRefreshTokens(string email, string refreshtoken);

        void DeleteUserRefreshTokens(string email, string refreshToken);

        int SaveCommit();
    }
}
