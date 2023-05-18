using System.ComponentModel.DataAnnotations;

namespace GraduationProject_DAL.Data.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string Title { get; set; }

        [Required, MaxLength(500)]
        public required string Description { get; set; }

        public virtual IEnumerable<Doctor> Doctors { get; set; } = new HashSet<Doctor>();
    }
}
