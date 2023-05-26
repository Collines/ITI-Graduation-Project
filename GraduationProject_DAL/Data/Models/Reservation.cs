using GraduationProject_DAL.Data.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProject_DAL.Data.Models
{

    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Range(1, int.MaxValue)]
        public int? Queue { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }

        public virtual Patient? Patient { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorId { get; set; }
        [EnumDataType(typeof(ReservationStatus))]
        [DefaultValue(ReservationStatus.ToVisit)]
        public ReservationStatus Status { get; set; }

        public virtual Doctor? Doctor { get; set; }

    }
}
