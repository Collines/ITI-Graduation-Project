using GraduationProject_DAL.Data.Enums;
using GraduationProject_DAL.Data.Validators;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace GraduationProject_DAL.Data.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(14)]
        public required string SSN { get; set; }

        [Required, MaxLength(50)]
        public required string FirstName { get; set; }

        [Required, MaxLength(50)]
        public required string LastName { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public required string Password { get; set; }

        [HiddenInput]
        public string? PasswordSalt { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DOB { get; set; }

        [EgyptianPhones]
        public required string PhoneNumber { get; set; }

        [MaxLength(500)]
        public string? MedicalHistory { get; set; }

        public bool Blocked { get; set; } = false;

        public virtual PatientImage? Image { get; set; }

        public virtual IEnumerable<Reservation> Reservations { get; set; } = new HashSet<Reservation>();
    }
}
