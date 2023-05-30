using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GraduationProject_BL.DTO.PatientDTOs
{
    public class PatientUpdateDTO
    {
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
        public required DateTime DOB { get; set; }
        public string? MedicalHistory { get; set; }
        public string? Password { get; set; }
        public IFormFile? Image { get; set; }

    }
}
