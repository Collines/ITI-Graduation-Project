using GraduationProject_BL.DTO.DoctorDTOs;
using GraduationProject_BL.DTO.PatientDTOs;
using GraduationProject_DAL.Data.Enums;

namespace GraduationProject_BL.DTO.ReservationDTOs
{
    public class ReservationDTO
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public int? Queue { get; set; }

        public int PatientId { get; set; }

        public ReservationStatus Status { get; set; }

        public PatientDTO? Patient { get; set; }

        public int DoctorId { get; set; }

        public DoctorDTO? Doctor { get; set; }
    }
}
