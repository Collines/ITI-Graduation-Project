using Microsoft.AspNetCore.Http;

namespace GraduationProject_BL.DTO.BannerDTOs
{
    public class BannerFormData
    {
        public required string Title_EN { get; set; }

        public required string Title_AR { get; set; }

        public required string Description_EN { get; set; }

        public required string Description_AR { get; set; }

        public required IFormFile Image { get; set; }
    }
}
