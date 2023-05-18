using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    public class DepartmentTranslations
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string Title_EN { get; set; }

        [Required, MaxLength(50)]
        public required string Title_AR { get; set; }

        [Required, MaxLength(500)]
        public required string Description_EN { get; set; }

        [Required, MaxLength(500)]
        public required string Description_AR { get; set; }

        [ForeignKey("Department")]
        [Required]
        public required int DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
    }
}
