using GraduationProject_BL.DTO.PatientDTOs;

namespace GraduationProject_BL.DTO
{
    public class LoginDTO
    {
        //public required PatientDTO Patient { get; set; }
        public required int Id { get; set; }

        public required string FullName { get; set; }

        public required string AccessToken { get; set; }

        public required string RefreshToken { get; set; }

        public required long Expiration { get; set; }

    }
}
