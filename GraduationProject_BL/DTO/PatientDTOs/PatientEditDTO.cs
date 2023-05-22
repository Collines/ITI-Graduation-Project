using GraduationProject_DAL.Data.Enums;
using System.ComponentModel.DataAnnotations;


namespace GraduationProject_BL.DTO.PatientDTOs
{
    public class PatientEditDTO
    {
        [MaxLength(14)]
        [Required]
        public required string SSN { get; set; }

        [Required]
        public required string FirstName_EN { get; set; }
        [Required]
        public required string FirstName_AR { get; set; }
        [Required]
        public required string LastName_EN { get; set; }
        [Required]
        public required string LastName_AR { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Phone { get; set; }
        [Required]
        public required Gender Gender { get; set; }
        [Required]
        public required DateTime DOB { get; set; }
        public string? MedicalHistory { get; set; }
    }
}
