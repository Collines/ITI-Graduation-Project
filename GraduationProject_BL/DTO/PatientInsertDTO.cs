using GraduationProject_DAL.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject_BL.DTO
{
    public class PatientInsertDTO
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
        //[RegularExpression("/[a-z0-9]+@[a-z]+\\.[a-z]{2,3}/",ErrorMessage ="Enter a valid Email")]
        [Required]
        public required string Email { get; set; }
        [Required]
        //[RegularExpression("/^01[0125][0-9]{8}$/",ErrorMessage ="Enter a valid phone number")]
        public required string Phone { get; set; }
        [Required]
        public required Gender Gender { get; set; }
        [Required]
        public required DateTime DOB { get; set; }
        public string? MedicalHistory { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
