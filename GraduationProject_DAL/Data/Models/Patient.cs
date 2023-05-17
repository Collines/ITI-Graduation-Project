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

        [Required,StringLength(14)]
        public string SSN { get; set; }

        [Required, MaxLength(50)]
        public string FName { get; set; }

        [Required, MaxLength(50)]
        public string FNameAR { get; set; }

        [Required, MaxLength(50)]
        public string LName { get; set; }

        [Required, MaxLength(50)]
        public string LNameAR { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [HiddenInput]
        public string? PasswordSalt { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DOB { get; set; }

        [EgyptianPhones]
        public string PhoneNumber { get; set; }

        [MaxLength(500)]
        public string? MedicalHistory { get; set; }

        public virtual IEnumerable<Reservation>? Reservations { get; set; }
    }
}
