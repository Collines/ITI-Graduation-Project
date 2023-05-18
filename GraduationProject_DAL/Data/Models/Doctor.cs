using GraduationProject_DAL.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required, MaxLength(50)]
        public required string Title { get; set; }

        [Required, MaxLength(500)]
        public required string Bio { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department? Department { get; set; }

        public virtual IEnumerable<Image> Images { get; set; } = new HashSet<Image>();

        public virtual IEnumerable<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}
