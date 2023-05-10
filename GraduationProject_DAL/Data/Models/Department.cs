using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Data.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required,MaxLength(50)]
        public string Title { get; set; }

        [Required, MaxLength(50)]
        public string TitleAR { get; set; }

        [Required, MaxLength(500)]
        public string Description { get; set; }

        [Required, MaxLength(500)]
        public string DescriptionAR { get; set; }

        public virtual IEnumerable<Doctor>? Doctors { get; set; }
    }
}
