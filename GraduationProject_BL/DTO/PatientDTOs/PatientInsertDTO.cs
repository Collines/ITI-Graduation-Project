using GraduationProject_DAL.Data.Enums;
using GraduationProject_DAL.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject_BL.DTO.PatientDTOs
{
    public class PatientInsertDTO
    {
        public int PatientId { get; set; }
        public required string SSN { get; set; }
        public required string FirstName_EN { get; set; }
        public required string FirstName_AR { get; set; }
        public required string LastName_EN { get; set; }
        public required string LastName_AR { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required Gender Gender { get; set; }
        public required DateTime DOB { get; set; }
        public string? Password { get; set; }
        public string? MedicalHistory { get; set; }
        public  PatientImage? Image { get; set; }    
    }
}
