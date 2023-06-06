using GraduationProject_DAL.Data.Models;
using Microsoft.AspNetCore.Http;

namespace GraduationProject_BL.DTO.DoctorDTOs
{
    public class ArticleInsertDTO
    {
        public required string Title_EN { get; set; }
        public required string Title_AR { get; set; }
        public required string Description_EN { get; set; }
        public required string Description_AR { get; set; }
        public ArticleImage Image { get; set; }
    }
}
