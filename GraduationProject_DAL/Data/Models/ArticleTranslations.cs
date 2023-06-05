using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    [PrimaryKey(nameof(Id), nameof(ArticleId))]
    public class ArticleTranslations
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public required string Title_EN { get; set; }

        [Required, MaxLength(255)]
        public required string Title_AR { get; set; }

        [Required]
        public required string Description_EN { get; set; }

        [Required]
        public required string Description_AR { get; set; }

        [ForeignKey("Article")]
        public required int ArticleId { get; set; }

        public virtual Article? Article { get; set; }
    }
}
