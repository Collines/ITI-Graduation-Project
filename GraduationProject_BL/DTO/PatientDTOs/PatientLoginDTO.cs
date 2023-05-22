using System.ComponentModel.DataAnnotations;

namespace GraduationProject_BL.DTO.PatientDTOs
{
    public class PatientLoginDTO
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
