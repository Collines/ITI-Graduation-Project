using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    public class ArticleImage
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [ForeignKey("Article")]
        public required int ArticleId { get; set; }

        public virtual Article? Article { get; set; }

    }
}
