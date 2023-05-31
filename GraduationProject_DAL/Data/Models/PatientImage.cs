using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    public class PatientImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}
