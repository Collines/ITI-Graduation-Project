using System.ComponentModel.DataAnnotations;

namespace GraduationProject_BL.DTO.Admin
{
    public class AdminLoginDTO
    {
        [Required]
        public required string UserName { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
