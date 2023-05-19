using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{
    public class PatientsLogins
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Patient")]
        [Required]
        public required int PatientId { get; set; }

        [Required]
        public required string AccessToken { get; set; }

        [Required]
        public required string RefreshToken { get; set; }

        [Required]
        public required long Expiration { get; set; }

        [Required, DataType(DataType.DateTime)]
        public required DateTime LoggedIn { get; set; }

        public virtual Patient? Patient { get; set; }
    }
}
