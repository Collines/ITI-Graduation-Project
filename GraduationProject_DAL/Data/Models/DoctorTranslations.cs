using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    [PrimaryKey(nameof(Id), nameof(DoctorId))]
    public class DoctorTranslations
    {
        [Column(Order = 0)]
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string FirstName_EN { get; set; }

        [Required, MaxLength(50)]
        public required string FirstName_AR { get; set; }

        [Required, MaxLength(50)]
        public required string LastName_EN { get; set; }

        [Required, MaxLength(50)]
        public required string LastName_AR { get; set; }

        [Required, MaxLength(50)]
        public required string Title_EN { get; set; }

        [Required, MaxLength(50)]
        public required string Title_AR { get; set; }

        [Required, MaxLength(500)]
        public required string Bio_EN { get; set; }

        [Required, MaxLength(500)]
        public required string Bio_AR { get; set; }

        [Column(Order = 1)]
        [ForeignKey("Doctor")]
        public required int DoctorId { get; set; }

        public virtual Doctor? Doctor { get; set; }
    }
}
