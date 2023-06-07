using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    [PrimaryKey(nameof(Id), nameof(PatientId))]
    public class PatientTranslations
    {
        [Column(Order = 0)]
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50), RegularExpression("^[a-zA-z]+$")]
        public required string FirstName_EN { get; set; }

        [Required, MaxLength(50), RegularExpression("^[a-zA-z]+$")]
        public required string FirstName_AR { get; set; }

        [Required, MaxLength(50)]
        public required string LastName_EN { get; set; }

        [Required, MaxLength(50)]
        public required string LastName_AR { get; set; }

        [Column(Order = 1)]
        [ForeignKey("Patient")]
        public required int PatientId { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}
