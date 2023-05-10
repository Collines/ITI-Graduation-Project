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
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Range(1,int.MaxValue)]
        public int? Queue { get; set; }

        [ForeignKey("Patient")]
        public string PId { get; set; }

        public virtual Patient? Patient  { get; set; }

        [ForeignKey("Doctor")]
        public int DocId { get; set; }

        public virtual Doctor? Doctor { get; set; }


    }
}
