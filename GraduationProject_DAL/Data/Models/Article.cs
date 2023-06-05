
using System.ComponentModel.DataAnnotations;

namespace GraduationProject_DAL.Data.Models
{
    public class Article
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required string Description { get; set; }
        [Required]
        public ArticleImage? Image { get; set; }
        public DateTime PostedAt { get; set; } = DateTime.Now;
    }
}
