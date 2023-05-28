using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace GraduationProject_DAL.Data.Models
{
    [PrimaryKey(nameof(Id), nameof(BannerId))]

    public class BannerTranslation
    {
        [Column(Order = 0)]
        [Key]
        
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string Title_EN { get; set; }

        [Required, MaxLength(50)]
        public required string Title_AR { get; set; }

        [Required, MaxLength(50)]
        public required string Description_EN { get; set; }

        [Required, MaxLength(50)]
        public required string Description_AR { get; set; }
        [Column(Order = 1)]
        [ForeignKey("Banners")]
        public required int BannerId { get; set; }

    }
}
