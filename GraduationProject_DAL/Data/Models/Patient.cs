using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Data.Models
{
    public class Patient
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Key,StringLength(14)]
        public string SSN { get; set; }

        [Required, MaxLength(50)]
        public string FName { get; set; }

        [Required, MaxLength(50)]
        public string FNameAR { get; set; }

        [Required, MaxLength(50)]
        public string LName { get; set; }

        [Required, MaxLength(50)]
        public string LNameAR { get; set; }

        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DOB { get; set; }

        [EgyptianPhones]
        public string PhoneNumber { get; set; }

        [MaxLength(500)]
        public string? MedicalHistory { get; set; }

        public virtual IEnumerable<Reservation> Reservations { get; set; }
    }
}
