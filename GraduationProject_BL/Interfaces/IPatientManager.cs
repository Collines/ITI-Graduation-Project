﻿using GraduationProject_BL.DTO;
using GraduationProject_BL.DTO.PatientDTOs;
using GraduationProject_DAL.Data.Models;

namespace GraduationProject_BL.Interfaces
{
    public interface IPatientManager
    {
        public Task<List<PatientDTO>> GetAllAsync(string lang);

        public Task<PatientEditDTO?> GetPatientEditDTOByAccessToken(string accessToken);

        public Task<Patient?> GetPatientByAccessToken(string token);

        // Used to get the patient depending on the user selected language
        public Task<PatientDTO?> GetByIdAsync(int id, string lang);
        public Task<LoginDTO?> InsertAsync(PatientFormData item);
        public Task UpdateAsync(int id, PatientFormData item);
        public Task DeleteAsync(int id);
        public Task ChangeBolckStatusAsync(int id, bool blockStatus);

        // Used to check if the email already exists or not
        public Task<bool> FindPatient(string email);

        // Used to check if the user entered a correct email and password that matches the database
        public Task<bool> FindPatient(string email, string password);

        // Used to check if the user have this refresh token
        public Task<PatientsLogins?> FindPatientByRefreshToken(string refreshToken);

        // Used to get the patient from database, generate the token and return the needed data to the user
        public Task<LoginDTO?> Login(string email);

        // Used to return a new access token to the user
        public Task<LoginDTO?> Refresh(string refreshToken);

        // Used to verify if patient is blocked or not
        public Task<bool> IsBlocked(string email);
    }
}
