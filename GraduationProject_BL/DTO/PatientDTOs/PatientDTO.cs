using GraduationProject_DAL.Data.Enums;
using GraduationProject_DAL.Data.Models;

namespace GraduationProject_BL.DTO.PatientDTOs
{
    public class PatientDTO
    {
        public required int Id { get; set; }

        public string SSN { get; set; }

        public required string FullName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DOB { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Image { get;set; }

        public string? MedicalHistory { get; set; }

        public IEnumerable<Reservation> Reservations { get; set; } = new HashSet<Reservation>();

    }
}
