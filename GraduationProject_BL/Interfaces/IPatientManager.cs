using GraduationProject_BL.DTO;

namespace GraduationProject_BL.Interfaces
{
    public interface IPatientManager
    {
        // Used to get the patient depending on the user selected language
        public Task<PatientDTO?> GetByIdAsync(int id, string lang);

        // Used to check if the user entered a correct email and password that matches the database
        public Task<bool> FindPatient(string email, string password);

        // Used to get the patient from database, generate the token and return the needed data to the user
        public Task<LoginDTO?> Login(string email);
    }
}
