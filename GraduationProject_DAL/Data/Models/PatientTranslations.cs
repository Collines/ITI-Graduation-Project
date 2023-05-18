using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    public class PatientTranslations
    {
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

        [ForeignKey("Patient")]
        [Required]
        public required int PatientId { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}
