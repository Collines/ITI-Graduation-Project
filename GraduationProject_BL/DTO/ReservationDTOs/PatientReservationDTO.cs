using GraduationProject_BL.DTO.DoctorDTOs;
using GraduationProject_BL.DTO.PatientDTOs;
using GraduationProject_DAL.Data.Enums;

namespace GraduationProject_BL.DTO.ReservationDTOs
{
    public class PatientReservationDTO
    {
        public required int Id { get; set; }

        public required DateTime DateTime { get; set; }

        public required int? Queue { get; set; }

        public required ReservationStatus Status { get; set; }

        public DoctorDTO? Doctor { get; set; }
    }
}
