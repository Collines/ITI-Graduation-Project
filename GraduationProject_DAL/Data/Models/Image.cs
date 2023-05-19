using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }

        public virtual Doctor? Doctor { get; set; }
    }
}
