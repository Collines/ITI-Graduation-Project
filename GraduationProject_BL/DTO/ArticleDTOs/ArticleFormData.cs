using Microsoft.AspNetCore.Http;

namespace GraduationProject_BL.DTO.DoctorDTOs
{
    public class ArticleFormData
    {
        public required string Title_EN { get; set; }
        public required string Title_AR { get; set; }
        public required string Description_EN { get; set; }
        public required string Description_AR { get; set; }
        public  IFormFile? Image { get; set; }
    }
}
