using GraduationProject_DAL.Data.Enums;
using GraduationProject_DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraduationProject_BL.DTO.DoctorDTOs
{
    public class DoctorDTO
    {
        public required int Id { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required Gender Gender { get; set; }

        public required string Title { get; set; }

        public required string Bio { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentTitle { get; set; }

        public string? Image { get; set; }
    }
}
