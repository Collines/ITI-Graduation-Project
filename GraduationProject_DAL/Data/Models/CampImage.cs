using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_DAL.Data.Models
{
    public class CampImage
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Image { get; set; }
    }
}
