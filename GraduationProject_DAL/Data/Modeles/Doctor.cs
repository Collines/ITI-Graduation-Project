using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Data.Modeles
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(50)]
        public string FName { get; set; }

        [Required, MaxLength(50)]
        public string FNameAR { get; set; }

        [Required, MaxLength(50)]
        public string LName { get; set; }

        [Required, MaxLength(50)]
        public string LNameAR { get; set; }

        [Required, MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(50)]
        public string TitleAR { get; set; }

        [Required, MaxLength(500)]
        public string Bio { get; set; }

        [Required, MaxLength(500)]
        public string BioAR { get; set; }

        public byte[]? Image { get; set; }

        [ForeignKey("Department")]
        public int DeptId { get; set; }
        [JsonIgnore]
        public virtual Department? Department { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<Reservation> Reservations { get; set; }
    }
}
